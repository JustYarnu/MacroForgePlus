using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsInput.Events;
using WindowsInput.Native;

namespace WinFormsApp.GUI;

public sealed class RecordingManager : IDisposable
{
    private const int WhKeyboardLl = 13;
    private const int WhMouseLl = 14;
    private const int WmKeydown = 0x0100;
    private const int WmKeyup = 0x0101;
    private const int WmSyskeydown = 0x0104;
    private const int WmSyskeyup = 0x0105;
    private const int WmMousemove = 0x0200;
    private const int WmLbuttondown = 0x0201;
    private const int WmLbuttonup = 0x0202;
    private const int WmRbuttondown = 0x0204;
    private const int WmRbuttonup = 0x0205;
    private const int WmMbuttondown = 0x0207;
    private const int WmMbuttonup = 0x0208;
    private const int WmXbuttondown = 0x020B;
    private const int WmXbuttonup = 0x020C;
    private const int WmMousewheel = 0x020A;

    private readonly List<IMacroCommand> _recordedCommands = new();
    private readonly HookProc _keyboardHookProc;
    private readonly HookProc _mouseHookProc;
    private IntPtr _keyboardHook = IntPtr.Zero;
    private IntPtr _mouseHook = IntPtr.Zero;
    private DateTime? _lastTimestamp;
    private POINT _lastMousePoint;
    private DateTime _lastMouseMoveTime;

    public bool IsRecording { get; private set; }

    public RecordingManager()
    {
        _keyboardHookProc = KeyboardHookCallback;
        _mouseHookProc = MouseHookCallback;
    }

    public void Start()
    {
        if (IsRecording)
            return;

        _recordedCommands.Clear();
        _lastTimestamp = null;
        _lastMousePoint = default;
        _lastMouseMoveTime = DateTime.MinValue;

        _keyboardHook = SetWindowsHookEx(WhKeyboardLl, _keyboardHookProc, GetModuleHandle(null), 0);
        _mouseHook = SetWindowsHookEx(WhMouseLl, _mouseHookProc, GetModuleHandle(null), 0);

        if (_keyboardHook == IntPtr.Zero || _mouseHook == IntPtr.Zero)
        {
            Stop();
            throw new InvalidOperationException("Unable to register recording hooks.");
        }

        IsRecording = true;
    }

    public IReadOnlyList<IMacroCommand> Stop()
    {
        if (!IsRecording)
            return _recordedCommands.AsReadOnly();

        if (_keyboardHook != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_keyboardHook);
            _keyboardHook = IntPtr.Zero;
        }

        if (_mouseHook != IntPtr.Zero)
        {
            UnhookWindowsHookEx(_mouseHook);
            _mouseHook = IntPtr.Zero;
        }

        IsRecording = false;
        return _recordedCommands.AsReadOnly();
    }

    public void Dispose()
    {
        Stop();
    }

    private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && IsRecording)
        {
            int messageType = wParam.ToInt32();
            var data = Marshal.PtrToStructure<KeyboardHookStruct>(lParam);
            var key = TranslateVirtualKey((int)data.VirtualKeyCode);

            if (messageType == WmKeydown || messageType == WmSyskeydown)
            {
                if (IsModifierKey(key))
                {
                    Record(new KeyboardCommand(key, KeyAction.Down));
                }
                else
                {
                    Record(new KeyboardCommand(key, KeyAction.Press));
                }
            }
            else if (messageType == WmKeyup || messageType == WmSyskeyup)
            {
                if (IsModifierKey(key))
                {
                    Record(new KeyboardCommand(key, KeyAction.Up));
                }
            }
        }

        return CallNextHookEx(_keyboardHook, nCode, wParam, lParam);
    }

    private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && IsRecording)
        {
            int messageType = wParam.ToInt32();
            var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            var now = DateTime.UtcNow;

            switch (messageType)
            {
                case WmMousemove:
                    if (ShouldRecordMouseMove(data.Point, now))
                    {
                        Record(new MouseMoveCommand(data.Point.X, data.Point.Y, isRelative: false));
                    }
                    break;
                case WmLbuttondown:
                    Record(new MouseButtonCommand(ButtonCode.Left, ButtonAction.Down));
                    break;
                case WmLbuttonup:
                    Record(new MouseButtonCommand(ButtonCode.Left, ButtonAction.Up));
                    break;
                case WmRbuttondown:
                    Record(new MouseButtonCommand(ButtonCode.Right, ButtonAction.Down));
                    break;
                case WmRbuttonup:
                    Record(new MouseButtonCommand(ButtonCode.Right, ButtonAction.Up));
                    break;
                case WmMbuttondown:
                    Record(new MouseButtonCommand(ButtonCode.Middle, ButtonAction.Down));
                    break;
                case WmMbuttonup:
                    Record(new MouseButtonCommand(ButtonCode.Middle, ButtonAction.Up));
                    break;
                case WmXbuttondown:
                    Record(new MouseButtonCommand(GetXButton(data.MouseData), ButtonAction.Down));
                    break;
                case WmXbuttonup:
                    Record(new MouseButtonCommand(GetXButton(data.MouseData), ButtonAction.Up));
                    break;
                case WmMousewheel:
                    var delta = (short)((data.MouseData >> 16) & 0xffff);
                    if (delta != 0)
                    {
                        var direction = delta > 0 ? ButtonCode.VScroll : ButtonCode.VScroll;
                        Record(new MouseScrollCommand(direction, Math.Abs(delta) / 120));
                    }
                    break;
            }
        }

        return CallNextHookEx(_mouseHook, nCode, wParam, lParam);
    }

    private bool ShouldRecordMouseMove(POINT point, DateTime now)
    {
        if (_lastMousePoint.X == point.X && _lastMousePoint.Y == point.Y)
            return false;

        if ((now - _lastMouseMoveTime).TotalMilliseconds < 150)
            return false;

        _lastMousePoint = point;
        _lastMouseMoveTime = now;
        return true;
    }

    private void Record(IMacroCommand command)
    {
        var timestamp = DateTime.UtcNow;
        if (_lastTimestamp.HasValue)
        {
            int delay = (int)Math.Round((timestamp - _lastTimestamp.Value).TotalMilliseconds);
            if (delay >= 40)
            {
                _recordedCommands.Add(new WaitCommand(delay));
            }
        }

        _recordedCommands.Add(command);
        _lastTimestamp = timestamp;
    }

    private static bool IsModifierKey(KeyCode key)
    {
        return key == KeyCode.Control || key == KeyCode.Shift || key == KeyCode.Alt || key == KeyCode.LWin || key == KeyCode.RWin || key == KeyCode.Menu;
    }

    private static KeyCode TranslateVirtualKey(int vk)
    {
        try
        {
            return (KeyCode)vk;
        }
        catch
        {
            return KeyCode.None;
        }
    }

    private static ButtonCode GetXButton(uint mouseData)
    {
        return (ushort)((mouseData >> 16) & 0xffff) == 1 ? ButtonCode.XButton1 : ButtonCode.XButton2;
    }

    public static string FormatCommands(IEnumerable<IMacroCommand> commands)
    {
        var lines = new List<string>();
        foreach (var command in commands)
        {
            switch (command)
            {
                case WaitCommand wait:
                    lines.Add(FormatWait(wait));
                    break;
                case MouseMoveCommand move:
                    lines.Add(move.IsRelative ? $"mouse moveby {move.X} {move.Y}" : $"mouse moveto {move.X} {move.Y}");
                    break;
                case MouseButtonCommand button:
                    lines.Add($"mouse {FormatMouseAction(button.Action)} {FormatButton(button.Button)}");
                    break;
                case MouseScrollCommand scroll:
                    lines.Add($"mouse scroll {FormatButton(scroll.button)} {scroll.clicks}");
                    break;
                case KeyboardCommand keyboard:
                    lines.Add($"keyboard {FormatKeyboardAction(keyboard.Action)} {FormatKey(keyboard.Key)}");
                    break;
                case KeyboardComboCommand combo:
                    lines.Add($"keyboard combo {FormatKey(combo.ModifierKey)} {FormatKey(combo.PrimaryKey)}");
                    break;
                case KeyboardTypeTextCommand textCommand:
                    if (!string.IsNullOrEmpty(textCommand.TextString))
                    {
                        lines.Add($"keyboard type {EscapeText(textCommand.TextString)}");
                    }
                    else if (textCommand.TextKeys != null)
                    {
                        lines.Add($"keyboard type {string.Join(" ", Array.ConvertAll(textCommand.TextKeys, FormatKey))}");
                    }
                    break;
                default:
                    lines.Add($"# Unsupported command: {command.GetType().Name}");
                    break;
            }
        }

        return string.Join(Environment.NewLine, lines);
    }

    private static string FormatWait(WaitCommand wait)
    {
        return wait.MinDelay == wait.MaxDelay
            ? $"engine wait {wait.MinDelay}"
            : $"engine wait r[{wait.MinDelay},{wait.MaxDelay}]";
    }

    private static string FormatMouseAction(ButtonAction action)
    {
        return action switch
        {
            ButtonAction.Down => "down",
            ButtonAction.Up => "up",
            ButtonAction.Press => "click",
            _ => action.ToString().ToLowerInvariant()
        };
    }

    private static string FormatKeyboardAction(KeyAction action)
    {
        return action switch
        {
            KeyAction.Down => "down",
            KeyAction.Up => "up",
            KeyAction.Press => "press",
            _ => action.ToString().ToLowerInvariant()
        };
    }

    private static string FormatButton(ButtonCode button)
    {
        return button.ToString().ToLowerInvariant();
    }

    private static string FormatKey(KeyCode key)
    {
        return key switch
        {
            KeyCode.Control => "ctrl",
            KeyCode.Shift => "shift",
            KeyCode.Alt => "alt",
            KeyCode.LWin => "oskey",
            KeyCode.RWin => "oskey",
            _ => key.ToString().ToLowerInvariant()
        };
    }

    private static string EscapeText(string text)
    {
        if (text.Contains(" "))
            return text;

        return text;
    }

    private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    private struct KeyboardHookStruct
    {
        public uint VirtualKeyCode;
        public uint ScanCode;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MSLLHOOKSTRUCT
    {
        public POINT Point;
        public uint MouseData;
        public uint Flags;
        public uint Time;
        public IntPtr ExtraInfo;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr GetModuleHandle(string? lpModuleName);

    private static Process GetCurrentProcess() => Process.GetCurrentProcess();
}
