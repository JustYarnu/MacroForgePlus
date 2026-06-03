using System;
using System.Collections.Generic;
using System.IO;
using WindowsInput.Events;

public class ScriptParser
{
    public List<IMacroCommand> ParseScript(string scriptText)
    {
        var rootCommands = new List<IMacroCommand>();
        var blockStack = new Stack<BlockContext>();
        blockStack.Push(new BlockContext(rootCommands));

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

                    blockStack.Peek().Commands.Add(ParseWaitToken(timeArg));
                }

                // If this is a block terminator, close the latest block
                if (currentIndex < tokens.Length && tokens[currentIndex] == "endif")
                {
                    currentIndex++;
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endif' without a matching conditional start.");

                    var completedBlock = blockStack.Pop();
                    blockStack.Peek().Commands.Add(new ConditionalCommand(completedBlock.Condition!, completedBlock.Commands));
                    continue;
                }

                if (currentIndex >= tokens.Length)
                    continue;

                // 5. Read the Action and Arguments
                string action = tokens[currentIndex++];

                if (action == "endif")
                {
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endif' without a matching conditional start.");

                    var completedBlock = blockStack.Pop();
                    if (completedBlock.Kind != BlockKind.Conditional)
                        throw new FormatException("Found 'endif' closing a non-conditional block.");

                    blockStack.Peek().Commands.Add(new ConditionalCommand(completedBlock.Condition!, completedBlock.Commands));
                    continue;
                }

                if (action == "endrepeat")
                {
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endrepeat' without a matching repeat start.");

                    var completedBlock = blockStack.Pop();
                    if (completedBlock.Kind != BlockKind.Loop)
                        throw new FormatException("Found 'endrepeat' closing a non-repeat block.");

                    blockStack.Peek().Commands.Add(new LoopCommand(completedBlock.RepeatCount, completedBlock.Commands));
                    continue;
                }

                if (IsConditionalStart(inputTarget, action))
                {
                    var condition = ParseConditional(inputTarget, action, tokens, ref currentIndex);
                    blockStack.Push(new BlockContext(condition, new List<IMacroCommand>()));
                    continue;
                }

                if (IsLoopStart(inputTarget, action))
                {
                    int repeatCount = ParseRepeatCount(tokens, ref currentIndex);
                    blockStack.Push(new BlockContext(repeatCount, new List<IMacroCommand>()));
                    continue;
                }

                if (inputTarget == "mouse")
                {
                    blockStack.Peek().Commands.Add(ParseMouseAction(action, tokens, ref currentIndex));
                }
                else if (inputTarget == "keyboard")
                {
                    blockStack.Peek().Commands.Add(ParseKeyboardAction(action, tokens, ref currentIndex));
                }
                else if (inputTarget == "engine")
                {
                    if (action != "wait")
                        throw new FormatException($"Unknown engine action: '{action}'");

                    // engine wait is already handled by the optional wait modifier
                }
                else
                {
                    throw new FormatException($"Unknown input target: '{inputTarget}'");
                }
            }
            catch (Exception ex)
            {
                throw new FormatException($"Error parsing script on line {i + 1}: '{trimmedLine}'. Details: {ex.Message}");
            }
        }

        if (blockStack.Count != 1)
            throw new FormatException("Unclosed conditional block detected in script.");

        return rootCommands;
    }

    private static bool IsConditionalStart(string inputTarget, string action)
        => action == "ifheld" && (inputTarget == "keyboard" || inputTarget == "mouse")
           || inputTarget == "engine" && (action == "ifon" || action == "ifoff");

    private static bool IsLoopStart(string inputTarget, string action)
        => inputTarget == "engine" && action == "repeat";

    private static int ParseRepeatCount(string[] tokens, ref int index)
    {
        if (index >= tokens.Length)
            throw new FormatException("Repeat requires a count.");

        if (!int.TryParse(tokens[index++], out int count))
            throw new FormatException($"Repeat count '{tokens[index - 1]}' is not a valid integer.");

        if (count < 0)
            throw new FormatException("Repeat count must be zero or greater.");

        return count;
    }

    private static Func<InputController, bool> ParseConditional(string inputTarget, string action, string[] tokens, ref int index)
    {
        if (index >= tokens.Length)
            throw new FormatException($"Conditional '{action}' requires an argument.");

        string conditionArg = tokens[index++];

        if (action == "ifheld")
        {
            if (inputTarget == "keyboard")
            {
                var key = ParseKeyCode(conditionArg);
                return controller => controller.IsKeyHeld(key);
            }

            if (inputTarget == "mouse")
            {
                var button = Enum.Parse<ButtonCode>(conditionArg, true);
                return controller => controller.IsButtonHeld(button);
            }
        }

        if (inputTarget == "engine")
        {
            return action switch
            {
                "ifon" => controller => controller.IsToggleOn(conditionArg),
                "ifoff" => controller => controller.IsToggleOff(conditionArg),
                _ => throw new FormatException($"Unknown engine conditional action: '{action}'")
            };
        }

        throw new FormatException($"Unsupported conditional action: '{inputTarget} {action}'");
    }

    private sealed class BlockContext
    {
        public BlockKind Kind { get; }
        public Func<InputController, bool>? Condition { get; }
        public int RepeatCount { get; }
        public List<IMacroCommand> Commands { get; }

        public BlockContext(List<IMacroCommand> commands)
        {
            Kind = BlockKind.Root;
            Commands = commands;
        }

        public BlockContext(Func<InputController, bool> condition, List<IMacroCommand> commands)
        {
            Kind = BlockKind.Conditional;
            Condition = condition;
            Commands = commands;
        }

        public BlockContext(int repeatCount, List<IMacroCommand> commands)
        {
            Kind = BlockKind.Loop;
            RepeatCount = repeatCount;
            Commands = commands;
        }
    }

    private enum BlockKind
    {
        Root,
        Conditional,
        Loop
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

    private static KeyCode ParseKeyCode(string token)
    {
        return token.ToLowerInvariant() switch
        {
            "ctrl" => KeyCode.Control,
            "control" => KeyCode.Control,
            "shift" => KeyCode.Shift,
            "alt" => KeyCode.Alt,
            "oskey" => KeyCode.LWin,
            "altgr" => KeyCode.RAlt,
            "capslock" => KeyCode.CapsLock,
            "numlock" => KeyCode.NumLock,
            "scrolllock" => KeyCode.Scroll,
            _ => Enum.Parse<KeyCode>(token, true)
        };
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
                var key = ParseKeyCode(tokens[index++]);
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
                var modifier = ParseKeyCode(tokens[index++]);
                var targetKey = ParseKeyCode(tokens[index++]);
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