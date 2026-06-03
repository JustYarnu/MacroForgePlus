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

    public async Task RunAsync(List<IMacroCommand> commands)
    {
        Stop();
        
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        await Task.Run(() => 
        {
            try
            {
                foreach (var command in commands)
                {
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"Executing {command.GetType().Name}");
                    command.Execute(_controller);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Macro execution aborted by user.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Macro execution failed: {ex.Message}");
            }
        }, token);
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