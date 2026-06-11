using System;
using System.IO;
using System.Windows.Forms;
using WinFormsApp.Keybinds;

namespace WinFormsApp.GUI;

public partial class KeybindEditDialog : Form
{
    private readonly KeybindEntry? _existingEntry;
    private readonly KeybindProfile _profile;

    public KeybindEntry? KeybindEntry { get; private set; }

    public KeybindEditDialog(KeybindEntry? existingEntry, KeybindProfile profile)
    {
        _existingEntry = existingEntry;
        _profile = profile;
        InitializeComponent();
        InitializeKeyComboBox();
        
        if (_existingEntry != null)
        {
            PopulateFromEntry(_existingEntry);
            titleLabel.Text = "Edit Keybind";
        }
        else
        {
            KeybindEntry = new KeybindEntry();
            titleLabel.Text = "Add Keybind";
        }
    }

    private void InitializeKeyComboBox()
    {
        // Add common keys that are safe to use as hotkeys
        var keys = new[]
        {
            // Function keys
            Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12,
            // Number keys
            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
            // Letter keys
            Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M,
            Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z,
            // Other keys
            Keys.Insert, Keys.Delete, Keys.Home, Keys.End, Keys.PageUp, Keys.PageDown,
            Keys.Up, Keys.Down, Keys.Left, Keys.Right,
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4,
            Keys.NumPad5, Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
            Keys.OemMinus, Keys.Oemplus, Keys.Oemcomma, Keys.OemPeriod
        };

        foreach (var key in keys)
        {
            keyComboBox.Items.Add(new KeyDisplayItem(key));
        }

        keyComboBox.DisplayMember = "DisplayName";
    }

    private void PopulateFromEntry(KeybindEntry entry)
    {
        // Select the key in combo box
        foreach (var item in keyComboBox.Items)
        {
            if (item is KeyDisplayItem kdi && kdi.Key == entry.Key)
            {
                keyComboBox.SelectedItem = kdi;
                break;
            }
        }

        ctrlCheckbox.Checked = entry.Control;
        altCheckbox.Checked = entry.Alt;
        shiftCheckbox.Checked = entry.Shift;
        scriptPathTextBox.Text = entry.ScriptPath;
        enabledCheckbox.Checked = entry.Enabled;
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Select Macro Script",
            Filter = "Macro Files|*.macro|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            scriptPathTextBox.Text = openFileDialog.FileName;
        }
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        // Validate inputs
        if (keyComboBox.SelectedItem == null)
        {
            MessageBox.Show(
                "Please select a key.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(scriptPathTextBox.Text))
        {
            MessageBox.Show(
                "Please select a script file.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (!File.Exists(scriptPathTextBox.Text))
        {
            MessageBox.Show(
                "The specified script file does not exist.",
                "File Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        // Create or update the keybind entry
        var keyDisplayItem = (KeyDisplayItem)keyComboBox.SelectedItem;
        
        if (_existingEntry != null)
        {
            // Update existing entry
            _existingEntry.Key = keyDisplayItem.Key;
            _existingEntry.Control = ctrlCheckbox.Checked;
            _existingEntry.Alt = altCheckbox.Checked;
            _existingEntry.Shift = shiftCheckbox.Checked;
            _existingEntry.ScriptPath = scriptPathTextBox.Text;
            _existingEntry.Enabled = enabledCheckbox.Checked;
            KeybindEntry = _existingEntry;
        }
        else
        {
            // Create new entry
            KeybindEntry = new KeybindEntry
            {
                Key = keyDisplayItem.Key,
                Control = ctrlCheckbox.Checked,
                Alt = altCheckbox.Checked,
                Shift = shiftCheckbox.Checked,
                ScriptPath = scriptPathTextBox.Text,
                Enabled = enabledCheckbox.Checked
            };
        }

        // Check for duplicate keybinds
        if (_profile.IsKeybindTaken(KeybindEntry))
        {
            MessageBox.Show(
                "This key combination is already bound to another script.",
                "Duplicate Keybind",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void CancelButton_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    /// Helper class for displaying keys in the combo box.
    /// </summary>
    private class KeyDisplayItem
    {
        public Keys Key { get; }
        public string DisplayName { get; }

        public KeyDisplayItem(Keys key)
        {
            Key = key;
            DisplayName = key.ToString();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}