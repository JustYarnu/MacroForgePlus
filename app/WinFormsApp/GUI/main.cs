using System;
using System.Windows.Forms;
using WinFormsApp.GUI;

namespace WinFormsApp;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
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
            // Basic functionality - just show a message for now
            // Domain-specific logic (loading and parsing the macro) will be implemented later
            MessageBox.Show(
                $"Selected file: {openFileDialog.FileName}\n\n" +
                "Macro loading functionality will be implemented soon!",
                "Open Macro",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }

    private void RunMacroButton_Click(object? sender, EventArgs e)
    {
        // Basic functionality - just show a message for now
        // Domain-specific logic (running the macro) will be implemented later
        MessageBox.Show(
            "Macro running functionality will be implemented soon!\n\n" +
            "For now, try opening a macro file first using the 'Open Macro' panel.",
            "Run Macro",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }
}
