using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class ExecutionEngine
{
    private readonly InputController _controller;
    private CancellationTokenSource? _cts;

    public ExecutionEngine(InputController controller)
    {
        _controller = controller;
    }

    public async Task RunAsync(ParsedScript parsedScript)
    {
        Stop();

        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        _controller.AutoRandomize = parsedScript.Options.AutoRandomize;
        _controller.AutoRandomizeLowerBound = parsedScript.Options.AutoRandomizeLowerBound;
        _controller.AutoRandomizeUpperBound = parsedScript.Options.AutoRandomizeUpperBound;
        _controller.AbortButton = parsedScript.Options.AbortButton;
        _controller.SetBlockPhysicalInput(parsedScript.Options.BlockPhysicalInput);

        try
        {
            if (parsedScript.Options.InputBuffering)
                _controller.BeginBuffering(token);

            await Task.Run(() => ExecuteScript(parsedScript, token), token);
        }
        finally
        {
            _controller.EndBuffering();
            _controller.SetBlockPhysicalInput(PhysicalInputBlockMode.None);
        }
    }

    private void ExecuteScript(ParsedScript parsedScript, CancellationToken token)
    {
        try
        {
            bool firstRun = true;
            do
            {
                if (!firstRun && parsedScript.Options.RepeatMode == RepeatMode.Interval)
                {
                    WaitForInterval(parsedScript.Options.RepeatIntervalMinutes, token);
                }

                token.ThrowIfCancellationRequested();
                ExecuteCommands(parsedScript.Commands, token);

                firstRun = false;
            }
            while (!token.IsCancellationRequested &&
                   (parsedScript.Options.RepeatMode == RepeatMode.Infinite || parsedScript.Options.RepeatMode == RepeatMode.Interval));
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Macro execution aborted by user.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Macro execution failed: {ex.Message}");
        }
    }

    private void ExecuteCommands(List<IMacroCommand> commands, CancellationToken token)
    {
        foreach (var command in commands)
        {
            token.ThrowIfCancellationRequested();

            if (_controller.AbortButton && _controller.IsAbortPressed())
                throw new OperationCanceledException();

            Console.WriteLine($"Executing {command.GetType().Name}");
            command.Execute(_controller);
        }

        if (_controller.InputBuffering)
            _controller.FlushBuffer(token);
    }

    private static void WaitForInterval(int intervalMinutes, CancellationToken token)
    {
        if (intervalMinutes <= 0)
            return;

        Task.Delay(intervalMinutes * 60_000, token).Wait(token);
    }

    public void Stop()
    {
        if (_cts != null && !_cts.IsCancellationRequested)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}