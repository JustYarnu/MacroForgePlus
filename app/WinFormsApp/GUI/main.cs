using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.GUI;

namespace WinFormsApp;

public partial class Main : Form
{
    private ScriptEditor? _activeEditor;
    private bool _isRunning;

    // Global hotkey for F5, F6, F7
    private const int WmHotkey = 0x0312;
    private const int HotkeyIdRun = 1;
    private const int HotkeyIdAbort = 2;
    private const int HotkeyIdStartRecording = 3;
    private const int HotkeyIdStopRecording = 4;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public Main()
    {
        InitializeComponent();
        RegisterGlobalHotkey();
    }

    private void RegisterGlobalHotkey()
    {
        // Register F5, F6, F7 and Escape as global hotkeys (no modifiers)
        RegisterHotKey(this.Handle, HotkeyIdRun, 0, (uint)Keys.F5);
        RegisterHotKey(this.Handle, HotkeyIdAbort, 0, (uint)Keys.Escape);
        RegisterHotKey(this.Handle, HotkeyIdStartRecording, 0, (uint)Keys.F6);
        RegisterHotKey(this.Handle, HotkeyIdStopRecording, 0, (uint)Keys.F7);
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WmHotkey)
        {
            var hotkeyId = m.WParam.ToInt32();
            if (hotkeyId == HotkeyIdRun)
            {
                // F5 was pressed globally
                if (_activeEditor != null && _activeEditor.Visible)
                {
                    _activeEditor.ExecuteFromExternal();
                }
                else
                {
                    RunLastMacro();
                }
            }
            else if (hotkeyId == HotkeyIdAbort)
            {
                // Global abort key pressed
                _activeEditor?.AbortFromExternal();
            }
            else if (hotkeyId == HotkeyIdStartRecording)
            {
                // F6 was pressed globally - start recording
                _activeEditor?.StartRecordingFromExternal();
            }
            else if (hotkeyId == HotkeyIdStopRecording)
            {
                // F7 was pressed globally - stop recording
                _activeEditor?.StopRecordingFromExternal();
            }
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        UnregisterHotKey(this.Handle, HotkeyIdRun);
        UnregisterHotKey(this.Handle, HotkeyIdAbort);
        UnregisterHotKey(this.Handle, HotkeyIdStartRecording);
        UnregisterHotKey(this.Handle, HotkeyIdStopRecording);
        base.OnFormClosing(e);
    }

    private void NewScriptButton_Click(object? sender, EventArgs e)
    {
        OpenNewScript();
    }

    private void NewScriptMenuItem_Click(object? sender, EventArgs e)
    {
        OpenNewScript();
    }

    private void OpenNewScript()
    {
        var editor = new ScriptEditor();
        EmbedEditor(editor);
    }

    private void OpenMacroButton_Click(object? sender, EventArgs e)
    {
        OpenMacroFile();
    }

    private void OpenMenuItem_Click(object? sender, EventArgs e)
    {
        OpenMacroFile();
    }

    private void OpenMacroFile()
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Open Macro File",
            Filter = "Macro Files|*.macro|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (_activeEditor != null)
                {
                    _activeEditor.LoadFile(openFileDialog.FileName);
                }
                else
                {
                    var editor = new ScriptEditor();
                    editor.LoadFile(openFileDialog.FileName);
                    EmbedEditor(editor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error opening file: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void RunMacroButton_Click(object? sender, EventArgs e)
    {
        RunLastMacro();
    }

    private void RunMacroMenuItem_Click(object? sender, EventArgs e)
    {
        RunLastMacro();
    }

    private void HelpMenuItem_Click(object? sender, EventArgs e)
    {
        OpenCommandReference();
    }

    private void OpenCommandReference()
    {
        try
        {
            // Try to find the commandReference.md file relative to the executable
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string docPath = Path.Combine(basePath, "doc", "commandReference.md");

            if (!File.Exists(docPath))
            {
                // Try relative to current working directory
                docPath = Path.Combine(Directory.GetCurrentDirectory(), "doc", "commandReference.md");
            }

            if (!File.Exists(docPath))
            {
                // Try relative to project root (for development)
                string? currentDir = Directory.GetCurrentDirectory();
                int idx = currentDir.LastIndexOf(Path.DirectorySeparatorChar + "app");
                if (idx > 0)
                {
                    string projectRoot = currentDir.Substring(0, idx);
                    docPath = Path.Combine(projectRoot, "doc", "commandReference.md");
                }
            }

            if (!File.Exists(docPath))
            {
                MessageBox.Show(
                    "Command reference file not found. Please ensure the documentation is installed.",
                    "Help",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Open with default application (usually a markdown viewer or text editor)
            Process.Start(new ProcessStartInfo
            {
                FileName = docPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error opening help file: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void RunLastMacro()
    {
        using var openFileDialog = new OpenFileDialog
        {
            Title = "Select Macro to Run",
            Filter = "Macro Files|*.macro|All Files|*.*",
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (_activeEditor != null)
                {
                    _activeEditor.LoadFile(openFileDialog.FileName);
                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        if (_activeEditor != null && _activeEditor.IsHandleCreated)
                        {
                            _activeEditor.Invoke(new Action(() => _activeEditor.ExecuteFromExternal()));
                        }
                    });
                }
                else
                {
                    var editor = new ScriptEditor();
                    editor.LoadFile(openFileDialog.FileName);
                    EmbedEditor(editor);

                    Task.Delay(2000).ContinueWith(_ =>
                    {
                        if (editor.IsHandleCreated)
                        {
                            editor.Invoke(new Action(() => editor.ExecuteFromExternal()));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error running macro: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    private void EmbedEditor(ScriptEditor editor)
    {
        if (_activeEditor != null)
        {
            _activeEditor.Close();
        }

        topMenu.Visible = false;
        MainMenuStrip = editor.EditorMenuStrip;
        mainContentPanel.Padding = Padding.Empty;

        editor.TopLevel = false;
        editor.FormBorderStyle = FormBorderStyle.None;
        editor.Dock = DockStyle.Fill;

        mainContentPanel.Controls.Clear();
        mainContentPanel.Controls.Add(editor);

        TrackEditor(editor);
        editor.Show();
    }

    private void ShowWelcomeCard()
    {
        mainContentPanel.Controls.Clear();
        mainContentPanel.Padding = new Padding(20, 10, 20, 20);
        mainContentPanel.Controls.Add(contentTableLayout);
        topMenu.Visible = true;
        MainMenuStrip = topMenu;
    }

    private void TrackEditor(ScriptEditor editor)
    {
        _activeEditor = editor;
        editor.Activated += (s, e) => _activeEditor = editor;
        editor.FormClosed += (s, e) =>
        {
            if (_activeEditor == editor)
            {
                _activeEditor = null;
                ShowWelcomeCard();
            }
        };
    }
}