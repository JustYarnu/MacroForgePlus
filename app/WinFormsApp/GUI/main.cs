using System;
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

    // Global hotkey for F5
    private const int WmHotkey = 0x0312;
    private const int HotkeyIdRun = 1;
    private const int HotkeyIdAbort = 2;

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
        // Register F5 and Escape as global hotkeys (no modifiers)
        RegisterHotKey(this.Handle, HotkeyIdRun, 0, (uint)Keys.F5);
        RegisterHotKey(this.Handle, HotkeyIdAbort, 0, (uint)Keys.Escape);
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
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        UnregisterHotKey(this.Handle, HotkeyIdRun);
        UnregisterHotKey(this.Handle, HotkeyIdAbort);
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
        TrackEditor(editor);
        editor.Show();
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
                // Open the file in a new editor window
                var editor = new ScriptEditor();
                editor.LoadFile(openFileDialog.FileName);
                TrackEditor(editor);
                editor.Show();
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
                // Open the file in a new editor window and auto-execute after a delay
                var editor = new ScriptEditor();
                editor.LoadFile(openFileDialog.FileName);
                TrackEditor(editor);
                editor.Show();

                // Auto-execute after a 2 second delay
                Task.Delay(2000).ContinueWith(_ =>
                {
                    if (editor.IsHandleCreated)
                    {
                        editor.Invoke(new Action(() => editor.ExecuteFromExternal()));
                    }
                });
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

    private void TrackEditor(ScriptEditor editor)
    {
        _activeEditor = editor;
        editor.Activated += (s, e) => _activeEditor = editor;
        editor.FormClosed += (s, e) =>
        {
            if (_activeEditor == editor)
            {
                _activeEditor = null;
            }
        };
    }
}