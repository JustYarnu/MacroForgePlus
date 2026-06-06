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
    private System.Windows.Forms.ToolStripMenuItem helpMenuItem;

    // Declaratie van de UI-elementen voor de main screen
    private System.Windows.Forms.Label mainTitleLabel;
    
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
    private System.Windows.Forms.Button runMacroButton;

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
        this.runMacroButton = new System.Windows.Forms.Button();
        
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
        // helpMenuItem
        // 
        this.helpMenuItem.Name = "helpMenuItem";
        this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
        this.helpMenuItem.Text = "Help";
        
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
        this.gettingStartedTitleLabel.AutoSize = false; // Changed to false
        this.gettingStartedTitleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.gettingStartedTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 200, 100);
        this.gettingStartedTitleLabel.Location = new System.Drawing.Point(0, 25); // Adjusted X
        this.gettingStartedTitleLabel.Name = "gettingStartedTitleLabel";
        this.gettingStartedTitleLabel.Size = new System.Drawing.Size(345, 30); // Stretched width
        this.gettingStartedTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; // Added anchor
        this.gettingStartedTitleLabel.TabIndex = 0;
        this.gettingStartedTitleLabel.Text = "Getting Started";
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
        this.gettingStartedDescriptionLabel.Text = "Create a new macro script to automate keyboard actions, mouse controls, and timed sequences.\r\n\r\nOpen the editor and build workflows for repetitive tasks with precision.";
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
        this.gettingStartedShortcutLabel.Text = "Press Ctrl+N to create a new macro script.";
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
        this.openMacroTitleLabel.AutoSize = false;
        this.openMacroTitleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.openMacroTitleLabel.ForeColor = System.Drawing.Color.FromArgb(100, 150, 250);
        this.openMacroTitleLabel.Location = new System.Drawing.Point(0, 25);
        this.openMacroTitleLabel.Name = "openMacroTitleLabel";
        this.openMacroTitleLabel.Size = new System.Drawing.Size(345, 30);
        this.openMacroTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.openMacroTitleLabel.TabIndex = 0;
        this.openMacroTitleLabel.Text = "Open a Macro";
        this.openMacroTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // openMacroDescriptionLabel
        // 
        this.openMacroDescriptionLabel.AutoSize = false;
        this.openMacroDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.openMacroDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.openMacroDescriptionLabel.Location = new System.Drawing.Point(20, 70);
        this.openMacroDescriptionLabel.Name = "openMacroDescriptionLabel";
        this.openMacroDescriptionLabel.Size = new System.Drawing.Size(305, 160);
        this.openMacroDescriptionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.openMacroDescriptionLabel.TabIndex = 1;
        this.openMacroDescriptionLabel.Text = "Browse and open an existing macro file from disk.\r\n\r\nUse this panel to load saved automations and continue your workflow.";
        this.openMacroDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // openMacroShortcutLabel
        // 
        this.openMacroShortcutLabel.AutoSize = false;
        this.openMacroShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
        this.openMacroShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.openMacroShortcutLabel.Location = new System.Drawing.Point(0, 350);
        this.openMacroShortcutLabel.Name = "openMacroShortcutLabel";
        this.openMacroShortcutLabel.Size = new System.Drawing.Size(345, 40);
        this.openMacroShortcutLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.openMacroShortcutLabel.TabIndex = 2;
        this.openMacroShortcutLabel.Text = "Press Ctrl+O to open a saved macro file.";
        this.openMacroShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // openMacroButton
        // 
        this.openMacroButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.openMacroButton.FlatAppearance.BorderSize = 0;
        this.openMacroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.openMacroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.openMacroButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.openMacroButton.ForeColor = System.Drawing.Color.White;
        this.openMacroButton.Location = new System.Drawing.Point(83, 270);
        this.openMacroButton.Name = "openMacroButton";
        this.openMacroButton.Size = new System.Drawing.Size(180, 45);
        this.openMacroButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
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
        this.runMacroPanel.Controls.Add(this.runMacroButton);
        this.runMacroPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.runMacroPanel.Location = new System.Drawing.Point(710, 3);
        this.runMacroPanel.Name = "runMacroPanel";
        this.runMacroPanel.Size = new System.Drawing.Size(347, 414);
        this.runMacroPanel.TabIndex = 2;
        
        // 
        // runMacroTitleLabel
        // 
        this.runMacroTitleLabel.AutoSize = false;
        this.runMacroTitleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.runMacroTitleLabel.ForeColor = System.Drawing.Color.FromArgb(250, 150, 100);
        this.runMacroTitleLabel.Location = new System.Drawing.Point(0, 25);
        this.runMacroTitleLabel.Name = "runMacroTitleLabel";
        this.runMacroTitleLabel.Size = new System.Drawing.Size(345, 30);
        this.runMacroTitleLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.runMacroTitleLabel.TabIndex = 0;
        this.runMacroTitleLabel.Text = "Run a Macro";
        this.runMacroTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // runMacroDescriptionLabel
        // 
        this.runMacroDescriptionLabel.AutoSize = false;
        this.runMacroDescriptionLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.runMacroDescriptionLabel.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.runMacroDescriptionLabel.Location = new System.Drawing.Point(20, 70);
        this.runMacroDescriptionLabel.Name = "runMacroDescriptionLabel";
        this.runMacroDescriptionLabel.Size = new System.Drawing.Size(305, 160);
        this.runMacroDescriptionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.runMacroDescriptionLabel.TabIndex = 1;
        this.runMacroDescriptionLabel.Text = "Run the currently loaded macro to execute automated actions.\r\n\r\nUse this panel after opening a script to start playback and validate your workflow.";
        this.runMacroDescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        
        // 
        // runMacroShortcutLabel
        // 
        this.runMacroShortcutLabel.AutoSize = false;
        this.runMacroShortcutLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
        this.runMacroShortcutLabel.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
        this.runMacroShortcutLabel.Location = new System.Drawing.Point(0, 350);
        this.runMacroShortcutLabel.Name = "runMacroShortcutLabel";
        this.runMacroShortcutLabel.Size = new System.Drawing.Size(345, 40);
        this.runMacroShortcutLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.runMacroShortcutLabel.TabIndex = 2;
        this.runMacroShortcutLabel.Text = "Press F5 to run the current macro.";
        this.runMacroShortcutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        
        // 
        // runMacroButton
        // 
        this.runMacroButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.runMacroButton.FlatAppearance.BorderSize = 0;
        this.runMacroButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.runMacroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.runMacroButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.runMacroButton.ForeColor = System.Drawing.Color.White;
        this.runMacroButton.Location = new System.Drawing.Point(83, 270);
        this.runMacroButton.Name = "runMacroButton";
        this.runMacroButton.Size = new System.Drawing.Size(180, 45);
        this.runMacroButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
        this.runMacroButton.TabIndex = 3;
        this.runMacroButton.Text = "Run Macro";
        this.runMacroButton.UseVisualStyleBackColor = false;
        
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
        this.Text = "Macro Forge Plus";
        this.Icon = new System.Drawing.Icon(System.IO.Path.Combine(Application.StartupPath, "Resources", "Favico.ico"));
        
        // Event handlers
        this.newScriptButton.Click += NewScriptButton_Click;
        this.openMacroButton.Click += OpenMacroButton_Click;
        this.runMacroButton.Click += RunMacroButton_Click;
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
        this.openMacroPanel.ResumeLayout(false);
        this.openMacroPanel.PerformLayout();
        this.runMacroPanel.ResumeLayout(false);
        this.runMacroPanel.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}