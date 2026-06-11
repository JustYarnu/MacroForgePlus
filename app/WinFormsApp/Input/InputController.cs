using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput.Events;

public class InputController
{
    private readonly MouseController mouse;
    private readonly KeyboardController keyboard;
    private readonly Random rnd;

    private readonly object _stateLock = new object();
    private bool _autoRandomize;
    private int _autoRandomizeLowerBound = 10;
    private int _autoRandomizeUpperBound = 30;
    private bool _abortButton;
    private bool _inputBuffering;
    private PhysicalInputBlockMode _blockPhysicalInputMode;

    private BlockingCollection<Action>? _actionQueue;
    private Thread? _bufferThread;
    private CancellationToken? _bufferCancellationToken;
    private readonly ManualResetEventSlim _bufferIdle = new(true);

    public bool AutoRandomize
    {
        get => _autoRandomize;
        set => _autoRandomize = value;
    }

    public int AutoRandomizeLowerBound
    {
        get => _autoRandomizeLowerBound;
        set => _autoRandomizeLowerBound = value;
    }

    public int AutoRandomizeUpperBound
    {
        get => _autoRandomizeUpperBound;
        set => _autoRandomizeUpperBound = value;
    }

    public bool AbortButton
    {
        get => _abortButton;
        set => _abortButton = value;
    }

    public bool InputBuffering => _inputBuffering;

    public InputController()
    {
        mouse = new MouseController();
        keyboard = new KeyboardController();
        rnd = new Random();
    }
    
    public void BeginBuffering(CancellationToken token)
    {
        if (_inputBuffering)
            return;

        _inputBuffering = true;
        _bufferCancellationToken = token;
        _actionQueue = new BlockingCollection<Action>();
        _bufferIdle.Set();

        _bufferThread = new Thread(ProcessBufferedActions)
        {
            IsBackground = true,
            Name = "InputBufferWorker"
        };
        _bufferThread.Start();
    }

    public void EndBuffering()
    {
        if (!_inputBuffering)
            return;

        if (_actionQueue != null)
            _actionQueue.CompleteAdding();

        _bufferThread?.Join(5000);
        _actionQueue = null;
        _bufferThread = null;
        _bufferCancellationToken = null;
        _inputBuffering = false;
        _bufferIdle.Set();
    }

    public void FlushBuffer(CancellationToken token)
    {
        if (!_inputBuffering)
            return;

        _bufferIdle.Wait(token);
    }

    public void SetBlockPhysicalInput(PhysicalInputBlockMode mode)
    {
        if (_blockPhysicalInputMode == mode)
            return;

        if (_blockPhysicalInputMode != PhysicalInputBlockMode.None)
            SetPhysicalInputBlocked(false);

        _blockPhysicalInputMode = mode;

        if (mode != PhysicalInputBlockMode.None)
            SetPhysicalInputBlocked(true);
    }

    public void Wait(int delay)
    {
        ExecuteOrQueue(() => SleepWithAbort(delay));
    }

    public void RandomWait(int lowerBound, int upperBound)
    {
        if (lowerBound > upperBound)
            (lowerBound, upperBound) = (upperBound, lowerBound);

        ExecuteOrQueue(() => SleepWithAbort(rnd.Next(lowerBound, upperBound + 1)));
    }

    public void MouseMoveTo(int x, int y) => PerformAction(() => mouse.MoveTo(x, y));
    public void MouseMoveBy(int x, int y) => PerformAction(() => mouse.MoveBy(x, y));
    public void MouseScroll(ButtonCode direction, int clicks) => PerformAction(() => mouse.Scroll(direction, clicks));

    public void MouseButtonDown(ButtonCode button) => PerformAction(() => mouse.ButtonDown(button));
    public void MouseButtonUp(ButtonCode button) => PerformAction(() => mouse.ButtonUp(button));
    public void MouseButtonPress(ButtonCode button) => PerformAction(() => mouse.ButtonPress(button));

    public void MouseDelayedButtonDown(ButtonCode button, int delay)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(delay);
            mouse.ButtonDown(button);
        });
    }

    public void RandomDelayedMouseButtonDown(ButtonCode button, int lowerBound, int upperBound)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(rnd.Next(lowerBound, upperBound + 1));
            mouse.ButtonDown(button);
        });
    }

    public void MouseDelayedButtonUp(ButtonCode button, int delay)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(delay);
            mouse.ButtonUp(button);
        });
    }

    public void RandomDelayedMouseButtonUp(ButtonCode button, int lowerBound, int upperBound)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(rnd.Next(lowerBound, upperBound + 1));
            mouse.ButtonUp(button);
        });
    }

    public void DelayedMouseButtonPress(ButtonCode button, int delay)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(delay);
            mouse.ButtonPress(button);
        });
    }
    
    public void RandomDelayedMouseButtonPress(ButtonCode button, int lowerBound, int upperBound)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(rnd.Next(lowerBound, upperBound + 1));
            mouse.ButtonPress(button);
        });
    }

    public void KeyboardKeyDown(KeyCode key) => PerformAction(() => keyboard.KeyDown(key));
    public void KeyboardKeyUp(KeyCode key) => PerformAction(() => keyboard.KeyUp(key));
    public void KeyboardKeyPress(KeyCode key) => PerformAction(() => keyboard.KeyPress(key));
    
    public void KeyboardModifiedStroke(KeyCode modifier, KeyCode key) => PerformAction(() => keyboard.ModifiedStroke(modifier, key));
    
    public void KeyboardTypeText(string text) => PerformAction(() => keyboard.TypeText(text));
    public void KeyboardTypeText(KeyCode[] text) => PerformAction(() => keyboard.TypeText(text));

    public void KeyboardDelayedKeyPress(KeyCode key, int delay)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(delay);
            keyboard.KeyPress(key);
        });
    }

    public void RandomDelayedKeyboardKeyPress(KeyCode key, int lowerBound, int upperBound)
    {
        ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            SleepWithAbort(rnd.Next(lowerBound, upperBound + 1));
            keyboard.KeyPress(key);
        });
    }

    public bool IsKeyHeld(KeyCode key)
        => IsVirtualKeyDown((int)key);

    public bool IsButtonHeld(ButtonCode button)
        => IsVirtualKeyDown(GetVirtualKeyFromButton(button));

    public bool IsToggleOn(string toggleName)
    {
        if (toggleName is null)
            throw new ArgumentNullException(nameof(toggleName));

        return toggleName.ToLowerInvariant() switch
        {
            "capslock" => Control.IsKeyLocked(Keys.CapsLock),
            "numlock" => Control.IsKeyLocked(Keys.NumLock),
            "scrolllock" => Control.IsKeyLocked(Keys.Scroll),
            _ => throw new ArgumentException($"Unknown toggle key: '{toggleName}'", nameof(toggleName))
        };
    }

    public bool IsToggleOff(string toggleName)
        => !IsToggleOn(toggleName);

    public bool IsAbortPressed()
        => IsVirtualKeyDown((int)Keys.Escape);

    private void PerformAction(Action action)
        => ExecuteOrQueue(() =>
        {
            MaybeAbort();
            MaybeAutoRandomize();
            action();
        });

    private void ExecuteOrQueue(Action action)
    {
        MaybeAbort();

        if (_inputBuffering && _actionQueue != null)
        {
            _bufferIdle.Reset();
            _actionQueue.Add(action);
            return;
        }

        action();
    }

    private void ProcessBufferedActions()
    {
        if (_actionQueue == null)
            return;

        try
        {
            foreach (var action in _actionQueue.GetConsumingEnumerable(_bufferCancellationToken ?? CancellationToken.None))
            {
                try
                {
                    action();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch
                {
                    // Allow the engine to handle exceptions and stop processing on errors.
                    break;
                }

                if (_actionQueue.Count == 0)
                    _bufferIdle.Set();
            }
        }
        catch (OperationCanceledException)
        {
            // Cancellation has been requested.
        }
        finally
        {
            _bufferIdle.Set();
        }
    }

    private void MaybeAutoRandomize()
    {
        if (!_autoRandomize)
            return;

        int lowerBound = Math.Min(_autoRandomizeLowerBound, _autoRandomizeUpperBound);
        int upperBound = Math.Max(_autoRandomizeLowerBound, _autoRandomizeUpperBound);
        Thread.Sleep(rnd.Next(lowerBound, upperBound + 1));
    }

    private void MaybeAbort()
    {
        if (_abortButton && IsAbortPressed())
            throw new OperationCanceledException();
    }

    private void SleepWithAbort(int milliseconds)
    {
        var remaining = milliseconds;
        const int slice = 50;

        while (remaining > 0)
        {
            MaybeAbort();
            Thread.Sleep(Math.Min(slice, remaining));
            remaining -= slice;
        }
    }

    private static bool IsVirtualKeyDown(int virtualKey)
        => (GetAsyncKeyState(virtualKey) & 0x8000) != 0;

    private static int GetVirtualKeyFromButton(ButtonCode button)
        => button switch
        {
            ButtonCode.Left => 0x01,
            ButtonCode.Right => 0x02,
            ButtonCode.Middle => 0x04,
            ButtonCode.XButton1 => 0x05,
            ButtonCode.XButton2 => 0x06,
            _ => throw new ArgumentException($"Unsupported mouse button for condition: '{button}'", nameof(button))
        };

    private void SetPhysicalInputBlocked(bool block)
    {
        if (!BlockInput(block))
            throw new InvalidOperationException($"Unable to {(block ? "enable" : "disable")} physical input blocking.");
    }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool BlockInput(bool fBlockIt);

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);
}