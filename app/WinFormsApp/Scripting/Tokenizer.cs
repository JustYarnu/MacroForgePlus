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
        var variables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var functions = new Dictionary<string, List<IMacroCommand>>(StringComparer.OrdinalIgnoreCase);
        bool variablesSectionEnded = false;
        bool functionSectionEnded = false;

        var lines = scriptText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            string trimmedLine = lines[i].Trim();

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
                string inputTarget = tokens[currentIndex++].ToLowerInvariant();

                // 4. Check for the optional "wait" modifier
                if (currentIndex < tokens.Length && tokens[currentIndex].ToLowerInvariant() == "wait")
                {
                    currentIndex++; // Consume 'wait'
                    string timeArg = ResolveToken(tokens[currentIndex++], variables);

                    blockStack.Peek().Commands.Add(ParseWaitToken(timeArg));
                }

                if (currentIndex >= tokens.Length)
                {
                    if (variablesSectionEnded == false && IsVariableDirective(inputTarget, string.Empty))
                        throw new FormatException("Variable directive requires an action.");

                    variablesSectionEnded = true;
                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;
                    continue;
                }

                // If this is a block terminator, close the latest block
                if (currentIndex < tokens.Length && tokens[currentIndex].ToLowerInvariant() == "endif")
                {
                    currentIndex++;
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endif' without a matching conditional start.");

                    var completedBlock = blockStack.Pop();
                    blockStack.Peek().Commands.Add(new ConditionalCommand(completedBlock.Condition!, completedBlock.Commands));
                    continue;
                }

                // 5. Read the Action and Arguments
                string action = tokens[currentIndex++].ToLowerInvariant();

                if (action == "endif")
                {
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endif' without a matching conditional start.");

                    var completedBlock = blockStack.Pop();
                    if (completedBlock.Kind != BlockKind.Conditional)
                        throw new FormatException("Found 'endif' closing a non-conditional block.");

                    blockStack.Peek().Commands.Add(new ConditionalCommand(completedBlock.Condition!, completedBlock.Commands));
                    variablesSectionEnded = true;
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
                    variablesSectionEnded = true;
                    continue;
                }

                if (action == "endfunction")
                {
                    if (blockStack.Count == 1)
                        throw new FormatException("Found 'endfunction' without a matching function start.");

                    var completedBlock = blockStack.Pop();
                    if (completedBlock.Kind != BlockKind.Function)
                        throw new FormatException("Found 'endfunction' closing a non-function block.");

                    if (functions.ContainsKey(completedBlock.FunctionName!))
                        throw new FormatException($"Function '{completedBlock.FunctionName}' is already defined.");

                    functions[completedBlock.FunctionName!] = completedBlock.Commands;
                    variablesSectionEnded = true;
                    continue;
                }

                if (IsVariableDirective(inputTarget, action))
                {
                    if (variablesSectionEnded)
                        throw new FormatException("Variable declarations must occur before any executable commands.");

                    ParseVariableDirective(action, tokens, ref currentIndex, variables);
                    continue;
                }

                variablesSectionEnded = true;

                if (IsFunctionStart(inputTarget, action))
                {
                    if (blockStack.Peek().Kind != BlockKind.Root)
                        throw new FormatException("Function definitions may only be declared at the root of the script.");

                    if (functionSectionEnded)
                        throw new FormatException("Function definitions must occur before executable commands.");

                    if (currentIndex >= tokens.Length)
                        throw new FormatException("setfunction requires a function name.");

                    string functionName = ResolveToken(tokens[currentIndex++], variables).ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(functionName))
                        throw new FormatException("Function name cannot be empty.");

                    blockStack.Push(new BlockContext(functionName, new List<IMacroCommand>()));
                    continue;
                }

                if (IsFunctionCall(inputTarget, action))
                {
                    if (currentIndex >= tokens.Length)
                        throw new FormatException("callfunction requires a function name.");

                    string functionName = ResolveToken(tokens[currentIndex++], variables).ToLowerInvariant();
                    if (!functions.TryGetValue(functionName, out var functionCommands))
                        throw new FormatException($"Function '{functionName}' is not defined.");

                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;

                    blockStack.Peek().Commands.Add(new FunctionCallCommand(functionName, functionCommands));
                    continue;
                }

                if (IsConditionalStart(inputTarget, action))
                {
                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;

                    var conditionArg = ResolveToken(tokens[currentIndex++], variables);
                    var condition = ParseConditional(inputTarget, action, conditionArg);
                    blockStack.Push(new BlockContext(condition, new List<IMacroCommand>()));
                    continue;
                }

                if (IsLoopStart(inputTarget, action))
                {
                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;

                    int repeatCount = ParseRepeatCount(tokens, ref currentIndex, variables);
                    blockStack.Push(new BlockContext(repeatCount, new List<IMacroCommand>()));
                    continue;
                }

                if (inputTarget == "mouse")
                {
                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;

                    blockStack.Peek().Commands.Add(ParseMouseAction(action, tokens, ref currentIndex, variables));
                }
                else if (inputTarget == "keyboard")
                {
                    if (blockStack.Peek().Kind == BlockKind.Root)
                        functionSectionEnded = true;

                    blockStack.Peek().Commands.Add(ParseKeyboardAction(action, tokens, ref currentIndex, variables));
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

    private static bool IsVariableDirective(string inputTarget, string action)
        => inputTarget == "engine" && (action == "setvar" || action == "updatevar" || action == "deletevar");

    private static bool IsFunctionDirective(string inputTarget, string action)
        => inputTarget == "engine" && action == "setfunction";

    private static void ParseVariableDirective(string action, string[] tokens, ref int index, Dictionary<string, string> variables)
    {
        if (index >= tokens.Length)
            throw new FormatException($"'{action}' requires a variable name.");

        string variableName = tokens[index++].ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(variableName))
            throw new FormatException("Variable name cannot be empty.");

        switch (action)
        {
            case "setvar":
                if (index >= tokens.Length)
                    throw new FormatException("setvar requires a value.");

                string setValue = string.Join(" ", tokens, index, tokens.Length - index);
                variables[variableName] = setValue;
                break;

            case "updatevar":
                if (!variables.ContainsKey(variableName))
                    throw new FormatException($"Variable '{variableName}' does not exist.");

                if (index >= tokens.Length)
                    throw new FormatException("updatevar requires a value.");

                string updateValue = string.Join(" ", tokens, index, tokens.Length - index);
                variables[variableName] = updateValue;
                break;

            case "deletevar":
                variables.Remove(variableName);
                break;

            default:
                throw new FormatException($"Unknown variable directive: '{action}'");
        }
    }

    private static string ResolveToken(string token, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(token))
            return token;

        if (token.StartsWith("$") && token.Length > 1 && token[1] != '{')
        {
            string lookupName = token.Substring(1);
            if (!variables.TryGetValue(lookupName, out var value))
                throw new FormatException($"Variable '{lookupName}' is not defined.");

            return value;
        }

        return ResolveVariableExpressions(token, variables);
    }

    private static string ResolveTextValue(string text, Dictionary<string, string> variables)
    {
        return ResolveVariableExpressions(text, variables);
    }

    private static string ResolveVariableExpressions(string text, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(text) || !text.Contains("${"))
            return text;

        int index = 0;
        var builder = new System.Text.StringBuilder();

        while (index < text.Length)
        {
            int start = text.IndexOf("${", index, StringComparison.Ordinal);
            if (start == -1)
            {
                builder.Append(text[index..]);
                break;
            }

            builder.Append(text[index..start]);
            int end = text.IndexOf('}', start + 2);
            if (end == -1)
                throw new FormatException("Variable expression is not terminated with '}'.");

            string variableName = text[(start + 2)..end];
            if (!variables.TryGetValue(variableName, out var value))
                throw new FormatException($"Variable '{variableName}' is not defined.");

            builder.Append(value);
            index = end + 1;
        }

        return builder.ToString();
    }

    private static bool IsFunctionStart(string inputTarget, string action)
        => inputTarget == "engine" && action == "setfunction";

    private static bool IsFunctionCall(string inputTarget, string action)
        => inputTarget == "engine" && action == "callfunction";

    private static bool IsLoopStart(string inputTarget, string action)
        => inputTarget == "engine" && action == "repeat";

    private static int ParseRepeatCount(string[] tokens, ref int index, Dictionary<string, string> variables)
    {
        if (index >= tokens.Length)
            throw new FormatException("Repeat requires a count.");

        string countToken = ResolveToken(tokens[index++], variables);
        if (!int.TryParse(countToken, out int count))
            throw new FormatException($"Repeat count '{countToken}' is not a valid integer.");

        if (count < 0)
            throw new FormatException("Repeat count must be zero or greater.");

        return count;
    }

    private static Func<InputController, bool> ParseConditional(string inputTarget, string action, string conditionArg)
    {

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

        public BlockContext(string functionName, List<IMacroCommand> commands)
        {
            Kind = BlockKind.Function;
            FunctionName = functionName;
            Commands = commands;
        }

        public string? FunctionName { get; }
    }

    private enum BlockKind
    {
        Root,
        Conditional,
        Loop,
        Function
    }

    // --- R[min,max] logic ---
    private WaitCommand ParseWaitToken(string timeArg)
    {
        // Check if it's the randomized format: r[min,max]
        if (timeArg.StartsWith("r[", StringComparison.OrdinalIgnoreCase))
        {
            string inner = timeArg.Substring(2).Trim('[', ']');
            var parts = inner.Split(',');

            if (parts.Length != 2)
                throw new FormatException("Random wait must have exactly two values separated by a comma.");

            return new WaitCommand(int.Parse(parts[0]), int.Parse(parts[1]));
        }
        
        // Otherwise, it's a static delay
        return new WaitCommand(int.Parse(timeArg));
    }

    private IMacroCommand ParseMouseAction(string action, string[] tokens, ref int index, Dictionary<string, string> variables)
    {
        switch (action)
        {
            case "move":
            case "moveto":
            case "moveby":
                int x = int.Parse(ResolveToken(tokens[index++], variables));
                int y = int.Parse(ResolveToken(tokens[index++], variables));
                return new MouseMoveCommand(x, y, isRelative: action == "moveby");

            case "scroll":
                var directionToken = ResolveToken(tokens[index++], variables);
                int clicks = int.Parse(ResolveToken(tokens[index++], variables));
                var (direction, normalizedClicks) = ParseScrollDirection(directionToken, clicks);
                return new MouseScrollCommand(direction, normalizedClicks);

            case "down":
            case "hold":
            case "up":
            case "release":
            case "press":
            case "click":
                var button = Enum.Parse<ButtonCode>(ResolveToken(tokens[index++], variables), true);
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

    private static (ButtonCode direction, int clicks) ParseScrollDirection(string token, int clicks)
    {
        var normalized = token.ToLowerInvariant();
        return normalized switch
        {
            "up" => (ButtonCode.VScroll, -Math.Abs(clicks)),
            "down" => (ButtonCode.VScroll, Math.Abs(clicks)),
            "left" => (ButtonCode.HScroll, -Math.Abs(clicks)),
            "right" => (ButtonCode.HScroll, Math.Abs(clicks)),
            "vscroll" => (ButtonCode.VScroll, clicks),
            "hscroll" => (ButtonCode.HScroll, clicks),
            "scroll" => (ButtonCode.VScroll, clicks),
            _ when Enum.TryParse<ButtonCode>(token, true, out var direction) => (direction, clicks),
            _ => throw new FormatException($"Unknown scroll direction: '{token}'")
        };
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

    private IMacroCommand ParseKeyboardAction(string action, string[] tokens, ref int index, Dictionary<string, string> variables)
    {
        switch (action)
        {
            case "down":
            case "hold":
            case "up":
            case "release":
            case "press":
            case "tap":
                var key = ParseKeyCode(ResolveToken(tokens[index++], variables));
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
                var modifier = ParseKeyCode(ResolveToken(tokens[index++], variables));
                var targetKey = ParseKeyCode(ResolveToken(tokens[index++], variables));
                return new KeyboardComboCommand(modifier, targetKey);

            case "type":
                // Join all remaining tokens back together for the string typing
                string textToType = ResolveTextValue(string.Join(" ", tokens, index, tokens.Length - index), variables);
                index = tokens.Length; // Fast-forward index to the end
                return new KeyboardTypeTextCommand(textToType);

            default:
                throw new FormatException($"Unknown keyboard action: {action}");
        }
    }
}