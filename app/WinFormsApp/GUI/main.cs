using System;
using WinFormsApp.GUI;

namespace WinFormsApp;

public partial class Main : Form
{
    public Main()
    {
        InitializeComponent();
        newScriptMenuItem.Click += NewScriptMenuItem_Click;
    }

    private void NewScriptMenuItem_Click(object? sender, EventArgs e)
    {
        var editor = new ScriptEditor();
        editor.Show();
    }
}
