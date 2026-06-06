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
        this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
        this.editToolStripMenuItem});
        this.menuStrip.Location = new System.Drawing.Point(0, 0);
        this.menuStrip.Name = "menuStrip";
        this.menuStrip.Size = new System.Drawing.Size(784, 24);
        this.menuStrip.TabIndex = 4;
        this.menuStrip.Text = "menuStrip";
        // 
        // fileToolStripMenuItem
        // 
        this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.saveToolStripMenuItem,
        this.toolStripSeparator1,
        this.closeToolStripMenuItem});
        this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
        this.fileToolStripMenuItem.Text = "File";
        // 
        // saveToolStripMenuItem
        // 
        this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
        this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
        this.saveToolStripMenuItem.Text = "Save";
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
        this.redoToolStripMenuItem});
        this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
        this.editToolStripMenuItem.Name = "editToolStripMenuItem";
        this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
        this.editToolStripMenuItem.Text = "Edit";
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
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
}