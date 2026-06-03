using System;
using System.Collections.Generic;

public class LoopCommand : IMacroCommand
{
    private readonly int _repeatCount;
    private readonly List<IMacroCommand> _commands;

    public LoopCommand(int repeatCount, List<IMacroCommand> commands)
    {
        if (repeatCount < 0)
            throw new ArgumentOutOfRangeException(nameof(repeatCount), "Repeat count must be zero or greater.");

        _repeatCount = repeatCount;
        _commands = commands ?? throw new ArgumentNullException(nameof(commands));
    }

    public void Execute(InputController controller)
    {
        for (int iteration = 0; iteration < _repeatCount; iteration++)
        {
            foreach (var command in _commands)
            {
                command.Execute(controller);
            }
        }
    }
}
