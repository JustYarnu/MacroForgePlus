namespace WinFormsApp.GUI;

partial class KeybindManagerDialog
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
        this.keybindsList = new System.Windows.Forms.ListView();
        this.keyColumn = new System.Windows.Forms.ColumnHeader();
        this.scriptColumn = new System.Windows.Forms.ColumnHeader();
        this.enabledColumn = new System.Windows.Forms.ColumnHeader();
        this.topPanel = new System.Windows.Forms.Panel();
        this.titleLabel = new System.Windows.Forms.Label();
        this.bottomPanel = new System.Windows.Forms.Panel();
        this.addButton = new System.Windows.Forms.Button();
        this.editButton = new System.Windows.Forms.Button();
        this.removeButton = new System.Windows.Forms.Button();
        this.closeButton = new System.Windows.Forms.Button();
        this.saveButton = new System.Windows.Forms.Button();
        this.loadButton = new System.Windows.Forms.Button();
        this.autoLoadCheckbox = new System.Windows.Forms.CheckBox();
        this.mainPanel.SuspendLayout();
        this.topPanel.SuspendLayout();
        this.bottomPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainPanel
        // 
        this.mainPanel.Controls.Add(this.keybindsList);
        this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainPanel.Location = new System.Drawing.Point(0, 60);
        this.mainPanel.Name = "mainPanel";
        this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
        this.mainPanel.Size = new System.Drawing.Size(700, 400);
        this.mainPanel.TabIndex = 0;
        // 
        // keybindsList
        // 
        this.keybindsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.keyColumn,
            this.scriptColumn,
            this.enabledColumn});
        this.keybindsList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.keybindsList.FullRowSelect = true;
        this.keybindsList.GridLines = true;
        this.keybindsList.HideSelection = false;
        this.keybindsList.Location = new System.Drawing.Point(10, 10);
        this.keybindsList.MultiSelect = false;
        this.keybindsList.Name = "keybindsList";
        this.keybindsList.Size = new System.Drawing.Size(680, 380);
        this.keybindsList.TabIndex = 0;
        this.keybindsList.UseCompatibleStateImageBehavior = false;
        this.keybindsList.View = System.Windows.Forms.View.Details;
        this.keybindsList.SelectedIndexChanged += new System.EventHandler(this.KeybindsList_SelectedIndexChanged);
        // 
        // keyColumn
        // 
        this.keyColumn.Text = "Key Binding";
        this.keyColumn.Width = 150;
        // 
        // scriptColumn
        // 
        this.scriptColumn.Text = "Script";
        this.scriptColumn.Width = 400;
        // 
        // enabledColumn
        // 
        this.enabledColumn.Text = "Enabled";
        this.enabledColumn.Width = 100;
        // 
        // topPanel
        // 
        this.topPanel.Controls.Add(this.titleLabel);
        this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.topPanel.Location = new System.Drawing.Point(0, 0);
        this.topPanel.Name = "topPanel";
        this.topPanel.Size = new System.Drawing.Size(700, 60);
        this.topPanel.TabIndex = 1;
        // 
        // titleLabel
        // 
        this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.titleLabel.ForeColor = System.Drawing.Color.White;
        this.titleLabel.Location = new System.Drawing.Point(0, 0);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(700, 60);
        this.titleLabel.TabIndex = 0;
        this.titleLabel.Text = "Keybind Manager";
        this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // bottomPanel
        // 
        this.bottomPanel.Controls.Add(this.autoLoadCheckbox);
        this.bottomPanel.Controls.Add(this.loadButton);
        this.bottomPanel.Controls.Add(this.saveButton);
        this.bottomPanel.Controls.Add(this.closeButton);
        this.bottomPanel.Controls.Add(this.removeButton);
        this.bottomPanel.Controls.Add(this.editButton);
        this.bottomPanel.Controls.Add(this.addButton);
        this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.bottomPanel.Location = new System.Drawing.Point(0, 460);
        this.bottomPanel.Name = "bottomPanel";
        this.bottomPanel.Padding = new System.Windows.Forms.Padding(10);
        this.bottomPanel.Size = new System.Drawing.Size(700, 60);
        this.bottomPanel.TabIndex = 2;
        // 
        // addButton
        // 
        this.addButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.addButton.FlatAppearance.BorderSize = 0;
        this.addButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.addButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.addButton.ForeColor = System.Drawing.Color.White;
        this.addButton.Location = new System.Drawing.Point(10, 10);
        this.addButton.Name = "addButton";
        this.addButton.Size = new System.Drawing.Size(100, 35);
        this.addButton.TabIndex = 0;
        this.addButton.Text = "Add";
        this.addButton.UseVisualStyleBackColor = false;
        this.addButton.Click += new System.EventHandler(this.AddButton_Click);
        // 
        // editButton
        // 
        this.editButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.editButton.FlatAppearance.BorderSize = 0;
        this.editButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.editButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.editButton.ForeColor = System.Drawing.Color.White;
        this.editButton.Location = new System.Drawing.Point(120, 10);
        this.editButton.Name = "editButton";
        this.editButton.Size = new System.Drawing.Size(100, 35);
        this.editButton.TabIndex = 1;
        this.editButton.Text = "Edit";
        this.editButton.UseVisualStyleBackColor = false;
        this.editButton.Click += new System.EventHandler(this.EditButton_Click);
        // 
        // removeButton
        // 
        this.removeButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.removeButton.FlatAppearance.BorderSize = 0;
        this.removeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.removeButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.removeButton.ForeColor = System.Drawing.Color.White;
        this.removeButton.Location = new System.Drawing.Point(230, 10);
        this.removeButton.Name = "removeButton";
        this.removeButton.Size = new System.Drawing.Size(100, 35);
        this.removeButton.TabIndex = 2;
        this.removeButton.Text = "Remove";
        this.removeButton.UseVisualStyleBackColor = false;
        this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
        // 
        // closeButton
        // 
        this.closeButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.closeButton.FlatAppearance.BorderSize = 0;
        this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.closeButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.closeButton.ForeColor = System.Drawing.Color.White;
        this.closeButton.Location = new System.Drawing.Point(590, 10);
        this.closeButton.Name = "closeButton";
        this.closeButton.Size = new System.Drawing.Size(100, 35);
        this.closeButton.TabIndex = 6;
        this.closeButton.Text = "Close";
        this.closeButton.UseVisualStyleBackColor = false;
        this.closeButton.Click += new System.EventHandler(this.CloseButton_Click);
        // 
        // saveButton
        // 
        this.saveButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.saveButton.FlatAppearance.BorderSize = 0;
        this.saveButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.saveButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.saveButton.ForeColor = System.Drawing.Color.White;
        this.saveButton.Location = new System.Drawing.Point(380, 10);
        this.saveButton.Name = "saveButton";
        this.saveButton.Size = new System.Drawing.Size(100, 35);
        this.saveButton.TabIndex = 4;
        this.saveButton.Text = "Save Profile";
        this.saveButton.UseVisualStyleBackColor = false;
        this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
        // 
        // loadButton
        // 
        this.loadButton.BackColor = System.Drawing.Color.FromArgb(60, 60, 65);
        this.loadButton.FlatAppearance.BorderSize = 0;
        this.loadButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(80, 80, 85);
        this.loadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.loadButton.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.loadButton.ForeColor = System.Drawing.Color.White;
        this.loadButton.Location = new System.Drawing.Point(475, 10);
        this.loadButton.Name = "loadButton";
        this.loadButton.Size = new System.Drawing.Size(100, 35);
        this.loadButton.TabIndex = 5;
        this.loadButton.Text = "Load Profile";
        this.loadButton.UseVisualStyleBackColor = false;
        this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
        // 
        // autoLoadCheckbox
        // 
        this.autoLoadCheckbox.AutoSize = true;
        this.autoLoadCheckbox.ForeColor = System.Drawing.Color.White;
        this.autoLoadCheckbox.Location = new System.Drawing.Point(340, 20);
        this.autoLoadCheckbox.Name = "autoLoadCheckbox";
        this.autoLoadCheckbox.Size = new System.Drawing.Size(15, 14);
        this.autoLoadCheckbox.TabIndex = 7;
        this.autoLoadCheckbox.UseVisualStyleBackColor = true;
        this.autoLoadCheckbox.Visible = false;
        // 
        // KeybindManagerDialog
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
        this.ClientSize = new System.Drawing.Size(700, 520);
        this.Controls.Add(this.mainPanel);
        this.Controls.Add(this.bottomPanel);
        this.Controls.Add(this.topPanel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "KeybindManagerDialog";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Keybind Manager";
        this.mainPanel.ResumeLayout(false);
        this.topPanel.ResumeLayout(false);
        this.bottomPanel.ResumeLayout(false);
        this.bottomPanel.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel mainPanel;
    private System.Windows.Forms.ListView keybindsList;
    private System.Windows.Forms.ColumnHeader keyColumn;
    private System.Windows.Forms.ColumnHeader scriptColumn;
    private System.Windows.Forms.ColumnHeader enabledColumn;
    private System.Windows.Forms.Panel topPanel;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Panel bottomPanel;
    private System.Windows.Forms.Button addButton;
    private System.Windows.Forms.Button editButton;
    private System.Windows.Forms.Button removeButton;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button loadButton;
    private System.Windows.Forms.CheckBox autoLoadCheckbox;
}