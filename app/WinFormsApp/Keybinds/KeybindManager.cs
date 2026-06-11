using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.Keybinds;

/// <summary>
/// Manages keybind registration, execution, and lifecycle.
/// Handles global hotkey registration and macro execution.
/// </summary>
public class KeybindManager : IDisposable
{
    private readonly Main _mainForm;
    private readonly KeybindProfile _profile;
    private readonly Dictionary<int, KeybindEntry> _registeredHotkeys;
    private bool _disposed;

    // Windows API constants
    private const int WmHotkey = 0x0312;
    private const uint ModControl = 0x0002;
    private const uint ModAlt = 0x0001;
    private const uint ModShift = 0x0004;

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>
    /// Event raised when a keybind is triggered.
    /// </summary>
    public event EventHandler<KeybindTriggeredEventArgs>? KeybindTriggered;

    /// <summary>
    /// Creates a new KeybindManager instance.
    /// </summary>
    /// <param name="mainForm">The main form that will receive hotkey messages.</param>
    /// <param name="profile">The keybind profile to use.</param>
    public KeybindManager(Main mainForm, KeybindProfile profile)
    {
        _mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
        _profile = profile ?? throw new ArgumentNullException(nameof(profile));
        _registeredHotkeys = new Dictionary<int, KeybindEntry>();
    }

    /// <summary>
    /// Registers all enabled keybinds from the profile as global hotkeys.
    /// </summary>
    public void RegisterAllKeybinds()
    {
        UnregisterAllKeybinds();

        foreach (var keybind in _profile.GetEnabledKeybinds())
        {
            RegisterKeybind(keybind);
        }
    }

    /// <summary>
    /// Registers a single keybind as a global hotkey.
    /// </summary>
    /// <param name="keybind">The keybind to register.</param>
    /// <returns>True if registration succeeded, false otherwise.</returns>
    public bool RegisterKeybind(KeybindEntry keybind)
    {
        if (keybind == null || string.IsNullOrEmpty(keybind.ScriptPath))
            return false;

        // Check if already registered
        if (_registeredHotkeys.ContainsKey(keybind.HotkeyId))
            return false;

        bool result = RegisterHotKey(_mainForm.Handle, keybind.HotkeyId, keybind.ModifierFlags, keybind.VirtualKeyCode);
        
        if (result)
        {
            _registeredHotkeys[keybind.HotkeyId] = keybind;
        }

        return result;
    }

    /// <summary>
    /// Unregisters a single keybind.
    /// </summary>
    /// <param name="keybind">The keybind to unregister.</param>
    public void UnregisterKeybind(KeybindEntry keybind)
    {
        if (keybind == null)
            return;

        if (_registeredHotkeys.ContainsKey(keybind.HotkeyId))
        {
            UnregisterHotKey(_mainForm.Handle, keybind.HotkeyId);
            _registeredHotkeys.Remove(keybind.HotkeyId);
        }
    }

    /// <summary>
    /// Unregisters all keybinds.
    /// </summary>
    public void UnregisterAllKeybinds()
    {
        foreach (var hotkeyId in _registeredHotkeys.Keys)
        {
            UnregisterHotKey(_mainForm.Handle, hotkeyId);
        }
        _registeredHotkeys.Clear();
    }

    /// <summary>
    /// Handles a hotkey message from the main form's WndProc.
    /// </summary>
    /// <param name="hotkeyId">The hotkey ID that was triggered.</param>
    public void HandleHotkey(int hotkeyId)
    {
        if (_registeredHotkeys.TryGetValue(hotkeyId, out var keybind))
        {
            ExecuteKeybind(keybind);
        }
    }

    /// <summary>
    /// Executes a keybind's associated macro script.
    /// </summary>
    /// <param name="keybind">The keybind to execute.</param>
    private async void ExecuteKeybind(KeybindEntry keybind)
    {
        if (string.IsNullOrEmpty(keybind.ScriptPath) || !File.Exists(keybind.ScriptPath))
        {
            KeybindTriggered?.Invoke(this, new KeybindTriggeredEventArgs(keybind, false, "Script file not found"));
            return;
        }

        try
        {
            KeybindTriggered?.Invoke(this, new KeybindTriggeredEventArgs(keybind, true, null));

            // Load and execute the script
            string scriptContent = File.ReadAllText(keybind.ScriptPath);
            var parser = new ScriptParser();
            var parsedScript = parser.ParseScript(scriptContent);

            var inputController = new InputController();
            var executionEngine = new ExecutionEngine(inputController);

            await Task.Run(async () =>
            {
                try
                {
                    await executionEngine.RunAsync(parsedScript);
                }
                catch (Exception ex)
                {
                    if (KeybindTriggered != null)
                    {
                        _mainForm.Invoke(new Action(() =>
                        {
                            KeybindTriggered?.Invoke(this, new KeybindTriggeredEventArgs(keybind, false, ex.Message));
                        }));
                    }
                }
            });
        }
        catch (Exception ex)
        {
            KeybindTriggered?.Invoke(this, new KeybindTriggeredEventArgs(keybind, false, ex.Message));
        }
    }

    /// <summary>
    /// Gets the profile managed by this KeybindManager.
    /// </summary>
    public KeybindProfile Profile => _profile;

    /// <summary>
    /// Gets the number of currently registered keybinds.
    /// </summary>
    public int RegisteredCount => _registeredHotkeys.Count;

    /// <summary>
    /// Gets all registered keybinds.
    /// </summary>
    public IEnumerable<KeybindEntry> RegisteredKeybinds => _registeredHotkeys.Values;

    /// <summary>
    /// Saves the current profile to disk.
    /// </summary>
    public void SaveProfile()
    {
        _profile.Save();
    }

    /// <summary>
    /// Reloads the profile from disk and re-registers keybinds.
    /// </summary>
    public void ReloadProfile()
    {
        var newProfile = KeybindProfile.Load();
        _profile.Keybinds.Clear();
        _profile.Keybinds.AddRange(newProfile.Keybinds);
        _profile.Name = newProfile.Name;
        _profile.Description = newProfile.Description;
        _profile.AutoLoad = newProfile.AutoLoad;

        RegisterAllKeybinds();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            UnregisterAllKeybinds();
        }

        _disposed = true;
    }
}

/// <summary>
/// Event args for when a keybind is triggered.
/// </summary>
public class KeybindTriggeredEventArgs : EventArgs
{
    public KeybindEntry Keybind { get; }
    public bool Success { get; }
    public string? ErrorMessage { get; }

    public KeybindTriggeredEventArgs(KeybindEntry keybind, bool success, string? errorMessage)
    {
        Keybind = keybind;
        Success = success;
        ErrorMessage = errorMessage;
    }
}