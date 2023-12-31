﻿namespace EU4_Parse_Lib
{
   partial class ManageMapmodes
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
         components = new System.ComponentModel.Container();
         MapmodesTooltip = new ToolTip(components);
         AttributeToScopeTT = new ToolTip(components);
         FTLTooltip = new ToolTip(components);
         ManageMapmodesTab = new TabControl();
         CreateMapmodesTab = new TabPage();
         OneColorPerValueBox = new GroupBox();
         label10 = new Label();
         OneColorPerValueAttributeBox = new ComboBox();
         ClearMErroLog = new Button();
         label20 = new Label();
         ColorTableBox = new GroupBox();
         TableColorPreview = new Panel();
         AddToColorTable = new Button();
         label18 = new Label();
         label15 = new Label();
         ColortablePreview = new ListView();
         label16 = new Label();
         ColAtributeBox = new ComboBox();
         ColValueBox = new TextBox();
         label17 = new Label();
         ColColorBox = new TextBox();
         UseTriggerButton = new RadioButton();
         UseOneColroPerValueButton = new RadioButton();
         MapModeNameBox = new ComboBox();
         MErrorBox = new TextBox();
         OnlyLandProvincesCheckBox = new CheckBox();
         SaveMapmodeButton = new Button();
         GradianColorBox = new GroupBox();
         GradColorPreview = new Panel();
         label21 = new Label();
         GradAttributeBox = new ComboBox();
         GradColorBox = new TextBox();
         label14 = new Label();
         NullValueBox = new TextBox();
         label13 = new Label();
         MaxValueBox = new TextBox();
         label12 = new Label();
         MinValueBox = new TextBox();
         label11 = new Label();
         UseColorTableButton = new RadioButton();
         TriggerBox = new GroupBox();
         ModifyTriggerButton = new Button();
         ResetListButton = new Button();
         label9 = new Label();
         FinalTriggersListBox = new ListBox();
         label8 = new Label();
         DeleteTriggerButton = new Button();
         label3 = new Label();
         IsNegatedCheckBox = new CheckBox();
         ExistingTriggersInMM = new ListBox();
         TriggerNameBox = new ComboBox();
         SubTriggerGroup = new GroupBox();
         label24 = new Label();
         label23 = new Label();
         AvailableTriggersList = new ListBox();
         SelectedSubTriggersList = new ListBox();
         TriggerScopeList = new ComboBox();
         label19 = new Label();
         AddTriggerButton = new Button();
         label7 = new Label();
         TriggerAttributeBox = new ComboBox();
         label6 = new Label();
         TriggerValueBox = new TextBox();
         label5 = new Label();
         TriggerTypeBox = new ComboBox();
         label4 = new Label();
         UseGradiantButton = new RadioButton();
         MapModeScope = new ComboBox();
         label2 = new Label();
         label1 = new Label();
         ManageMapmodesTab.SuspendLayout();
         CreateMapmodesTab.SuspendLayout();
         OneColorPerValueBox.SuspendLayout();
         ColorTableBox.SuspendLayout();
         GradianColorBox.SuspendLayout();
         TriggerBox.SuspendLayout();
         SubTriggerGroup.SuspendLayout();
         SuspendLayout();
         // 
         // AttributeToScopeTT
         // 
         AttributeToScopeTT.Popup += AttributeToScopeTT_Popup;
         // 
         // FTLTooltip
         // 
         FTLTooltip.AutomaticDelay = 0;
         FTLTooltip.AutoPopDelay = 0;
         FTLTooltip.InitialDelay = 0;
         FTLTooltip.ReshowDelay = 0;
         FTLTooltip.ShowAlways = true;
         // 
         // ManageMapmodesTab
         // 
         ManageMapmodesTab.Alignment = TabAlignment.Left;
         ManageMapmodesTab.Controls.Add(CreateMapmodesTab);
         ManageMapmodesTab.ItemSize = new Size(316, 23);
         ManageMapmodesTab.Location = new Point(12, 12);
         ManageMapmodesTab.Multiline = true;
         ManageMapmodesTab.Name = "ManageMapmodesTab";
         ManageMapmodesTab.SelectedIndex = 0;
         ManageMapmodesTab.Size = new Size(860, 637);
         ManageMapmodesTab.SizeMode = TabSizeMode.Fixed;
         ManageMapmodesTab.TabIndex = 0;
         // 
         // CreateMapmodesTab
         // 
         CreateMapmodesTab.Controls.Add(OneColorPerValueBox);
         CreateMapmodesTab.Controls.Add(ClearMErroLog);
         CreateMapmodesTab.Controls.Add(label20);
         CreateMapmodesTab.Controls.Add(ColorTableBox);
         CreateMapmodesTab.Controls.Add(UseTriggerButton);
         CreateMapmodesTab.Controls.Add(UseOneColroPerValueButton);
         CreateMapmodesTab.Controls.Add(MapModeNameBox);
         CreateMapmodesTab.Controls.Add(MErrorBox);
         CreateMapmodesTab.Controls.Add(OnlyLandProvincesCheckBox);
         CreateMapmodesTab.Controls.Add(SaveMapmodeButton);
         CreateMapmodesTab.Controls.Add(GradianColorBox);
         CreateMapmodesTab.Controls.Add(UseColorTableButton);
         CreateMapmodesTab.Controls.Add(TriggerBox);
         CreateMapmodesTab.Controls.Add(UseGradiantButton);
         CreateMapmodesTab.Controls.Add(MapModeScope);
         CreateMapmodesTab.Controls.Add(label2);
         CreateMapmodesTab.Controls.Add(label1);
         CreateMapmodesTab.Location = new Point(27, 4);
         CreateMapmodesTab.Name = "CreateMapmodesTab";
         CreateMapmodesTab.Padding = new Padding(3);
         CreateMapmodesTab.Size = new Size(829, 629);
         CreateMapmodesTab.TabIndex = 1;
         CreateMapmodesTab.Text = "Create Mapmodes";
         CreateMapmodesTab.UseVisualStyleBackColor = true;
         // 
         // OneColorPerValueBox
         // 
         OneColorPerValueBox.Controls.Add(label10);
         OneColorPerValueBox.Controls.Add(OneColorPerValueAttributeBox);
         OneColorPerValueBox.Enabled = false;
         OneColorPerValueBox.Location = new Point(506, 369);
         OneColorPerValueBox.Name = "OneColorPerValueBox";
         OneColorPerValueBox.Size = new Size(317, 48);
         OneColorPerValueBox.TabIndex = 37;
         OneColorPerValueBox.TabStop = false;
         OneColorPerValueBox.Text = "Use once color per value";
         // 
         // label10
         // 
         label10.AutoSize = true;
         label10.Location = new Point(9, 20);
         label10.Name = "label10";
         label10.Size = new Size(57, 15);
         label10.TabIndex = 21;
         label10.Text = "Attribute:";
         // 
         // OneColorPerValueAttributeBox
         // 
         OneColorPerValueAttributeBox.AutoCompleteMode = AutoCompleteMode.Append;
         OneColorPerValueAttributeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         OneColorPerValueAttributeBox.FormattingEnabled = true;
         OneColorPerValueAttributeBox.Location = new Point(77, 17);
         OneColorPerValueAttributeBox.Name = "OneColorPerValueAttributeBox";
         OneColorPerValueAttributeBox.Size = new Size(93, 23);
         OneColorPerValueAttributeBox.TabIndex = 21;
         OneColorPerValueAttributeBox.Text = "Id";
         // 
         // ClearMErroLog
         // 
         ClearMErroLog.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
         ClearMErroLog.Location = new Point(754, 432);
         ClearMErroLog.Name = "ClearMErroLog";
         ClearMErroLog.Size = new Size(69, 21);
         ClearMErroLog.TabIndex = 36;
         ClearMErroLog.Text = "Clear log";
         ClearMErroLog.UseVisualStyleBackColor = true;
         ClearMErroLog.Click += ClearMErrorLog_Click;
         // 
         // label20
         // 
         label20.AutoSize = true;
         label20.Location = new Point(506, 434);
         label20.Name = "label20";
         label20.Size = new Size(55, 15);
         label20.TabIndex = 35;
         label20.Text = "Error Log";
         // 
         // ColorTableBox
         // 
         ColorTableBox.Controls.Add(TableColorPreview);
         ColorTableBox.Controls.Add(AddToColorTable);
         ColorTableBox.Controls.Add(label18);
         ColorTableBox.Controls.Add(label15);
         ColorTableBox.Controls.Add(ColortablePreview);
         ColorTableBox.Controls.Add(label16);
         ColorTableBox.Controls.Add(ColAtributeBox);
         ColorTableBox.Controls.Add(ColValueBox);
         ColorTableBox.Controls.Add(label17);
         ColorTableBox.Controls.Add(ColColorBox);
         ColorTableBox.Location = new Point(506, 118);
         ColorTableBox.Name = "ColorTableBox";
         ColorTableBox.Size = new Size(317, 136);
         ColorTableBox.TabIndex = 34;
         ColorTableBox.TabStop = false;
         ColorTableBox.Text = "Use color table";
         // 
         // TableColorPreview
         // 
         TableColorPreview.BackColor = Color.Transparent;
         TableColorPreview.Location = new Point(54, 78);
         TableColorPreview.Name = "TableColorPreview";
         TableColorPreview.Size = new Size(23, 23);
         TableColorPreview.TabIndex = 22;
         // 
         // AddToColorTable
         // 
         AddToColorTable.Location = new Point(9, 106);
         AddToColorTable.Name = "AddToColorTable";
         AddToColorTable.Size = new Size(161, 23);
         AddToColorTable.TabIndex = 16;
         AddToColorTable.Text = "Add to color table";
         AddToColorTable.UseVisualStyleBackColor = true;
         AddToColorTable.Click += AddToColorTable_Click;
         // 
         // label18
         // 
         label18.AutoSize = true;
         label18.Location = new Point(176, 16);
         label18.Name = "label18";
         label18.Size = new Size(62, 15);
         label18.TabIndex = 17;
         label18.Text = "Colortable";
         // 
         // label15
         // 
         label15.AutoSize = true;
         label15.Location = new Point(9, 23);
         label15.Name = "label15";
         label15.Size = new Size(57, 15);
         label15.TabIndex = 8;
         label15.Text = "Attribute:";
         // 
         // ColortablePreview
         // 
         ColortablePreview.Location = new Point(176, 35);
         ColortablePreview.Name = "ColortablePreview";
         ColortablePreview.Size = new Size(135, 94);
         ColortablePreview.TabIndex = 11;
         ColortablePreview.UseCompatibleStateImageBehavior = false;
         ColortablePreview.View = View.List;
         ColortablePreview.MouseClick += ColorTablePreview_MouseClick;
         // 
         // label16
         // 
         label16.AutoSize = true;
         label16.Location = new Point(9, 52);
         label16.Name = "label16";
         label16.Size = new Size(38, 15);
         label16.TabIndex = 12;
         label16.Text = "Value:";
         // 
         // ColAtributeBox
         // 
         ColAtributeBox.AutoCompleteMode = AutoCompleteMode.Append;
         ColAtributeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         ColAtributeBox.FormattingEnabled = true;
         ColAtributeBox.Location = new Point(81, 20);
         ColAtributeBox.Name = "ColAtributeBox";
         ColAtributeBox.Size = new Size(89, 23);
         ColAtributeBox.TabIndex = 19;
         ColAtributeBox.Text = "Id";
         ColAtributeBox.SelectedValueChanged += ColAttributeBox_SelectedValueChanged;
         // 
         // ColValueBox
         // 
         ColValueBox.Location = new Point(81, 49);
         ColValueBox.Name = "ColValueBox";
         ColValueBox.Size = new Size(89, 23);
         ColValueBox.TabIndex = 13;
         // 
         // label17
         // 
         label17.AutoSize = true;
         label17.Location = new Point(9, 81);
         label17.Name = "label17";
         label17.Size = new Size(39, 15);
         label17.TabIndex = 14;
         label17.Text = "Color:";
         // 
         // ColColorBox
         // 
         ColColorBox.Location = new Point(81, 78);
         ColColorBox.Name = "ColColorBox";
         ColColorBox.Size = new Size(89, 23);
         ColColorBox.TabIndex = 15;
         ColColorBox.Text = "0,155,155";
         ColColorBox.TextAlign = HorizontalAlignment.Center;
         ColColorBox.TextChanged += ColColorBox_TextChanged;
         // 
         // UseTriggerButton
         // 
         UseTriggerButton.AutoSize = true;
         UseTriggerButton.Location = new Point(316, 7);
         UseTriggerButton.Name = "UseTriggerButton";
         UseTriggerButton.Size = new Size(228, 19);
         UseTriggerButton.TabIndex = 33;
         UseTriggerButton.Text = "Use trigger ( red = false ; green = true )";
         UseTriggerButton.UseVisualStyleBackColor = true;
         UseTriggerButton.CheckedChanged += UseTriggerButton_CheckedChanged;
         UseTriggerButton.Click += MapModeTypeSelection_Click;
         // 
         // UseOneColroPerValueButton
         // 
         UseOneColroPerValueButton.AutoSize = true;
         UseOneColroPerValueButton.Location = new Point(316, 36);
         UseOneColroPerValueButton.Name = "UseOneColroPerValueButton";
         UseOneColroPerValueButton.Size = new Size(278, 19);
         UseOneColroPerValueButton.TabIndex = 18;
         UseOneColroPerValueButton.Text = "Use One color per value (random unique colors)";
         UseOneColroPerValueButton.UseVisualStyleBackColor = true;
         UseOneColroPerValueButton.CheckedChanged += UseOneColorPerValueButton_CheckedChanged;
         UseOneColroPerValueButton.Click += MapModeTypeSelection_Click;
         // 
         // MapModeNameBox
         // 
         MapModeNameBox.AutoCompleteMode = AutoCompleteMode.Append;
         MapModeNameBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         MapModeNameBox.FormattingEnabled = true;
         MapModeNameBox.Location = new Point(121, 6);
         MapModeNameBox.Name = "MapModeNameBox";
         MapModeNameBox.Size = new Size(189, 23);
         MapModeNameBox.TabIndex = 23;
         MapModeNameBox.SelectedValueChanged += MapModeNameBox_SelectedValueChanged;
         // 
         // MErrorBox
         // 
         MErrorBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
         MErrorBox.Location = new Point(506, 454);
         MErrorBox.Multiline = true;
         MErrorBox.Name = "MErrorBox";
         MErrorBox.ScrollBars = ScrollBars.Vertical;
         MErrorBox.Size = new Size(317, 39);
         MErrorBox.TabIndex = 10;
         // 
         // OnlyLandProvincesCheckBox
         // 
         OnlyLandProvincesCheckBox.AutoSize = true;
         OnlyLandProvincesCheckBox.Checked = true;
         OnlyLandProvincesCheckBox.CheckState = CheckState.Checked;
         OnlyLandProvincesCheckBox.Location = new Point(121, 66);
         OnlyLandProvincesCheckBox.Name = "OnlyLandProvincesCheckBox";
         OnlyLandProvincesCheckBox.Size = new Size(131, 19);
         OnlyLandProvincesCheckBox.TabIndex = 9;
         OnlyLandProvincesCheckBox.Text = "Only land provinces";
         OnlyLandProvincesCheckBox.UseVisualStyleBackColor = true;
         // 
         // SaveMapmodeButton
         // 
         SaveMapmodeButton.Location = new Point(704, 499);
         SaveMapmodeButton.Name = "SaveMapmodeButton";
         SaveMapmodeButton.Size = new Size(119, 23);
         SaveMapmodeButton.TabIndex = 8;
         SaveMapmodeButton.Text = "Save";
         SaveMapmodeButton.UseVisualStyleBackColor = true;
         SaveMapmodeButton.Click += SaveMapModeButton_Click;
         // 
         // GradianColorBox
         // 
         GradianColorBox.Controls.Add(GradColorPreview);
         GradianColorBox.Controls.Add(label21);
         GradianColorBox.Controls.Add(GradAttributeBox);
         GradianColorBox.Controls.Add(GradColorBox);
         GradianColorBox.Controls.Add(label14);
         GradianColorBox.Controls.Add(NullValueBox);
         GradianColorBox.Controls.Add(label13);
         GradianColorBox.Controls.Add(MaxValueBox);
         GradianColorBox.Controls.Add(label12);
         GradianColorBox.Controls.Add(MinValueBox);
         GradianColorBox.Controls.Add(label11);
         GradianColorBox.Location = new Point(506, 260);
         GradianColorBox.Name = "GradianColorBox";
         GradianColorBox.Size = new Size(317, 103);
         GradianColorBox.TabIndex = 7;
         GradianColorBox.TabStop = false;
         GradianColorBox.Text = "Use gradient";
         // 
         // GradColorPreview
         // 
         GradColorPreview.BackColor = Color.Transparent;
         GradColorPreview.Location = new Point(288, 16);
         GradColorPreview.Name = "GradColorPreview";
         GradColorPreview.Size = new Size(23, 23);
         GradColorPreview.TabIndex = 21;
         // 
         // label21
         // 
         label21.AutoSize = true;
         label21.Location = new Point(128, 48);
         label21.Name = "label21";
         label21.Size = new Size(57, 15);
         label21.TabIndex = 20;
         label21.Text = "Attribute:";
         // 
         // GradAttributeBox
         // 
         GradAttributeBox.AutoCompleteMode = AutoCompleteMode.Append;
         GradAttributeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         GradAttributeBox.FormattingEnabled = true;
         GradAttributeBox.Location = new Point(198, 45);
         GradAttributeBox.Name = "GradAttributeBox";
         GradAttributeBox.Size = new Size(113, 23);
         GradAttributeBox.TabIndex = 20;
         GradAttributeBox.Text = "Id";
         // 
         // GradColorBox
         // 
         GradColorBox.Location = new Point(198, 16);
         GradColorBox.Name = "GradColorBox";
         GradColorBox.Size = new Size(84, 23);
         GradColorBox.TabIndex = 8;
         GradColorBox.Text = "0,19,86";
         GradColorBox.TextAlign = HorizontalAlignment.Center;
         GradColorBox.TextChanged += GradColorBox_TextChanged;
         // 
         // label14
         // 
         label14.AutoSize = true;
         label14.Location = new Point(128, 19);
         label14.Name = "label14";
         label14.Size = new Size(64, 15);
         label14.TabIndex = 7;
         label14.Text = "Null Color:";
         // 
         // NullValueBox
         // 
         NullValueBox.Location = new Point(45, 74);
         NullValueBox.Name = "NullValueBox";
         NullValueBox.Size = new Size(56, 23);
         NullValueBox.TabIndex = 6;
         // 
         // label13
         // 
         label13.AutoSize = true;
         label13.Location = new Point(8, 77);
         label13.Name = "label13";
         label13.Size = new Size(32, 15);
         label13.TabIndex = 5;
         label13.Text = "Null:";
         // 
         // MaxValueBox
         // 
         MaxValueBox.Location = new Point(45, 45);
         MaxValueBox.Name = "MaxValueBox";
         MaxValueBox.Size = new Size(56, 23);
         MaxValueBox.TabIndex = 4;
         // 
         // label12
         // 
         label12.AutoSize = true;
         label12.Location = new Point(8, 48);
         label12.Name = "label12";
         label12.Size = new Size(33, 15);
         label12.TabIndex = 3;
         label12.Text = "Max:";
         // 
         // MinValueBox
         // 
         MinValueBox.Location = new Point(45, 16);
         MinValueBox.Name = "MinValueBox";
         MinValueBox.Size = new Size(56, 23);
         MinValueBox.TabIndex = 2;
         // 
         // label11
         // 
         label11.AutoSize = true;
         label11.Location = new Point(8, 19);
         label11.Name = "label11";
         label11.Size = new Size(31, 15);
         label11.TabIndex = 1;
         label11.Text = "Min:";
         // 
         // UseColorTableButton
         // 
         UseColorTableButton.AutoSize = true;
         UseColorTableButton.Location = new Point(316, 92);
         UseColorTableButton.Name = "UseColorTableButton";
         UseColorTableButton.Size = new Size(103, 19);
         UseColorTableButton.TabIndex = 10;
         UseColorTableButton.Text = "Use color table";
         UseColorTableButton.UseVisualStyleBackColor = true;
         UseColorTableButton.CheckedChanged += UseColorTableButton_CheckedChanged;
         UseColorTableButton.Click += MapModeTypeSelection_Click;
         // 
         // TriggerBox
         // 
         TriggerBox.Controls.Add(ModifyTriggerButton);
         TriggerBox.Controls.Add(ResetListButton);
         TriggerBox.Controls.Add(label9);
         TriggerBox.Controls.Add(FinalTriggersListBox);
         TriggerBox.Controls.Add(label8);
         TriggerBox.Controls.Add(DeleteTriggerButton);
         TriggerBox.Controls.Add(label3);
         TriggerBox.Controls.Add(IsNegatedCheckBox);
         TriggerBox.Controls.Add(ExistingTriggersInMM);
         TriggerBox.Controls.Add(TriggerNameBox);
         TriggerBox.Controls.Add(SubTriggerGroup);
         TriggerBox.Controls.Add(TriggerScopeList);
         TriggerBox.Controls.Add(label19);
         TriggerBox.Controls.Add(AddTriggerButton);
         TriggerBox.Controls.Add(label7);
         TriggerBox.Controls.Add(TriggerAttributeBox);
         TriggerBox.Controls.Add(label6);
         TriggerBox.Controls.Add(TriggerValueBox);
         TriggerBox.Controls.Add(label5);
         TriggerBox.Controls.Add(TriggerTypeBox);
         TriggerBox.Controls.Add(label4);
         TriggerBox.Location = new Point(6, 118);
         TriggerBox.Name = "TriggerBox";
         TriggerBox.Size = new Size(494, 375);
         TriggerBox.TabIndex = 4;
         TriggerBox.TabStop = false;
         TriggerBox.Text = "Trigger";
         // 
         // ModifyTriggerButton
         // 
         ModifyTriggerButton.Enabled = false;
         ModifyTriggerButton.Location = new Point(157, 344);
         ModifyTriggerButton.Name = "ModifyTriggerButton";
         ModifyTriggerButton.Size = new Size(147, 23);
         ModifyTriggerButton.TabIndex = 37;
         ModifyTriggerButton.Text = "Modify trigger";
         ModifyTriggerButton.UseVisualStyleBackColor = true;
         ModifyTriggerButton.Click += ModifyTriggerButton_Click;
         // 
         // ResetListButton
         // 
         ResetListButton.Location = new Point(309, 343);
         ResetListButton.Name = "ResetListButton";
         ResetListButton.Size = new Size(178, 23);
         ResetListButton.TabIndex = 35;
         ResetListButton.Text = "Reset List";
         ResetListButton.UseVisualStyleBackColor = true;
         ResetListButton.Click += ResetListButton_Click;
         // 
         // label9
         // 
         label9.AutoSize = true;
         label9.Location = new Point(306, 195);
         label9.Name = "label9";
         label9.Size = new Size(181, 15);
         label9.TabIndex = 34;
         label9.Text = "Final trigger list (RMB to remove)";
         // 
         // FinalTriggersListBox
         // 
         FinalTriggersListBox.FormattingEnabled = true;
         FinalTriggersListBox.ItemHeight = 15;
         FinalTriggersListBox.Location = new Point(309, 213);
         FinalTriggersListBox.Name = "FinalTriggersListBox";
         FinalTriggersListBox.Size = new Size(178, 124);
         FinalTriggersListBox.TabIndex = 33;
         FinalTriggersListBox.MouseDown += FinalTriggersListBox_MouseDown;
         FinalTriggersListBox.MouseHover += FinalTriggersListBox_MouseHover;
         // 
         // label8
         // 
         label8.AutoSize = true;
         label8.Location = new Point(310, 20);
         label8.Name = "label8";
         label8.Size = new Size(109, 15);
         label8.TabIndex = 20;
         label8.Text = "Created triggers list";
         // 
         // DeleteTriggerButton
         // 
         DeleteTriggerButton.Location = new Point(310, 158);
         DeleteTriggerButton.Name = "DeleteTriggerButton";
         DeleteTriggerButton.Size = new Size(178, 23);
         DeleteTriggerButton.TabIndex = 32;
         DeleteTriggerButton.Text = "Delete selected trigger";
         DeleteTriggerButton.UseVisualStyleBackColor = true;
         DeleteTriggerButton.Click += DeleteTriggerButton_Click;
         // 
         // label3
         // 
         label3.AutoSize = true;
         label3.Location = new Point(6, 159);
         label3.Name = "label3";
         label3.Size = new Size(64, 15);
         label3.TabIndex = 31;
         label3.Text = "Is negated:";
         // 
         // IsNegatedCheckBox
         // 
         IsNegatedCheckBox.AutoSize = true;
         IsNegatedCheckBox.Location = new Point(115, 158);
         IsNegatedCheckBox.Name = "IsNegatedCheckBox";
         IsNegatedCheckBox.Size = new Size(95, 19);
         IsNegatedCheckBox.TabIndex = 30;
         IsNegatedCheckBox.Text = "                       ";
         IsNegatedCheckBox.UseVisualStyleBackColor = true;
         // 
         // ExistingTriggersInMM
         // 
         ExistingTriggersInMM.ItemHeight = 15;
         ExistingTriggersInMM.Location = new Point(310, 43);
         ExistingTriggersInMM.Name = "ExistingTriggersInMM";
         ExistingTriggersInMM.Size = new Size(178, 109);
         ExistingTriggersInMM.TabIndex = 29;
         ExistingTriggersInMM.MouseDoubleClick += ExistingTriggersInMM_MouseDoubleClick;
         // 
         // TriggerNameBox
         // 
         TriggerNameBox.AutoCompleteMode = AutoCompleteMode.Append;
         TriggerNameBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         TriggerNameBox.FormattingEnabled = true;
         TriggerNameBox.Location = new Point(115, 16);
         TriggerNameBox.Name = "TriggerNameBox";
         TriggerNameBox.Size = new Size(189, 23);
         TriggerNameBox.TabIndex = 22;
         TriggerNameBox.Text = "customTrigger";
         TriggerNameBox.TextChanged += TriggerNameBox_TextChanged;
         // 
         // SubTriggerGroup
         // 
         SubTriggerGroup.Controls.Add(label24);
         SubTriggerGroup.Controls.Add(label23);
         SubTriggerGroup.Controls.Add(AvailableTriggersList);
         SubTriggerGroup.Controls.Add(SelectedSubTriggersList);
         SubTriggerGroup.Enabled = false;
         SubTriggerGroup.Location = new Point(6, 183);
         SubTriggerGroup.Name = "SubTriggerGroup";
         SubTriggerGroup.Size = new Size(298, 154);
         SubTriggerGroup.TabIndex = 21;
         SubTriggerGroup.TabStop = false;
         SubTriggerGroup.Text = "Subtriggers ( only for OR | AND )";
         // 
         // label24
         // 
         label24.AutoSize = true;
         label24.Location = new Point(184, 19);
         label24.Name = "label24";
         label24.Size = new Size(113, 15);
         label24.TabIndex = 28;
         label24.Text = "Selected subtriggers";
         // 
         // label23
         // 
         label23.AutoSize = true;
         label23.Location = new Point(3, 19);
         label23.Name = "label23";
         label23.Size = new Size(98, 15);
         label23.TabIndex = 27;
         label23.Text = "Available triggers";
         // 
         // AvailableTriggersList
         // 
         AvailableTriggersList.FormattingEnabled = true;
         AvailableTriggersList.ItemHeight = 15;
         AvailableTriggersList.Location = new Point(3, 37);
         AvailableTriggersList.Name = "AvailableTriggersList";
         AvailableTriggersList.Size = new Size(142, 109);
         AvailableTriggersList.TabIndex = 21;
         AvailableTriggersList.MouseDoubleClick += AvailableTriggersList_MouseDoubleClick;
         // 
         // SelectedSubTriggersList
         // 
         SelectedSubTriggersList.ItemHeight = 15;
         SelectedSubTriggersList.Location = new Point(151, 37);
         SelectedSubTriggersList.Name = "SelectedSubTriggersList";
         SelectedSubTriggersList.Size = new Size(142, 109);
         SelectedSubTriggersList.TabIndex = 22;
         SelectedSubTriggersList.MouseClick += SelectedSubTriggersList_MouseClick;
         SelectedSubTriggersList.MouseDoubleClick += SelectedSubTriggersList_MouseDoubleClick;
         // 
         // TriggerScopeList
         // 
         TriggerScopeList.AutoCompleteMode = AutoCompleteMode.Append;
         TriggerScopeList.AutoCompleteSource = AutoCompleteSource.ListItems;
         TriggerScopeList.DropDownStyle = ComboBoxStyle.DropDownList;
         TriggerScopeList.FormattingEnabled = true;
         TriggerScopeList.Location = new Point(115, 100);
         TriggerScopeList.Name = "TriggerScopeList";
         TriggerScopeList.Size = new Size(189, 23);
         TriggerScopeList.TabIndex = 18;
         TriggerScopeList.SelectedValueChanged += TriggerScopeList_SelectedValueChanged;
         // 
         // label19
         // 
         label19.AutoSize = true;
         label19.Location = new Point(6, 103);
         label19.Name = "label19";
         label19.Size = new Size(80, 15);
         label19.TabIndex = 17;
         label19.Text = "Trigger scope:";
         // 
         // AddTriggerButton
         // 
         AddTriggerButton.Location = new Point(6, 343);
         AddTriggerButton.Name = "AddTriggerButton";
         AddTriggerButton.Size = new Size(145, 23);
         AddTriggerButton.TabIndex = 10;
         AddTriggerButton.Text = "Add trigger";
         AddTriggerButton.UseVisualStyleBackColor = true;
         AddTriggerButton.Click += AddTriggerButton_Click;
         // 
         // label7
         // 
         label7.AutoSize = true;
         label7.Location = new Point(6, 19);
         label7.Name = "label7";
         label7.Size = new Size(79, 15);
         label7.TabIndex = 9;
         label7.Text = "Trigger name:";
         // 
         // TriggerAttributeBox
         // 
         TriggerAttributeBox.AutoCompleteMode = AutoCompleteMode.Append;
         TriggerAttributeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         TriggerAttributeBox.FormattingEnabled = true;
         TriggerAttributeBox.Location = new Point(115, 129);
         TriggerAttributeBox.Name = "TriggerAttributeBox";
         TriggerAttributeBox.Size = new Size(189, 23);
         TriggerAttributeBox.TabIndex = 8;
         // 
         // label6
         // 
         label6.AutoSize = true;
         label6.Location = new Point(6, 132);
         label6.Name = "label6";
         label6.Size = new Size(94, 15);
         label6.TabIndex = 7;
         label6.Text = "Trigger attribute:";
         // 
         // TriggerValueBox
         // 
         TriggerValueBox.Location = new Point(115, 72);
         TriggerValueBox.Name = "TriggerValueBox";
         TriggerValueBox.Size = new Size(189, 23);
         TriggerValueBox.TabIndex = 6;
         TriggerValueBox.TextChanged += TriggerValueBox_TextChanged;
         // 
         // label5
         // 
         label5.AutoSize = true;
         label5.Location = new Point(6, 75);
         label5.Name = "label5";
         label5.Size = new Size(77, 15);
         label5.TabIndex = 5;
         label5.Text = "Trigger value:";
         // 
         // TriggerTypeBox
         // 
         TriggerTypeBox.AutoCompleteMode = AutoCompleteMode.Append;
         TriggerTypeBox.AutoCompleteSource = AutoCompleteSource.ListItems;
         TriggerTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
         TriggerTypeBox.FormattingEnabled = true;
         TriggerTypeBox.Items.AddRange(new object[] { "MinTrigger", "EqualsTrigger", "OrTrigger", "AndTrigger" });
         TriggerTypeBox.Location = new Point(115, 43);
         TriggerTypeBox.Name = "TriggerTypeBox";
         TriggerTypeBox.Size = new Size(189, 23);
         TriggerTypeBox.TabIndex = 3;
         TriggerTypeBox.SelectedValueChanged += TriggerTypeBox_SelectedValueChanged;
         // 
         // label4
         // 
         label4.AutoSize = true;
         label4.Location = new Point(6, 46);
         label4.Name = "label4";
         label4.Size = new Size(72, 15);
         label4.TabIndex = 2;
         label4.Text = "Trigger type:";
         // 
         // UseGradiantButton
         // 
         UseGradiantButton.AutoSize = true;
         UseGradiantButton.Checked = true;
         UseGradiantButton.Location = new Point(316, 65);
         UseGradiantButton.Name = "UseGradiantButton";
         UseGradiantButton.Size = new Size(91, 19);
         UseGradiantButton.TabIndex = 9;
         UseGradiantButton.TabStop = true;
         UseGradiantButton.Text = "Use gradient";
         UseGradiantButton.UseVisualStyleBackColor = true;
         UseGradiantButton.CheckedChanged += UseGradientButton_CheckedChanged;
         UseGradiantButton.Click += MapModeTypeSelection_Click;
         // 
         // MapModeScope
         // 
         MapModeScope.DisplayMember = "Province";
         MapModeScope.DropDownStyle = ComboBoxStyle.DropDownList;
         MapModeScope.FormattingEnabled = true;
         MapModeScope.Items.AddRange(new object[] { "Province", "Country" });
         MapModeScope.Location = new Point(121, 35);
         MapModeScope.Name = "MapModeScope";
         MapModeScope.Size = new Size(189, 23);
         MapModeScope.TabIndex = 3;
         MapModeScope.SelectedValueChanged += MapModeScope_SelectedValueChanged;
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.Location = new Point(6, 38);
         label2.Name = "label2";
         label2.Size = new Size(99, 15);
         label2.TabIndex = 2;
         label2.Text = "Mapmode scope:";
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Location = new Point(6, 9);
         label1.Name = "label1";
         label1.Size = new Size(101, 15);
         label1.TabIndex = 0;
         label1.Text = "Mapmode name: ";
         // 
         // ManageMapmodes
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(884, 661);
         Controls.Add(ManageMapmodesTab);
         Name = "ManageMapmodes";
         Text = "Mapmode Manager";
         FormClosing += ManageMapModes_FormClosing;
         Load += ManageMapModes_Load;
         ManageMapmodesTab.ResumeLayout(false);
         CreateMapmodesTab.ResumeLayout(false);
         CreateMapmodesTab.PerformLayout();
         OneColorPerValueBox.ResumeLayout(false);
         OneColorPerValueBox.PerformLayout();
         ColorTableBox.ResumeLayout(false);
         ColorTableBox.PerformLayout();
         GradianColorBox.ResumeLayout(false);
         GradianColorBox.PerformLayout();
         TriggerBox.ResumeLayout(false);
         TriggerBox.PerformLayout();
         SubTriggerGroup.ResumeLayout(false);
         SubTriggerGroup.PerformLayout();
         ResumeLayout(false);
      }

      #endregion
      public ToolTip MapmodesTooltip;
      private ComboBox MapModeTypeList;
      private ComboBox SubTriggerListBox;
      private ToolTip AttributeToScopeTT;
      private ToolTip FTLTooltip;
      private TabControl ManageMapmodesTab;
      private TabPage CreateMapmodesTab;
      private GroupBox OneColorPerValueBox;
      private Label label10;
      private ComboBox OneColorPerValueAttributeBox;
      private Button ClearMErroLog;
      private Label label20;
      private GroupBox ColorTableBox;
      private Panel TableColorPreview;
      private Button AddToColorTable;
      private Label label18;
      private Label label15;
      private ListView ColortablePreview;
      private Label label16;
      private ComboBox ColAtributeBox;
      private TextBox ColValueBox;
      private Label label17;
      private TextBox ColColorBox;
      private RadioButton UseTriggerButton;
      private RadioButton UseOneColroPerValueButton;
      private ComboBox MapModeNameBox;
      public TextBox MErrorBox;
      private CheckBox OnlyLandProvincesCheckBox;
      private Button SaveMapmodeButton;
      private GroupBox GradianColorBox;
      private Panel GradColorPreview;
      private Label label21;
      private ComboBox GradAttributeBox;
      private TextBox GradColorBox;
      private Label label14;
      private TextBox NullValueBox;
      private Label label13;
      private TextBox MaxValueBox;
      private Label label12;
      private TextBox MinValueBox;
      private Label label11;
      private RadioButton UseColorTableButton;
      private GroupBox TriggerBox;
      private Button ModifyTriggerButton;
      private Button ResetListButton;
      private Label label9;
      private ListBox FinalTriggersListBox;
      private Label label8;
      private Button DeleteTriggerButton;
      private Label label3;
      private CheckBox IsNegatedCheckBox;
      private ListBox ExistingTriggersInMM;
      private ComboBox TriggerNameBox;
      private GroupBox SubTriggerGroup;
      private Label label24;
      private Label label23;
      private ListBox AvailableTriggersList;
      private ListBox SelectedSubTriggersList;
      private ComboBox TriggerScopeList;
      private Label label19;
      private Button AddTriggerButton;
      private Label label7;
      private ComboBox TriggerAttributeBox;
      private Label label6;
      private TextBox TriggerValueBox;
      private Label label5;
      private ComboBox TriggerTypeBox;
      private Label label4;
      private RadioButton UseGradiantButton;
      private ComboBox MapModeScope;
      private Label label2;
      private Label label1;
   }
}