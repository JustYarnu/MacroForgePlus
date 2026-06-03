using System;
using System.Collections.Generic;

public class ConditionalCommand : IMacroCommand
{
    private readonly Func<InputController, bool> _condition;
    private readonly List<IMacroCommand> _commands;

    public ConditionalCommand(Func<InputController, bool> condition, List<IMacroCommand> commands)
    {
        _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public void Execute(InputController controller)
    {
        if (!_condition(controller))
            return;

        foreach (var command in _commands)
            command.Execute(controller);
    }
}
