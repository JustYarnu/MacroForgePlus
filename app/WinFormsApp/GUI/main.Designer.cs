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
    private System.Windows.Forms.ToolStripMenuItem helpMenuItem;

    // Declaratie van de UI-elementen voor de main screen
    private System.Windows.Forms.Label welcomeTitleLabel;
    
    // Panels for the three-column layout
    private System.Windows.Forms.Panel mainContentPanel;
    private System.Windows.Forms.TableLayoutPanel contentTableLayout;
    
    // Getting Started Panel (Left)
    private System.Windows.Forms.Panel gettingStartedPanel;
    private System.Windows.Forms.Label gettingStartedTitleLabel;
    private System.Windows.Forms.Label gettingStartedDescriptionLabel;
    private System.Windows.Forms.Label gettingStartedShortcutLabel;
    private System.Windows.Forms.Button newScriptButton;
    
    // Open Macro Panel (Middle)
    private System.Windows.Forms.Panel openMacroPanel;
    private System.Windows.Forms.Label openMacroTitleLabel;
    private System.Windows.Forms.Label openMacroDescriptionLabel;
    private System.Windows.Forms.Label openMacroShortcutLabel;
    private System.Windows.Forms.Button openMacroButton;
    
    // Run Macro Panel (Right)
    private System.Windows.Forms.Panel runMacroPanel;
    private System.Windows.Forms.Label runMacroTitleLabel;
    private System.Windows.Forms.Label runMacroDescriptionLabel;
    private System.Windows.Forms.Label runMacroShortcutLabel;
    private System.Windows.Forms.Label runMacroInfoLabel;

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
        this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.welcomeTitleLabel = new System.Windows.Forms.Label();
        this.mainContentPanel = new System.Windows.Forms.Panel();
        this.contentTableLayout = new System.Windows.Forms.TableLayoutPanel();
        
        // Getting Started Panel components
        this.gettingStartedPanel = new System.Windows.Forms.Panel();
        this.gettingStartedTitleLabel = new System.Windows.Forms.Label();
        this.gettingStartedDescriptionLabel = new System.Windows.Forms.Label();
        this.gettingStartedShortcutLabel = new System.Windows.Forms.Label();
        this.newScriptButton = new System.Windows.Forms.Button();
        
        // Open Macro Panel components
        this.openMacroPanel = new System.Windows.Forms.Panel();
        this.openMacroTitleLabel = new System.Windows.Forms.Label();
        this.openMacroDescriptionLabel = new System.Windows.Forms.Label();
        this.openMacroShortcutLabel = new System.Windows.Forms.Label();
        this.openMacroButton = new System.Windows.Forms.Button();
        
        // Run Macro Panel components
        this.runMacroPanel = new System.Windows.Forms.Panel();
        this.runMacroTitleLabel = new System.Windows.Forms.Label();
        this.runMacroDescriptionLabel = new System.Windows.Forms.Label();
        this.runMacroShortcutLabel = new System.Windows.Forms.Label();
        this.runMacroInfoLabel = new System.Windows.Forms.Label();
        
        // Pauzeer de layout logica tijdens het opbouwen
        this.topMenu.SuspendLayout();
        this.mainContentPanel.SuspendLayout();
        this.contentTableLayout.SuspendLayout();
        this.gettingStartedPanel.SuspendLayout();
        this.openMacroPanel.SuspendLayout();
        this.runMacroPanel.SuspendLayout();
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
        this.topMenu.Size = new System.Drawing.Size(1100, 24);
        this.topMenu.TabIndex = 0;
        this.topMenu.Text = "topMenu";
        
        // 
        // fileMenuItem
        // 
        this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newScriptMenuItem,
            this.openMenuItem
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
        this.welcomeTitleLabel.Location = new System.Drawing.Point(350, 40);
        this.welcomeTitleLabel.Name = "welcomeTitleLabel";
        this.welcomeTitleLabel.Size = new System.Drawing.Size(400, 51);
        this.welcomeTitleLabel.TabIndex = 1;
        this.welcomeTitleLabel.Text = "Macro Forge+";
        this.welcomeTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // mainContentPanel
        // 
        this.mainContentPanel.Controls.Add(this.contentTableLayout);
        this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainContentPanel.Location = new System.Drawing.Point(0, 100);
        this.mainContentPanel.Name = "mainContentPanel";
        this.mainContentPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 20);
        this.mainContentPanel.Size = new System.Drawing.Size(1100, 450);
        this.mainContentPanel.TabIndex = 2;
        
        // 
        // contentTableLayout
        // 
        this.contentTableLayout.ColumnCount = 3;
        this.contentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
        this.contentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
        this.contentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
        this.contentTableLayout.Controls.Add(this.gettingStartedPanel, 0, 0);
        this.contentTableLayout.Controls.Add(this.openMacroPanel, 1, 0);
        this.contentTableLayout.Controls.Add(this.runMacroPanel, 2, 0);
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
        this.gettingStartedTitleLabel.AutoSize = true;
        this.gettingStartedTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.gettingStartedTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 200, 100);
        this.gettingStartedTitleLabel.Location = new System.Drawing.Point(50, 20);
        this.gettingStartedTitleLabel.Name = "gettingStartedTitleLabel";
        this.gettingStartedTitleLabel.Size = new System.Drawing.Size(240, 25);
        this.gettingStartedTitleLabel.TabIndex = 0;
        this.gettingStartedTitleLabel.Text = "Getting Started";
        this.gettingStartedTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // gettingStartedDescriptionLabel
        // 
        this.gettingStartedDescriptionLabel.AutoSize = true;
        this.gettingStartedDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.gettingStartedDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.gettingStartedDescriptionLabel.Location = new System.Drawing.Point(20, 60);
        this.gettingStartedDescriptionLabel.Name = "gettingStartedDescriptionLabel";
        this.gettingStartedDescriptionLabel.Size = new System.Drawing.Size(300, 120);
        this.gettingStartedDescriptionLabel.TabIndex = 1;
        this.gettingStartedDescriptionLabel.Text = "Create a new macro script\r\nto automate your workflow.\r\n\r\nWrite custom scripts\r\nwith keyboard and mouse\r\ncommands.\r\n\r\nSave and organize\r\nyour macros.";
        this.gettingStartedDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // gettingStartedShortcutLabel
        // 
        this.gettingStartedShortcutLabel.AutoSize = true;
        this.gettingStartedShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.gettingStartedShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.gettingStartedShortcutLabel.Location = new System.Drawing.Point(60, 340);
        this.gettingStartedShortcutLabel.Name = "gettingStartedShortcutLabel";
        this.gettingStartedShortcutLabel.Size = new System.Drawing.Size(220, 30);
        this.gettingStartedShortcutLabel.TabIndex = 2;
        this.gettingStartedShortcutLabel.Text = "💡 Shortcut: Ctrl+N\r\nto create new script";
        this.gettingStartedShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // newScriptButton
        // 
        this.newScriptButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.newScriptButton.FlatAppearance.BorderSize = 0;
        this.newScriptButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.newScriptButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.newScriptButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.newScriptButton.ForeColor = System.Drawing.Color.White;
        this.newScriptButton.Location = new System.Drawing.Point(80, 260);
        this.newScriptButton.Name = "newScriptButton";
        this.newScriptButton.Size = new System.Drawing.Size(180, 40);
        this.newScriptButton.TabIndex = 3;
        this.newScriptButton.Text = "New Script";
        this.newScriptButton.UseVisualStyleBackColor = false;
        
        // 
        // Open Macro Panel (Middle)
        // 
        this.openMacroPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.openMacroPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.openMacroPanel.Controls.Add(this.openMacroTitleLabel);
        this.openMacroPanel.Controls.Add(this.openMacroDescriptionLabel);
        this.openMacroPanel.Controls.Add(this.openMacroShortcutLabel);
        this.openMacroPanel.Controls.Add(this.openMacroButton);
        this.openMacroPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.openMacroPanel.Location = new System.Drawing.Point(356, 3);
        this.openMacroPanel.Name = "openMacroPanel";
        this.openMacroPanel.Size = new System.Drawing.Size(348, 414);
        this.openMacroPanel.TabIndex = 1;
        
        // 
        // openMacroTitleLabel
        // 
        this.openMacroTitleLabel.AutoSize = true;
        this.openMacroTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.openMacroTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 150, 250);
        this.openMacroTitleLabel.Location = new System.Drawing.Point(70, 20);
        this.openMacroTitleLabel.Name = "openMacroTitleLabel";
        this.openMacroTitleLabel.Size = new System.Drawing.Size(210, 25);
        this.openMacroTitleLabel.TabIndex = 0;
        this.openMacroTitleLabel.Text = "Open a Macro";
        this.openMacroTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // openMacroDescriptionLabel
        // 
        this.openMacroDescriptionLabel.AutoSize = true;
        this.openMacroDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.openMacroDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.openMacroDescriptionLabel.Location = new System.Drawing.Point(20, 60);
        this.openMacroDescriptionLabel.Name = "openMacroDescriptionLabel";
        this.openMacroDescriptionLabel.Size = new System.Drawing.Size(300, 120);
        this.openMacroDescriptionLabel.TabIndex = 1;
        this.openMacroDescriptionLabel.Text = "Load an existing macro\r\nfrom your files.\r\n\r\nBrowse through your\r\nsaved macro scripts.\r\n\r\nSelect and open\r\nany .macro file.";
        this.openMacroDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // openMacroShortcutLabel
        // 
        this.openMacroShortcutLabel.AutoSize = true;
        this.openMacroShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.openMacroShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.openMacroShortcutLabel.Location = new System.Drawing.Point(60, 340);
        this.openMacroShortcutLabel.Name = "openMacroShortcutLabel";
        this.openMacroShortcutLabel.Size = new System.Drawing.Size(220, 30);
        this.openMacroShortcutLabel.TabIndex = 2;
        this.openMacroShortcutLabel.Text = "💡 Shortcut: Ctrl+O\r\nto open macro file";
        this.openMacroShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // openMacroButton
        // 
        this.openMacroButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.openMacroButton.FlatAppearance.BorderSize = 0;
        this.openMacroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.openMacroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.openMacroButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.openMacroButton.ForeColor = System.Drawing.Color.White;
        this.openMacroButton.Location = new System.Drawing.Point(80, 260);
        this.openMacroButton.Name = "openMacroButton";
        this.openMacroButton.Size = new System.Drawing.Size(180, 40);
        this.openMacroButton.TabIndex = 3;
        this.openMacroButton.Text = "Open Macro";
        this.openMacroButton.UseVisualStyleBackColor = false;
        
        // 
        // Run Macro Panel (Right)
        // 
        this.runMacroPanel.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.runMacroPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.runMacroPanel.Controls.Add(this.runMacroTitleLabel);
        this.runMacroPanel.Controls.Add(this.runMacroDescriptionLabel);
        this.runMacroPanel.Controls.Add(this.runMacroShortcutLabel);
        this.runMacroPanel.Controls.Add(this.runMacroInfoLabel);
        this.runMacroPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.runMacroPanel.Location = new System.Drawing.Point(710, 3);
        this.runMacroPanel.Name = "runMacroPanel";
        this.runMacroPanel.Size = new System.Drawing.Size(347, 414);
        this.runMacroPanel.TabIndex = 2;
        
        // 
        // runMacroTitleLabel
        // 
        this.runMacroTitleLabel.AutoSize = true;
        this.runMacroTitleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
        this.runMacroTitleLabel.ForeColor = System.Drawing.Color.FromArgb(250, 150, 100);
        this.runMacroTitleLabel.Location = new System.Drawing.Point(80, 20);
        this.runMacroTitleLabel.Name = "runMacroTitleLabel";
        this.runMacroTitleLabel.Size = new System.Drawing.Size(190, 25);
        this.runMacroTitleLabel.TabIndex = 0;
        this.runMacroTitleLabel.Text = "Run a Macro";
        this.runMacroTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // runMacroDescriptionLabel
        // 
        this.runMacroDescriptionLabel.AutoSize = true;
        this.runMacroDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.runMacroDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.runMacroDescriptionLabel.Location = new System.Drawing.Point(20, 60);
        this.runMacroDescriptionLabel.Name = "runMacroDescriptionLabel";
        this.runMacroDescriptionLabel.Size = new System.Drawing.Size(300, 120);
        this.runMacroDescriptionLabel.TabIndex = 1;
        this.runMacroDescriptionLabel.Text = "Execute your macro\r\nscripts with ease.\r\n\r\nSelect a macro and\r\nhit the run button.\r\n\r\nMonitor execution\r\nin real-time.";
        this.runMacroDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // runMacroShortcutLabel
        // 
        this.runMacroShortcutLabel.AutoSize = true;
        this.runMacroShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.runMacroShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.runMacroShortcutLabel.Location = new System.Drawing.Point(60, 340);
        this.runMacroShortcutLabel.Name = "runMacroShortcutLabel";
        this.runMacroShortcutLabel.Size = new System.Drawing.Size(220, 30);
        this.runMacroShortcutLabel.TabIndex = 2;
        this.runMacroShortcutLabel.Text = "💡 Shortcut: F5\r\nto run selected macro";
        this.runMacroShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // runMacroInfoLabel
        // 
        this.runMacroInfoLabel.AutoSize = true;
        this.runMacroInfoLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        this.runMacroInfoLabel.ForeColor = System.Drawing.Color.FromArgb(200, 100, 50);
        this.runMacroInfoLabel.Location = new System.Drawing.Point(80, 260);
        this.runMacroInfoLabel.Name = "runMacroInfoLabel";
        this.runMacroInfoLabel.Size = new System.Drawing.Size(180, 40);
        this.runMacroInfoLabel.TabIndex = 3;
        this.runMacroInfoLabel.Text = "Open a macro first\r\nto run it";
        this.runMacroInfoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // Main (Het hoofdvenster)
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25); // Sleek bijna-zwart achtergrond
        this.ClientSize = new System.Drawing.Size(1100, 550);
        this.Controls.Add(this.mainContentPanel);
        this.Controls.Add(this.welcomeTitleLabel);
        this.Controls.Add(this.topMenu);
        this.MainMenuStrip = this.topMenu;
        this.Name = "Main";
        this.Text = "Bidenator";
        this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(Application.StartupPath, "Resources", "Favico.ico"));
        
        // Event handlers
        this.newScriptButton.Click += NewScriptButton_Click;
        this.openMacroButton.Click += OpenMacroButton_Click;
        this.newScriptMenuItem.Click += NewScriptMenuItem_Click;
        this.openMenuItem.Click += OpenMenuItem_Click;
        
        // Hervat de layout logica
        this.topMenu.ResumeLayout(false);
        this.topMenu.PerformLayout();
        this.mainContentPanel.ResumeLayout(false);
        this.contentTableLayout.ResumeLayout(false);
        this.gettingStartedPanel.ResumeLayout(false);
        this.gettingStartedPanel.PerformLayout();
        this.openMacroPanel.ResumeLayout(false);
        this.openMacroPanel.PerformLayout();
        this.runMacroPanel.ResumeLayout(false);
        this.runMacroPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}