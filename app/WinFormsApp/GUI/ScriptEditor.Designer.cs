using System.Windows.Forms.Integration;

namespace WinFormsApp.GUI;

partial class ScriptEditor
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.elementHost1 = new ElementHost();
        this.editorPanel = new System.Windows.Forms.Panel();
        this.menuStrip = new System.Windows.Forms.MenuStrip();
        this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.newScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.openScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.startRecordingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.stopRecordingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.stopExecutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip.SuspendLayout();
        this.editorPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // elementHost1
        // 
        this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.elementHost1.Location = new System.Drawing.Point(0, 0);
        this.elementHost1.Name = "elementHost1";
        this.elementHost1.Size = new System.Drawing.Size(784, 406);
        this.elementHost1.TabIndex = 0;
        this.elementHost1.Text = "elementHost1";
        this.elementHost1.Child = null;
        // 
        // editorPanel
        // 
        this.editorPanel.Controls.Add(this.elementHost1);
        this.editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.editorPanel.Location = new System.Drawing.Point(0, 24);
        this.editorPanel.Name = "editorPanel";
        this.editorPanel.Size = new System.Drawing.Size(784, 406);
        this.editorPanel.TabIndex = 3;
        // 
        // menuStrip
        // 
        this.menuStrip.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.fileToolStripMenuItem,
        this.editToolStripMenuItem,
        this.recordToolStripMenuItem,
        this.executeToolStripMenuItem,
        this.stopExecutionToolStripMenuItem});
        this.menuStrip.Location = new System.Drawing.Point(0, 0);
        this.menuStrip.Name = "menuStrip";
        this.menuStrip.Size = new System.Drawing.Size(784, 24);
        this.menuStrip.TabIndex = 4;
        this.menuStrip.Text = "menuStrip";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.newScriptMenuItem,
        this.openScriptMenuItem,
        this.saveToolStripMenuItem,
        this.saveAsToolStripMenuItem,
        this.toolStripSeparator1,
        this.closeToolStripMenuItem});
        this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
        this.fileToolStripMenuItem.Text = "File";
        // 
        // newScriptMenuItem
        // 
        this.newScriptMenuItem.ForeColor = System.Drawing.Color.Black;
        this.newScriptMenuItem.Name = "newScriptMenuItem";
        this.newScriptMenuItem.Size = new System.Drawing.Size(180, 22);
        this.newScriptMenuItem.Text = "New Script";
        this.newScriptMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
        // 
        // openScriptMenuItem
        // 
        this.openScriptMenuItem.ForeColor = System.Drawing.Color.Black;
        this.openScriptMenuItem.Name = "openScriptMenuItem";
        this.openScriptMenuItem.Size = new System.Drawing.Size(180, 22);
        this.openScriptMenuItem.Text = "Open Script";
        this.openScriptMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
        // 
        // saveToolStripMenuItem
        // 
        this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        this.saveToolStripMenuItem.Text = "Save";
        this.saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
        // 
        // saveAsToolStripMenuItem
        // 
        this.saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        this.saveAsToolStripMenuItem.Text = "Save As";
        this.saveAsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.S;
        // 
        // toolStripSeparator2
        // 
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new System.Drawing.Size(107, 6);
        
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
        // 
        // closeToolStripMenuItem
        // 
        this.closeToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
        this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
        this.closeToolStripMenuItem.Text = "Close";
        this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
        // 
        // editToolStripMenuItem
        // 
        this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.undoToolStripMenuItem,
        this.redoToolStripMenuItem,
        this.toolStripSeparator2,
        this.findToolStripMenuItem});
        this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
        this.editToolStripMenuItem.Text = "Edit";
        // 
        // recordToolStripMenuItem
        // 
        this.recordToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.startRecordingMenuItem,
        this.stopRecordingMenuItem});
        this.recordToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
        this.recordToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
        this.recordToolStripMenuItem.Text = "Record";
        // 
        // startRecordingMenuItem
        // 
        this.startRecordingMenuItem.ForeColor = System.Drawing.Color.Black;
        this.startRecordingMenuItem.Name = "startRecordingMenuItem";
        this.startRecordingMenuItem.Size = new System.Drawing.Size(180, 22);
        this.startRecordingMenuItem.Text = "Start Recording";
        this.startRecordingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
        // 
        // stopRecordingMenuItem
        // 
        this.stopRecordingMenuItem.ForeColor = System.Drawing.Color.Black;
        this.stopRecordingMenuItem.Name = "stopRecordingMenuItem";
        this.stopRecordingMenuItem.Size = new System.Drawing.Size(180, 22);
        this.stopRecordingMenuItem.Text = "Stop Recording";
        this.stopRecordingMenuItem.Enabled = false;
        this.stopRecordingMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
        // 
        // executeToolStripMenuItem
        // 
        this.executeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
        this.executeToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
        this.executeToolStripMenuItem.Text = "Execute";
        this.executeToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
        // 
        // stopExecutionToolStripMenuItem
        // 
        this.stopExecutionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.stopExecutionToolStripMenuItem.Name = "stopExecutionToolStripMenuItem";
        this.stopExecutionToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
        this.stopExecutionToolStripMenuItem.Text = "Stop";
        this.stopExecutionToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5;
        // 
        // undoToolStripMenuItem
        // 
        this.undoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
        this.undoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
        this.undoToolStripMenuItem.Text = "Undo";
        this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
        // 
        // redoToolStripMenuItem
        // 
        this.redoToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
        this.redoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
        this.redoToolStripMenuItem.Text = "Redo";
        this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
        
        // 
        // findToolStripMenuItem
        // 
        this.findToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.findToolStripMenuItem.Name = "findToolStripMenuItem";
        this.findToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
        this.findToolStripMenuItem.Text = "Find";
        this.findToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F;
        this.findToolStripMenuItem.Click += new System.EventHandler(this.FindToolStripMenuItem_Click);
        // 
        // ScriptEditor
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
        this.ClientSize = new System.Drawing.Size(784, 430);
        this.Controls.Add(this.editorPanel);
        this.Controls.Add(this.menuStrip);
        this.MainMenuStrip = this.menuStrip;
        this.Name = "ScriptEditor";
        this.Text = "Script Editor - Untitled";
        this.menuStrip.ResumeLayout(false);
        this.menuStrip.PerformLayout();
        this.editorPanel.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private ElementHost elementHost1;
    private System.Windows.Forms.Panel editorPanel;
    private System.Windows.Forms.MenuStrip menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openScriptMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem startRecordingMenuItem;
    private System.Windows.Forms.ToolStripMenuItem stopRecordingMenuItem;
    private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem stopExecutionToolStripMenuItem;

    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
}
