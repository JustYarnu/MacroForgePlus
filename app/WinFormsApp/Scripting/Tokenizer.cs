using System;
using System.Collections.Generic;
using System.IO;
using WindowsInput.Events;

public class ScriptParser
{
    public List<IMacroCommand> ParseScript(string scriptText)
    {
        var commands = new List<IMacroCommand>();
        var lines = scriptText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            // 1. Normalize the line (Lowercase and Trim)
            string trimmedLine = lines[i].Trim().ToLowerInvariant();

            // Skip comments and empty lines
            if (trimmedLine.StartsWith("#") || string.IsNullOrWhiteSpace(trimmedLine))
                continue;

            // 2. Tokenize by spaces
            var tokens = trimmedLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 0) continue;

            try
            {
                int currentIndex = 0;
                
                // 3. Read the Target Input (mouse, keyboard, engine)
                string inputTarget = tokens[currentIndex++];

                // 4. Check for the optional "wait" modifier
                if (currentIndex < tokens.Length && tokens[currentIndex] == "wait")
                {
                    currentIndex++; // Consume 'wait'
                    string timeArg = tokens[currentIndex++]; // e.g., "1000" or "r[50,120]"
                    
                    // Add the wait command immediately before the action
                    commands.Add(ParseWaitToken(timeArg)); 
                }

                // If the target was just "engine" (e.g. engine wait 1000), we are done with this line
                if (inputTarget == "engine")
                    continue;

                // 5. Read the Action and Arguments
                string action = tokens[currentIndex++];
                
                if (inputTarget == "mouse")
                {
                    commands.Add(ParseMouseAction(action, tokens, ref currentIndex));
                }
                else if (inputTarget == "keyboard")
                {
                    commands.Add(ParseKeyboardAction(action, tokens, ref currentIndex));
                }
                else
                {
                    throw new FormatException($"Unknown input target: '{inputTarget}'");
                }
            }
            catch (Exception ex)
            {
                // Attach the line number so the user knows exactly where their script broke
                throw new FormatException($"Error parsing script on line {i + 1}: '{trimmedLine}'. Details: {ex.Message}");
            }
        }

        return commands;
    }

    // --- R[min,max] logic ---
    private WaitCommand ParseWaitToken(string timeArg)
    {
        // Check if it's the randomized format: r[min,max]
        if (timeArg.StartsWith("r["))
        {
            // Strip the 'r', '[', and ']' characters
            string inner = timeArg.Trim('r', '[', ']');
            var parts = inner.Split(',');

            if (parts.Length != 2)
                throw new FormatException("Random wait must have exactly two values separated by a comma.");

            return new WaitCommand(int.Parse(parts[0]), int.Parse(parts[1]));
        }
        
        // Otherwise, it's a static static delay
        return new WaitCommand(int.Parse(timeArg));
    }

    private IMacroCommand ParseMouseAction(string action, string[] tokens, ref int index)
    {
        switch (action)
        {
            case "move":
            case "moveto":
            case "moveby":
                int x = int.Parse(tokens[index++]);
                int y = int.Parse(tokens[index++]);
                return new MouseMoveCommand(x, y, isRelative: action == "moveby");

            case "scroll":
                var direction = Enum.Parse<ButtonCode>(tokens[index++], true);
                int clicks = int.Parse(tokens[index++]);
                return new MouseScrollCommand(direction, clicks); 

            case "down":
            case "hold":
            case "up":
            case "release":
            case "press":
            case "click":
                var button = Enum.Parse<ButtonCode>(tokens[index++], true);
                var buttonAction = action switch
                {
                    "down" => ButtonAction.Down,
                    "hold" => ButtonAction.Down,
                    "up" => ButtonAction.Up,
                    "release" => ButtonAction.Up,
                    "press" => ButtonAction.Press,
                    "click" => ButtonAction.Press,
                    _ => Enum.Parse<ButtonAction>(action, true)
                };
                return new MouseButtonCommand(button, buttonAction);

            default:
                throw new FormatException($"Unknown mouse action: {action}");
        }
    }

    private IMacroCommand ParseKeyboardAction(string action, string[] tokens, ref int index)
    {
        switch (action)
        {
            case "down":
            case "hold":
            case "up":
            case "release":
            case "press":
            case "tap":
                var key = Enum.Parse<KeyCode>(tokens[index++], true);
                var keyAction = action switch
                {
                    "down" => KeyAction.Down,
                    "hold" => KeyAction.Down,
                    "up" => KeyAction.Up,
                    "release" => KeyAction.Up,
                    "press" => KeyAction.Press,
                    "tap" => KeyAction.Press,
                    _ => Enum.Parse<KeyAction>(action, true)
                };
                return new KeyboardCommand(key, keyAction);

            case "combo":
                var modifier = Enum.Parse<KeyCode>(tokens[index++], true);
                var targetKey = Enum.Parse<KeyCode>(tokens[index++], true);
                return new KeyboardComboCommand(modifier, targetKey);

            case "type":
                // Join all remaining tokens back together for the string typing
                string textToType = string.Join(" ", tokens, index, tokens.Length - index);
                index = tokens.Length; // Fast-forward index to the end
                return new KeyboardTypeTextCommand(textToType);

            default:
                throw new FormatException($"Unknown keyboard action: {action}");
        }
    }
}