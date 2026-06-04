namespace WinFormsApp;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    // Declaratie van de UI-elementen voor het menu
    private System.Windows.Forms.MenuStrip topMenu;
    private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newScriptMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpMenuItem;

    // Declaratie van de UI-elementen voor de main screen
    private System.Windows.Forms.Label welcomeTitleLabel;
    private System.Windows.Forms.Panel infoPanel;
    private System.Windows.Forms.Label infoTitleLabel;
    private System.Windows.Forms.Label infoDescriptionLabel;
    private System.Windows.Forms.Label shortcutHintLabel;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        // Instantieer de componenten
        this.topMenu = new System.Windows.Forms.MenuStrip();
        this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.newScriptMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.welcomeTitleLabel = new System.Windows.Forms.Label();
        this.infoPanel = new System.Windows.Forms.Panel();
        this.infoTitleLabel = new System.Windows.Forms.Label();
        this.infoDescriptionLabel = new System.Windows.Forms.Label();
        this.shortcutHintLabel = new System.Windows.Forms.Label();
        
        // Pauzeer de layout logica tijdens het opbouwen
        this.topMenu.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // topMenu (Het menu lint bovenaan)
        // 
        this.topMenu.BackColor = System.Drawing.Color.FromArgb(45, 45, 48); // Iets lichter grijs voor het lint
        this.topMenu.ForeColor = System.Drawing.Color.White; // Witte tekst voor leesbaarheid
        this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.helpMenuItem
        });
        this.topMenu.Location = new System.Drawing.Point(0, 0);
        this.topMenu.Name = "topMenu";
        this.topMenu.Size = new System.Drawing.Size(800, 24);
        this.topMenu.TabIndex = 0;
        this.topMenu.Text = "topMenu";
        
        // 
        // fileMenuItem
        // 
        this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptMenuItem
        });
        this.fileMenuItem.Name = "fileMenuItem";
        this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
        this.fileMenuItem.Text = "File";
        
        // 
        // newScriptMenuItem
        // 
        this.newScriptMenuItem.Name = "newScriptMenuItem";
        this.newScriptMenuItem.Size = new System.Drawing.Size(180, 22);
        this.newScriptMenuItem.Text = "New Script";
        this.newScriptMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
        
        // 
        // helpMenuItem
        // 
        this.helpMenuItem.Name = "helpMenuItem";
        this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
        this.helpMenuItem.Text = "Help";
        
        // 
        // welcomeTitleLabel
        // 
        this.welcomeTitleLabel.AutoSize = true;
        this.welcomeTitleLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
        this.welcomeTitleLabel.ForeColor = System.Drawing.Color.White;
        this.welcomeTitleLabel.Location = new System.Drawing.Point(250, 80);
        this.welcomeTitleLabel.Name = "welcomeTitleLabel";
        this.welcomeTitleLabel.Size = new System.Drawing.Size(300, 51);
        this.welcomeTitleLabel.TabIndex = 1;
        this.welcomeTitleLabel.Text = "Macro Forge+";
        this.welcomeTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // infoPanel
        // 
        this.infoPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.infoPanel.Controls.Add(this.infoTitleLabel);
        this.infoPanel.Controls.Add(this.infoDescriptionLabel);
        this.infoPanel.Controls.Add(this.shortcutHintLabel);
        this.infoPanel.Location = new System.Drawing.Point(200, 170);
        this.infoPanel.Name = "infoPanel";
        this.infoPanel.Size = new System.Drawing.Size(400, 220);
        this.infoPanel.TabIndex = 2;
        
        // 
        // infoTitleLabel
        // 
        this.infoTitleLabel.AutoSize = true;
        this.infoTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.infoTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 200, 100);
        this.infoTitleLabel.Location = new System.Drawing.Point(130, 15);
        this.infoTitleLabel.Name = "infoTitleLabel";
        this.infoTitleLabel.Size = new System.Drawing.Size(140, 25);
        this.infoTitleLabel.TabIndex = 3;
        this.infoTitleLabel.Text = "Getting Started";
        this.infoTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // infoDescriptionLabel
        // 
        this.infoDescriptionLabel.AutoSize = true;
        this.infoDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.infoDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.infoDescriptionLabel.Location = new System.Drawing.Point(40, 55);
        this.infoDescriptionLabel.Name = "infoDescriptionLabel";
        this.infoDescriptionLabel.Size = new System.Drawing.Size(320, 80);
        this.infoDescriptionLabel.TabIndex = 4;
        this.infoDescriptionLabel.Text = "Create a new script to get started.\r\n\r\nGo to File → New Script\r\nor press Ctrl+N";
        this.infoDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // shortcutHintLabel
        // 
        this.shortcutHintLabel.AutoSize = true;
        this.shortcutHintLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.shortcutHintLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.shortcutHintLabel.Location = new System.Drawing.Point(90, 175);
        this.shortcutHintLabel.Name = "shortcutHintLabel";
        this.shortcutHintLabel.Size = new System.Drawing.Size(220, 30);
        this.shortcutHintLabel.TabIndex = 5;
        this.shortcutHintLabel.Text = "💡 Tip: Use keyboard\r\nshortcut Ctrl+N";
        this.shortcutHintLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // Main (Het hoofdvenster)
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25); // Sleek bijna-zwart achtergrond
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.infoPanel);
        this.Controls.Add(this.welcomeTitleLabel);
        this.Controls.Add(this.topMenu);
        this.MainMenuStrip = this.topMenu;
        this.Name = "Main";
        this.Text = "Bidenator";
        this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(Application.StartupPath, "Resources", "Favico.ico"));
        
        // Hervat de layout logica
        this.topMenu.ResumeLayout(false);
        this.topMenu.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}