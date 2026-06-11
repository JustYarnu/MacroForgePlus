using System;
using System.IO;
using System.Windows.Forms;
using WinFormsApp.Keybinds;

namespace WinFormsApp.GUI;

public partial class KeybindManagerDialog : Form
{
    private readonly KeybindProfile _profile;
    private KeybindEntry? _selectedEntry;

    public event EventHandler? ProfileUpdated;

    public KeybindManagerDialog(KeybindProfile profile)
    {
        _profile = profile;
        InitializeComponent();
        LoadKeybinds();
    }

    private void LoadKeybinds()
    {
        keybindsList.Items.Clear();
        
        foreach (var keybind in _profile.Keybinds)
        {
            var item = new ListViewItem(keybind.KeyDisplay);
            item.SubItems.Add(keybind.ScriptFileName);
            item.SubItems.Add(keybind.Enabled ? "Yes" : "No");
            item.Tag = keybind;
            keybindsList.Items.Add(item);
        }
    }

    private void KeybindsList_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (keybindsList.SelectedItems.Count > 0)
        {
            _selectedEntry = keybindsList.SelectedItems[0].Tag as KeybindEntry;
        }
        else
        {
            _selectedEntry = null;
        }
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
        var editDialog = new KeybindEditDialog(null, _profile);
        if (editDialog.ShowDialog(this) == DialogResult.OK)
        {
            var newKeybind = editDialog.KeybindEntry;
            if (newKeybind != null)
            {
                _profile.AddKeybind(newKeybind);
                LoadKeybinds();
            }
        }
    }

    private void EditButton_Click(object? sender, EventArgs e)
    {
        if (_selectedEntry == null)
        {
            MessageBox.Show(
                "Please select a keybind to edit.",
                "No Selection",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var editDialog = new KeybindEditDialog(_selectedEntry, _profile);
        if (editDialog.ShowDialog(this) == DialogResult.OK)
        {
            LoadKeybinds();
        }
    }

    private void RemoveButton_Click(object? sender, EventArgs e)
    {
        if (_selectedEntry == null)
        {
            MessageBox.Show(
                "Please select a keybind to remove.",
                "No Selection",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show(
            $"Are you sure you want to remove the keybind '{_selectedEntry.KeyDisplay}'?",
            "Confirm Remove",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
            _profile.RemoveKeybind(_selectedEntry);
            _selectedEntry = null;
            LoadKeybinds();
        }
    }

    private void CloseButton_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog
        {
            Title = "Save Keybind Profile",
            Filter = "Macro Profile Files|*.mprofile|All Files|*.*",
            DefaultExt = "mprofile",
            AddExtension = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                _profile.Save(saveFileDialog.FileName);
                ProfileUpdated?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(
                    "Profile saved successfully!",
                    "Save Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error saving profile: {ex.Message}",
                    "Save Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void LoadButton_Click(object? sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Load Keybind Profile",
            Filter = "Macro Profile Files|*.mprofile|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                var loadedProfile = KeybindProfile.Load(openFileDialog.FileName);
                _profile.Keybinds.Clear();
                _profile.Keybinds.AddRange(loadedProfile.Keybinds);
                _profile.Name = loadedProfile.Name;
                _profile.Description = loadedProfile.Description;
                _profile.AutoLoad = loadedProfile.AutoLoad;
                LoadKeybinds();
                ProfileUpdated?.Invoke(this, EventArgs.Empty);
                
                MessageBox.Show(
                    "Profile loaded successfully!",
                    "Load Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading profile: {ex.Message}",
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}