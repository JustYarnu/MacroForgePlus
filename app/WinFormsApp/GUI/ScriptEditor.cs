using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp.GUI;

public partial class ScriptEditor : Form
{
    public ScriptEditor()
    {
        InitializeComponent();
        UpdateLineNumbers();
    }

    private void UpdateLineNumbers()
    {
        int lineCount = scriptTextBox.Lines.Length;
        string lineNumbers = "";
        for (int i = 1; i <= lineCount; i++)
        {
            lineNumbers += i + "\n";
        }
        lineNumbersLabel.Text = lineNumbers;
    }

    private void ScriptTextBox_TextChanged(object? sender, EventArgs e)
    {
        UpdateLineNumbers();
    }


    private void CloseToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        this.Close();
    }

    private void UndoToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        if (scriptTextBox.CanUndo)
        {
            scriptTextBox.Undo();
        }
    }

    private void RedoToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        scriptTextBox.Redo();
    }
}