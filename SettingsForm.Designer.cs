namespace EU4_Parse_Lib;

partial class SettingsForm
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
        TreeNode treeNode2 = new TreeNode("KeyBinds");
        KeyMappingSettingsTab = new TabPage();
        keyBindsView = new ListView();
        assignKeyButton = new Button();
        newKeyBindBox = new TextBox();
        ConfirmSettingsButton = new Button();
        SettingsTreeView = new TreeView();
        tabControl1 = new TabControl();
        ResetKeyMappingsButton = new Button();
        label1 = new Label();
        KeyMappingSettingsTab.SuspendLayout();
        tabControl1.SuspendLayout();
        SuspendLayout();
        // 
        // KeyMappingSettingsTab
        // 
        KeyMappingSettingsTab.Controls.Add(label1);
        KeyMappingSettingsTab.Controls.Add(ResetKeyMappingsButton);
        KeyMappingSettingsTab.Controls.Add(keyBindsView);
        KeyMappingSettingsTab.Controls.Add(assignKeyButton);
        KeyMappingSettingsTab.Controls.Add(newKeyBindBox);
        KeyMappingSettingsTab.Controls.Add(ConfirmSettingsButton);
        KeyMappingSettingsTab.Controls.Add(SettingsTreeView);
        KeyMappingSettingsTab.Location = new Point(4, 24);
        KeyMappingSettingsTab.Name = "KeyMappingSettingsTab";
        KeyMappingSettingsTab.Padding = new Padding(3);
        KeyMappingSettingsTab.Size = new Size(424, 422);
        KeyMappingSettingsTab.TabIndex = 1;
        KeyMappingSettingsTab.Text = "Key Mapping";
        KeyMappingSettingsTab.UseVisualStyleBackColor = true;
        // 
        // keyBindsView
        // 
        keyBindsView.Location = new Point(168, 6);
        keyBindsView.Name = "keyBindsView";
        keyBindsView.Size = new Size(248, 376);
        keyBindsView.TabIndex = 7;
        keyBindsView.UseCompatibleStateImageBehavior = false;
        // 
        // assignKeyButton
        // 
        assignKeyButton.Location = new Point(254, 388);
        assignKeyButton.Name = "assignKeyButton";
        assignKeyButton.Size = new Size(81, 23);
        assignKeyButton.TabIndex = 6;
        assignKeyButton.Text = "Assign";
        assignKeyButton.UseVisualStyleBackColor = true;
        assignKeyButton.Click += AssignKeyButton_Click;
        // 
        // newKeyBindBox
        // 
        newKeyBindBox.Location = new Point(168, 388);
        newKeyBindBox.Name = "newKeyBindBox";
        newKeyBindBox.Size = new Size(80, 23);
        newKeyBindBox.TabIndex = 5;
        // 
        // ConfirmSettingsButton
        // 
        ConfirmSettingsButton.Location = new Point(341, 388);
        ConfirmSettingsButton.Name = "ConfirmSettingsButton";
        ConfirmSettingsButton.Size = new Size(75, 23);
        ConfirmSettingsButton.TabIndex = 4;
        ConfirmSettingsButton.Text = "Confirm";
        ConfirmSettingsButton.UseVisualStyleBackColor = true;
        ConfirmSettingsButton.Click += ConfirmSettingsButton_Click_1;
        // 
        // SettingsTreeView
        // 
        SettingsTreeView.Indent = 10;
        SettingsTreeView.Location = new Point(6, 6);
        SettingsTreeView.Name = "SettingsTreeView";
        treeNode2.Name = "KeybindsSettingsNode";
        treeNode2.Text = "KeyBinds";
        SettingsTreeView.Nodes.AddRange(new TreeNode[] { treeNode2 });
        SettingsTreeView.Size = new Size(155, 320);
        SettingsTreeView.TabIndex = 0;
        SettingsTreeView.AfterSelect += SettingsTreeView_AfterSelect;
        // 
        // tabControl1
        // 
        tabControl1.Controls.Add(KeyMappingSettingsTab);
        tabControl1.Dock = DockStyle.Fill;
        tabControl1.Location = new Point(0, 0);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        tabControl1.Size = new Size(432, 450);
        tabControl1.TabIndex = 0;
        // 
        // ResetKeyMappingsButton
        // 
        ResetKeyMappingsButton.Location = new Point(8, 388);
        ResetKeyMappingsButton.Name = "ResetKeyMappingsButton";
        ResetKeyMappingsButton.Size = new Size(153, 23);
        ResetKeyMappingsButton.TabIndex = 8;
        ResetKeyMappingsButton.Text = "Reset key mapping";
        ResetKeyMappingsButton.UseVisualStyleBackColor = true;
        ResetKeyMappingsButton.Click += ResetKeyMappingsButton_Click;
        // 
        // label1
        // 
        label1.BorderStyle = BorderStyle.FixedSingle;
        label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        label1.Location = new Point(6, 329);
        label1.Name = "label1";
        label1.Size = new Size(155, 53);
        label1.TabIndex = 9;
        label1.Text = "To import a custom mapping replace the UserKeyMapping.json file";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // SettingsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(432, 450);
        Controls.Add(tabControl1);
        KeyPreview = true;
        MaximizeBox = false;
        Name = "SettingsForm";
        Text = "Settings";
        Load += SettingsForm_Load;
        KeyDown += SettingsForm_KeyDown;
        KeyUp += SettingsForm_KeyUp;
        KeyMappingSettingsTab.ResumeLayout(false);
        KeyMappingSettingsTab.PerformLayout();
        tabControl1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TabPage KeyMappingSettingsTab;
    private TreeView SettingsTreeView;
    private TabControl tabControl1;
    private ListView keyBindsView;
    private Button assignKeyButton;
    private TextBox newKeyBindBox;
    private Button ConfirmSettingsButton;
    private Button ResetKeyMappingsButton;
    private Label label1;
}