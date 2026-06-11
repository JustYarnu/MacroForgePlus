namespace WinFormsApp.GUI;

partial class KeybindEditDialog
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
        this.mainPanel = new System.Windows.Forms.Panel();
        this.enabledCheckbox = new System.Windows.Forms.CheckBox();
        this.browseButton = new System.Windows.Forms.Button();
        this.scriptPathTextBox = new System.Windows.Forms.TextBox();
        this.scriptPathLabel = new System.Windows.Forms.Label();
        this.keyPanel = new System.Windows.Forms.Panel();
        this.shiftCheckbox = new System.Windows.Forms.CheckBox();
        this.altCheckbox = new System.Windows.Forms.CheckBox();
        this.ctrlCheckbox = new System.Windows.Forms.CheckBox();
        this.keyComboBox = new System.Windows.Forms.ComboBox();
        this.keyLabel = new System.Windows.Forms.Label();
        this.bottomPanel = new System.Windows.Forms.Panel();
        this.cancelButton = new System.Windows.Forms.Button();
        this.okButton = new System.Windows.Forms.Button();
        this.topPanel = new System.Windows.Forms.Panel();
        this.titleLabel = new System.Windows.Forms.Label();
        this.mainPanel.SuspendLayout();
        this.keyPanel.SuspendLayout();
        this.bottomPanel.SuspendLayout();
        this.topPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainPanel
        // 
        this.mainPanel.Controls.Add(this.enabledCheckbox);
        this.mainPanel.Controls.Add(this.browseButton);
        this.mainPanel.Controls.Add(this.scriptPathTextBox);
        this.mainPanel.Controls.Add(this.scriptPathLabel);
        this.mainPanel.Controls.Add(this.keyPanel);
        this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainPanel.Location = new System.Drawing.Point(0, 60);
        this.mainPanel.Name = "mainPanel";
        this.mainPanel.Padding = new System.Windows.Forms.Padding(20);
        this.mainPanel.Size = new System.Drawing.Size(500, 300);
        this.mainPanel.TabIndex = 0;
        // 
        // enabledCheckbox
        // 
        this.enabledCheckbox.AutoSize = true;
        this.enabledCheckbox.Checked = true;
        this.enabledCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
        this.enabledCheckbox.ForeColor = System.Drawing.Color.White;
        this.enabledCheckbox.Location = new System.Drawing.Point(23, 250);
        this.enabledCheckbox.Name = "enabledCheckbox";
        this.enabledCheckbox.Size = new System.Drawing.Size(70, 19);
        this.enabledCheckbox.TabIndex = 4;
        this.enabledCheckbox.Text = "Enabled";
        this.enabledCheckbox.UseVisualStyleBackColor = true;
        // 
        // browseButton
        // 
        this.browseButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.browseButton.FlatAppearance.BorderSize = 0;
        this.browseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.browseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.browseButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.browseButton.ForeColor = System.Drawing.Color.White;
        this.browseButton.Location = new System.Drawing.Point(400, 180);
        this.browseButton.Name = "browseButton";
        this.browseButton.Size = new System.Drawing.Size(75, 30);
        this.browseButton.TabIndex = 3;
        this.browseButton.Text = "Browse";
        this.browseButton.UseVisualStyleBackColor = false;
        this.browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
        // 
        // scriptPathTextBox
        // 
        this.scriptPathTextBox.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.scriptPathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.scriptPathTextBox.ForeColor = System.Drawing.Color.White;
        this.scriptPathTextBox.Location = new System.Drawing.Point(23, 182);
        this.scriptPathTextBox.Name = "scriptPathTextBox";
        this.scriptPathTextBox.Size = new System.Drawing.Size(371, 23);
        this.scriptPathTextBox.TabIndex = 2;
        // 
        // scriptPathLabel
        // 
        this.scriptPathLabel.AutoSize = false;
        this.scriptPathLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        this.scriptPathLabel.ForeColor = System.Drawing.Color.White;
        this.scriptPathLabel.Location = new System.Drawing.Point(23, 150);
        this.scriptPathLabel.Name = "scriptPathLabel";
        this.scriptPathLabel.Size = new System.Drawing.Size(200, 25);
        this.scriptPathLabel.TabIndex = 1;
        this.scriptPathLabel.Text = "Script File";
        // 
        // keyPanel
        // 
        this.keyPanel.Controls.Add(this.shiftCheckbox);
        this.keyPanel.Controls.Add(this.altCheckbox);
        this.keyPanel.Controls.Add(this.ctrlCheckbox);
        this.keyPanel.Controls.Add(this.keyComboBox);
        this.keyPanel.Controls.Add(this.keyLabel);
        this.keyPanel.Location = new System.Drawing.Point(23, 20);
        this.keyPanel.Name = "keyPanel";
        this.keyPanel.Size = new System.Drawing.Size(450, 120);
        this.keyPanel.TabIndex = 0;
        // 
        // shiftCheckbox
        // 
        this.shiftCheckbox.AutoSize = true;
        this.shiftCheckbox.ForeColor = System.Drawing.Color.White;
        this.shiftCheckbox.Location = new System.Drawing.Point(320, 70);
        this.shiftCheckbox.Name = "shiftCheckbox";
        this.shiftCheckbox.Size = new System.Drawing.Size(57, 19);
        this.shiftCheckbox.TabIndex = 3;
        this.shiftCheckbox.Text = "Shift";
        this.shiftCheckbox.UseVisualStyleBackColor = true;
        // 
        // altCheckbox
        // 
        this.altCheckbox.AutoSize = true;
        this.altCheckbox.ForeColor = System.Drawing.Color.White;
        this.altCheckbox.Location = new System.Drawing.Point(220, 70);
        this.altCheckbox.Name = "altCheckbox";
        this.altCheckbox.Size = new System.Drawing.Size(46, 19);
        this.altCheckbox.TabIndex = 2;
        this.altCheckbox.Text = "Alt";
        this.altCheckbox.UseVisualStyleBackColor = true;
        // 
        // ctrlCheckbox
        // 
        this.ctrlCheckbox.AutoSize = true;
        this.ctrlCheckbox.ForeColor = System.Drawing.Color.White;
        this.ctrlCheckbox.Location = new System.Drawing.Point(120, 70);
        this.ctrlCheckbox.Name = "ctrlCheckbox";
        this.ctrlCheckbox.Size = new System.Drawing.Size(48, 19);
        this.ctrlCheckbox.TabIndex = 1;
        this.ctrlCheckbox.Text = "Ctrl";
        this.ctrlCheckbox.UseVisualStyleBackColor = true;
        // 
        // keyComboBox
        // 
        this.keyComboBox.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
        this.keyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.keyComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.keyComboBox.ForeColor = System.Drawing.Color.White;
        this.keyComboBox.FormattingEnabled = true;
        this.keyComboBox.Location = new System.Drawing.Point(20, 30);
        this.keyComboBox.Name = "keyComboBox";
        this.keyComboBox.Size = new System.Drawing.Size(150, 23);
        this.keyComboBox.TabIndex = 0;
        // 
        // keyLabel
        // 
        this.keyLabel.AutoSize = false;
        this.keyLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        this.keyLabel.ForeColor = System.Drawing.Color.White;
        this.keyLabel.Location = new System.Drawing.Point(20, 5);
        this.keyLabel.Name = "keyLabel";
        this.keyLabel.Size = new System.Drawing.Size(200, 25);
        this.keyLabel.TabIndex = 0;
        this.keyLabel.Text = "Key Binding";
        // 
        // bottomPanel
        // 
        this.bottomPanel.Controls.Add(this.cancelButton);
        this.bottomPanel.Controls.Add(this.okButton);
        this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.bottomPanel.Location = new System.Drawing.Point(0, 360);
        this.bottomPanel.Name = "bottomPanel";
        this.bottomPanel.Padding = new System.Windows.Forms.Padding(10);
        this.bottomPanel.Size = new System.Drawing.Size(500, 60);
        this.bottomPanel.TabIndex = 1;
        // 
        // cancelButton
        // 
        this.cancelButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.cancelButton.FlatAppearance.BorderSize = 0;
        this.cancelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.cancelButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.cancelButton.ForeColor = System.Drawing.Color.White;
        this.cancelButton.Location = new System.Drawing.Point(280, 12);
        this.cancelButton.Name = "cancelButton";
        this.cancelButton.Size = new System.Drawing.Size(100, 35);
        this.cancelButton.TabIndex = 1;
        this.cancelButton.Text = "Cancel";
        this.cancelButton.UseVisualStyleBackColor = false;
        this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
        // 
        // okButton
        // 
        this.okButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.okButton.FlatAppearance.BorderSize = 0;
        this.okButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.okButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.okButton.ForeColor = System.Drawing.Color.White;
        this.okButton.Location = new System.Drawing.Point(120, 12);
        this.okButton.Name = "okButton";
        this.okButton.Size = new System.Drawing.Size(100, 35);
        this.okButton.TabIndex = 0;
        this.okButton.Text = "OK";
        this.okButton.UseVisualStyleBackColor = false;
        this.okButton.Click += new System.EventHandler(this.OkButton_Click);
        // 
        // topPanel
        // 
        this.topPanel.Controls.Add(this.titleLabel);
        this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.topPanel.Location = new System.Drawing.Point(0, 0);
        this.topPanel.Name = "topPanel";
        this.topPanel.Size = new System.Drawing.Size(500, 60);
        this.topPanel.TabIndex = 2;
        // 
        // titleLabel
        // 
        this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
        this.titleLabel.ForeColor = System.Drawing.Color.White;
        this.titleLabel.Location = new System.Drawing.Point(0, 0);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(500, 60);
        this.titleLabel.TabIndex = 0;
        this.titleLabel.Text = "Edit Keybind";
        this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // KeybindEditDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
        this.ClientSize = new System.Drawing.Size(500, 420);
        this.Controls.Add(this.mainPanel);
        this.Controls.Add(this.bottomPanel);
        this.Controls.Add(this.topPanel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "KeybindEditDialog";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Edit Keybind";
        this.mainPanel.ResumeLayout(false);
        this.mainPanel.PerformLayout();
        this.keyPanel.ResumeLayout(false);
        this.keyPanel.PerformLayout();
        this.bottomPanel.ResumeLayout(false);
        this.topPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel mainPanel;
    private System.Windows.Forms.Panel keyPanel;
    private System.Windows.Forms.ComboBox keyComboBox;
    private System.Windows.Forms.Label keyLabel;
    private System.Windows.Forms.CheckBox shiftCheckbox;
    private System.Windows.Forms.CheckBox altCheckbox;
    private System.Windows.Forms.CheckBox ctrlCheckbox;
    private System.Windows.Forms.Button browseButton;
    private System.Windows.Forms.TextBox scriptPathTextBox;
    private System.Windows.Forms.Label scriptPathLabel;
    private System.Windows.Forms.CheckBox enabledCheckbox;
    private System.Windows.Forms.Panel bottomPanel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Panel topPanel;
    private System.Windows.Forms.Label titleLabel;
}