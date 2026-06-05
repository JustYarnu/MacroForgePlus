using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp.GUI;

public partial class ScriptEditor : Form
{
    private static readonly string[] HighlightKeywords = new[]
    {
        "mouse", "keyboard", "engine",
        "wait", "move", "moveto", "moveby", "scroll",
        "down", "hold", "up", "release", "press", "click",
        "type", "combo", "tap",
        "ifheld", "endif", "ifon", "ifoff",
        "repeat", "endrepeat",
        "setvar", "updatevar", "deletevar",
        "setfunction", "endfunction", "callfunction",
        "ctrl", "shift", "alt", "oskey", "altgr",
        "capslock", "numlock", "scrolllock"
    };

    public ScriptEditor()
    {
        InitializeComponent();
        UpdateLineNumbers();
        ApplySyntaxHighlighting();
    }

    private void UpdateLineNumbers()
    {
        int lineCount = Math.Max(1, scriptTextBox.Lines.Length);
        var builder = new StringBuilder();

        for (int i = 1; i <= lineCount; i++)
        {
            builder.AppendLine(i.ToString());
        }

        lineNumbersLabel.Text = builder.ToString();

        int lineHeight = TextRenderer.MeasureText("0", scriptTextBox.Font).Height;
        lineNumbersLabel.Size = new Size(lineNumbersPanel.Width, Math.Max(lineNumbersPanel.Height, lineCount * lineHeight));
        lineNumbersLabel.Location = new Point(0, -GetFirstVisibleLine() * lineHeight);
    }

    private void ApplySyntaxHighlighting()
    {
        string text = scriptTextBox.Text;
        int selectionStart = scriptTextBox.SelectionStart;
        int selectionLength = scriptTextBox.SelectionLength;

        SendMessage(scriptTextBox.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

        scriptTextBox.SelectAll();
        scriptTextBox.SelectionColor = Color.FromArgb(200, 200, 200);

        foreach (string keyword in HighlightKeywords)
        {
            foreach (Match match in Regex.Matches(text, $"\\b{Regex.Escape(keyword)}\\b", RegexOptions.IgnoreCase))
            {
                scriptTextBox.Select(match.Index, match.Length);
                scriptTextBox.SelectionColor = Color.FromArgb(150, 220, 255);
            }
        }

        foreach (Match commentMatch in Regex.Matches(text, "#.*$", RegexOptions.Multiline))
        {
            scriptTextBox.Select(commentMatch.Index, commentMatch.Length);
            scriptTextBox.SelectionColor = Color.FromArgb(100, 255, 140);
        }

        scriptTextBox.SelectionStart = selectionStart;
        scriptTextBox.SelectionLength = selectionLength;
        scriptTextBox.SelectionColor = Color.FromArgb(200, 200, 200);

        SendMessage(scriptTextBox.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
        scriptTextBox.Invalidate();
    }

    private void ScriptTextBox_TextChanged(object? sender, EventArgs e)
    {
        UpdateLineNumbers();
        ApplySyntaxHighlighting();
    }

    private void ScriptTextBox_VScroll(object? sender, EventArgs e)
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

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    private const int WM_SETREDRAW = 0x000B;
    private const int EM_GETFIRSTVISIBLELINE = 0x00CE;

    private int GetFirstVisibleLine()
    {
        return SendMessage(scriptTextBox.Handle, EM_GETFIRSTVISIBLELINE, IntPtr.Zero, IntPtr.Zero);
    }
}