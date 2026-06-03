using System;
using System.Collections.Generic;

public class FunctionCallCommand : IMacroCommand
{
    private readonly List<IMacroCommand> _commands;

    public string FunctionName { get; }

    public FunctionCallCommand(string functionName, List<IMacroCommand> commands)
    {
        FunctionName = functionName ?? throw new ArgumentNullException(nameof(functionName));
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public void Execute(InputController controller)
    {
        foreach (var command in _commands)
        {
            command.Execute(controller);
        }
    }
}
