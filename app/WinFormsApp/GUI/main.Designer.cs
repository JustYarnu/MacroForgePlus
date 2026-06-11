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
    private System.Windows.Forms.ToolStripMenuItem openMenuItem;
    private System.Windows.Forms.ToolStripMenuItem runMacroMenuItem;
    private System.Windows.Forms.ToolStripMenuItem keybindsMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpMenuItem;

    // Declaratie van de UI-elementen voor de main screen
    private System.Windows.Forms.Label mainTitleLabel;
    
    // Panels for the one-card layout
    private System.Windows.Forms.Panel mainContentPanel;
    private System.Windows.Forms.TableLayoutPanel contentTableLayout;
    
    // Welcome Card Panel
    private System.Windows.Forms.Panel gettingStartedPanel;
    private System.Windows.Forms.Label gettingStartedTitleLabel;
    private System.Windows.Forms.Label gettingStartedDescriptionLabel;
    private System.Windows.Forms.Label gettingStartedShortcutLabel;
    private System.Windows.Forms.Button newScriptButton;

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
        this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.runMacroMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.keybindsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.mainTitleLabel = new System.Windows.Forms.Label();
        this.mainContentPanel = new System.Windows.Forms.Panel();
        this.contentTableLayout = new System.Windows.Forms.TableLayoutPanel();
        
        // Getting Started Panel components
        this.gettingStartedPanel = new System.Windows.Forms.Panel();
        this.gettingStartedTitleLabel = new System.Windows.Forms.Label();
        this.gettingStartedDescriptionLabel = new System.Windows.Forms.Label();
        this.gettingStartedShortcutLabel = new System.Windows.Forms.Label();
        this.newScriptButton = new System.Windows.Forms.Button();
        
        // Pauzeer de layout logica tijdens het opbouwen
        this.topMenu.SuspendLayout();
        this.mainContentPanel.SuspendLayout();
        this.contentTableLayout.SuspendLayout();
        this.gettingStartedPanel.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // topMenu (Het menu lint bovenaan)
        // 
        this.topMenu.BackColor = System.Drawing.Color.FromArgb(45, 45, 48); // Iets lichter grijs voor het lint
        this.topMenu.ForeColor = System.Drawing.Color.White; // Witte tekst voor leesbaarheid
        this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.keybindsMenuItem,
            this.helpMenuItem
        });
        this.topMenu.Location = new System.Drawing.Point(0, 0);
        this.topMenu.Name = "topMenu";
        this.topMenu.Size = new System.Drawing.Size(1100, 24);
        this.topMenu.TabIndex = 0;
        this.topMenu.Text = "topMenu";
        
        // 
        // fileMenuItem
        // 
        this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptMenuItem,
            this.openMenuItem,
            this.runMacroMenuItem
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
        // openMenuItem
        // 
        this.openMenuItem.Name = "openMenuItem";
        this.openMenuItem.Size = new System.Drawing.Size(180, 22);
        this.openMenuItem.Text = "Open Macro";
        this.openMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
        
        // 
        // runMacroMenuItem
        // 
        this.runMacroMenuItem.Name = "runMacroMenuItem";
        this.runMacroMenuItem.Size = new System.Drawing.Size(180, 22);
        this.runMacroMenuItem.Text = "Run Macro";
        this.runMacroMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
        
        // 
        // keybindsMenuItem
        // 
        this.keybindsMenuItem.Name = "keybindsMenuItem";
        this.keybindsMenuItem.Size = new System.Drawing.Size(67, 20);
        this.keybindsMenuItem.Text = "Keybinds";
        this.keybindsMenuItem.Click += new System.EventHandler(this.KeybindsMenuItem_Click);
        
        // 
        // helpMenuItem
        // 
        this.helpMenuItem.Name = "helpMenuItem";
        this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
        this.helpMenuItem.Text = "Help";
        this.helpMenuItem.Click += new System.EventHandler(this.HelpMenuItem_Click);
        
        // 
        // mainTitleLabel
        // 
        this.mainTitleLabel.AutoSize = false;
        this.mainTitleLabel.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold);
        this.mainTitleLabel.ForeColor = System.Drawing.Color.White;
        this.mainTitleLabel.Location = new System.Drawing.Point(0, 35);
        this.mainTitleLabel.Name = "mainTitleLabel";
        this.mainTitleLabel.Size = new System.Drawing.Size(1100, 60);
        this.mainTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; // Add this line
        this.mainTitleLabel.TabIndex = 1;
        this.mainTitleLabel.Text = "Macro Forge+";
        this.mainTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // mainContentPanel
        // 
        this.mainContentPanel.Controls.Add(this.contentTableLayout);
        this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainContentPanel.Location = new System.Drawing.Point(0, 110);
        this.mainContentPanel.Name = "mainContentPanel";
        this.mainContentPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
        this.mainContentPanel.Size = new System.Drawing.Size(1100, 450);
        this.mainContentPanel.TabIndex = 2;
        
        // 
        // contentTableLayout
        // 
        this.contentTableLayout.ColumnCount = 1;
        this.contentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.contentTableLayout.Controls.Add(this.gettingStartedPanel, 0, 0);
        this.contentTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        this.contentTableLayout.Location = new System.Drawing.Point(20, 10);
        this.contentTableLayout.Name = "contentTableLayout";
        this.contentTableLayout.RowCount = 1;
        this.contentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.contentTableLayout.Size = new System.Drawing.Size(1060, 420);
        this.contentTableLayout.TabIndex = 0;
        
        // 
        // Getting Started Panel (Left)
        // 
        this.gettingStartedPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.gettingStartedPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.gettingStartedPanel.Controls.Add(this.gettingStartedTitleLabel);
        this.gettingStartedPanel.Controls.Add(this.gettingStartedDescriptionLabel);
        this.gettingStartedPanel.Controls.Add(this.gettingStartedShortcutLabel);
        this.gettingStartedPanel.Controls.Add(this.newScriptButton);
        this.gettingStartedPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gettingStartedPanel.Location = new System.Drawing.Point(3, 3);
        this.gettingStartedPanel.Name = "gettingStartedPanel";
        this.gettingStartedPanel.Size = new System.Drawing.Size(347, 414);
        this.gettingStartedPanel.TabIndex = 0;
        
        // 
        // gettingStartedTitleLabel
        // 
        this.gettingStartedTitleLabel.AutoSize = false; // Changed to false
        this.gettingStartedTitleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.gettingStartedTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 200, 100);
        this.gettingStartedTitleLabel.Location = new System.Drawing.Point(0, 25); // Adjusted X
        this.gettingStartedTitleLabel.Name = "gettingStartedTitleLabel";
        this.gettingStartedTitleLabel.Size = new System.Drawing.Size(345, 30); // Stretched width
        this.gettingStartedTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; // Added anchor
        this.gettingStartedTitleLabel.TabIndex = 0;
        this.gettingStartedTitleLabel.Text = "Open Script Editor";
        this.gettingStartedTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // gettingStartedDescriptionLabel
        // 
        this.gettingStartedDescriptionLabel.AutoSize = false;
        this.gettingStartedDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.gettingStartedDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.gettingStartedDescriptionLabel.Location = new System.Drawing.Point(20, 70); // Adjusted X
        this.gettingStartedDescriptionLabel.Name = "gettingStartedDescriptionLabel";
        this.gettingStartedDescriptionLabel.Size = new System.Drawing.Size(305, 160); // Stretched width
        this.gettingStartedDescriptionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; // Added anchor
        this.gettingStartedDescriptionLabel.TabIndex = 1;
        this.gettingStartedDescriptionLabel.Text = "Open the script editor to create or edit macros.\r\n\r\nStart with a blank script or load an existing macro once the editor opens.\r\n\r\nYou can also select the Keybinds option in the menubar to customize your shortcuts.";
        this.gettingStartedDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // gettingStartedShortcutLabel
        // 
        this.gettingStartedShortcutLabel.AutoSize = false; // Changed to false
        this.gettingStartedShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
        this.gettingStartedShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.gettingStartedShortcutLabel.Location = new System.Drawing.Point(0, 350); // Adjusted X
        this.gettingStartedShortcutLabel.Name = "gettingStartedShortcutLabel";
        this.gettingStartedShortcutLabel.Size = new System.Drawing.Size(345, 40); // Stretched width
        this.gettingStartedShortcutLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; // Added anchor
        this.gettingStartedShortcutLabel.TabIndex = 2;
        this.gettingStartedShortcutLabel.Text = "Press Ctrl+N to open the script editor.";
        this.gettingStartedShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // newScriptButton
        // 
        this.newScriptButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.newScriptButton.FlatAppearance.BorderSize = 0;
        this.newScriptButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.newScriptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.newScriptButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.newScriptButton.ForeColor = System.Drawing.Color.White;
        this.newScriptButton.Location = new System.Drawing.Point(83, 270); // Adjusted X to 83 for perfect center
        this.newScriptButton.Name = "newScriptButton";
        this.newScriptButton.Size = new System.Drawing.Size(180, 45);
        this.newScriptButton.Anchor = System.Windows.Forms.AnchorStyles.Top; // Added Top Anchor
        this.newScriptButton.TabIndex = 3;
        this.newScriptButton.Text = "Open Editor";
        this.newScriptButton.UseVisualStyleBackColor = false;        
        
        // 
        // Main (Het hoofdvenster)
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25); // Sleek bijna-zwart achtergrond
        this.ClientSize = new System.Drawing.Size(1100, 550);
        this.Controls.Add(this.mainContentPanel);
        this.Controls.Add(this.mainTitleLabel);
        this.Controls.Add(this.topMenu);
        this.MainMenuStrip = this.topMenu;
        this.Name = "Main";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        this.Text = "Macro Forge Plus";
        this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(Application.StartupPath, "Resources", "Favico.ico"));
        
        // Event handlers
        this.newScriptButton.Click += NewScriptButton_Click;
        this.newScriptMenuItem.Click += NewScriptMenuItem_Click;
        this.openMenuItem.Click += OpenMenuItem_Click;
        this.runMacroMenuItem.Click += RunMacroMenuItem_Click;
        
        // Hervat de layout logica
        this.topMenu.ResumeLayout(false);
        this.topMenu.PerformLayout();
        this.mainContentPanel.ResumeLayout(false);
        this.contentTableLayout.ResumeLayout(false);
        this.gettingStartedPanel.ResumeLayout(false);
        this.gettingStartedPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}