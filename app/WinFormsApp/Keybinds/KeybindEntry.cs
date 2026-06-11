using System;
using System.Windows.Forms;

namespace WinFormsApp.Keybinds;

/// <summary>
/// Represents a single keybind entry that maps a key combination to a macro script.
/// </summary>
public class KeybindEntry
{
    /// <summary>
    /// Unique identifier for this keybind entry.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The key to trigger this keybind.
    /// </summary>
    public Keys Key { get; set; }

    /// <summary>
    /// Whether Ctrl modifier is required.
    /// </summary>
    public bool Control { get; set; }

    /// <summary>
    /// Whether Alt modifier is required.
    /// </summary>
    public bool Alt { get; set; }

    /// <summary>
    /// Whether Shift modifier is required.
    /// </summary>
    public bool Shift { get; set; }

    /// <summary>
    /// Path to the macro script file to execute.
    /// </summary>
    public string ScriptPath { get; set; } = string.Empty;

    /// <summary>
    /// Whether this keybind is currently enabled.
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// Gets a display name for the key combination.
    /// </summary>
    public string KeyDisplay
    {
        get
        {
            var parts = new System.Collections.Generic.List<string>();
            if (Control) parts.Add("Ctrl");
            if (Alt) parts.Add("Alt");
            if (Shift) parts.Add("Shift");
            parts.Add(Key.ToString());
            return string.Join("+", parts);
        }
    }

    /// <summary>
    /// Gets the hotkey ID for Windows API registration.
    /// </summary>
    public int HotkeyId => Id.GetHashCode();

    /// <summary>
    /// Gets the modifier flags for RegisterHotKey API.
    /// </summary>
    public uint ModifierFlags
    {
        get
        {
            uint flags = 0;
            if (Control) flags |= 0x0002; // MOD_CONTROL
            if (Alt) flags |= 0x0001;     // MOD_ALT
            if (Shift) flags |= 0x0004;   // MOD_SHIFT
            return flags;
        }
    }

    /// <summary>
    /// Gets the virtual key code for RegisterHotKey API.
    /// </summary>
    public uint VirtualKeyCode => (uint)Key;

    /// <summary>
    /// Gets the file name only (without path) for display purposes.
    /// </summary>
    public string ScriptFileName => System.IO.Path.GetFileName(ScriptPath);

    /// <summary>
    /// Creates a deep copy of this keybind entry.
    /// </summary>
    public KeybindEntry Clone()
    {
        return new KeybindEntry
        {
            Id = this.Id,
            Key = this.Key,
            Control = this.Control,
            Alt = this.Alt,
            Shift = this.Shift,
            ScriptPath = this.ScriptPath,
            Enabled = this.Enabled
        };
    }
}