namespace EU4_Parse_Lib
{
   partial class MainWindow
   {
      /// <summary>
      ///  Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

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
         components = new System.ComponentModel.Container();
         Map = new PictureBox();
         StepsizeMove = new TrackBar();
         label1 = new Label();
         label2 = new Label();
         zoomTrackBar = new TrackBar();
         _tt = new ToolTip(components);
         menuStrip1 = new MenuStrip();
         settingsToolStripMenuItem = new ToolStripMenuItem();
         ReloadLocalisationToolStripMenuItem = new ToolStripMenuItem();
         keyMappingToolStripMenuItem = new ToolStripMenuItem();
         toolStripSeparator5 = new ToolStripSeparator();
         clearCacheToolStripMenuItem = new ToolStripMenuItem();
         clearAllUserDefinedFilesToolStripMenuItem = new ToolStripMenuItem();
         AlwayRenderProvinceOutlineSetting = new ToolStripMenuItem();
         mapmodesToolStripMenuItem = new ToolStripMenuItem();
         mapmodeSelectorToolStripMenuItem = new ToolStripMenuItem();
         ManageMapModesMenu = new ToolStripMenuItem();
         resetAllMapmodesToolStripMenuItem = new ToolStripMenuItem();
         tooltipToolStripMenuItem = new ToolStripMenuItem();
         StatisticsTollStripMenuItem = new ToolStripMenuItem();
         selectionModeToolStripMenuItem = new ToolStripMenuItem();
         ProvinceSelectionModeMenuItem = new ToolStripMenuItem();
         AreaSelectionModeMenuItem = new ToolStripMenuItem();
         RegionSelectionModeMenuItem = new ToolStripMenuItem();
         OwnerSelectionModeMenuItem = new ToolStripMenuItem();
         helpToolStripMenuItem = new ToolStripMenuItem();
         ZoomInButton = new Button();
         ZoomOutButton = new Button();
         panel1 = new Panel();
         MapModeButtons = new TableLayoutPanel();
         MainRightClickMenu = new ContextMenuStrip(components);
         OpenProvinceFileContext = new ToolStripMenuItem();
         OpenCountryFileContext = new ToolStripMenuItem();
         toolStripSeparator1 = new ToolStripSeparator();
         QuickSelectionContext = new ToolStripMenuItem();
         SelectOwnerContext = new ToolStripMenuItem();
         SelectAllCoresContext = new ToolStripMenuItem();
         ProvinceCoresContext = new ToolStripComboBox();
         CoreSelectionConfirmContext = new ToolStripMenuItem();
         SelectAreaContext = new ToolStripMenuItem();
         SelectRegionContext = new ToolStripMenuItem();
         SelectTradenodeContext = new ToolStripMenuItem();
         SelectSuperRegionContext = new ToolStripMenuItem();
         SelectContinentContext = new ToolStripMenuItem();
         SelectReligionContext = new ToolStripMenuItem();
         ReligionSelectionContext = new ToolStripComboBox();
         CultureSelectionMenuContext = new ToolStripMenuItem();
         SelectCultureContext = new ToolStripComboBox();
         AdvancedSelectionMenuContext = new ToolStripMenuItem();
         CustomTriggerSelectionContext = new ToolStripMenuItem();
         RandomSelectionContext = new ToolStripMenuItem();
         toolStripSeparator3 = new ToolStripSeparator();
         AddToContextCollection = new ToolStripMenuItem();
         AddToAreaContext = new ToolStripMenuItem();
         AddToRegionContext = new ToolStripMenuItem();
         AddToTradeNodeContext = new ToolStripMenuItem();
         AddToSuperRegionContext = new ToolStripMenuItem();
         AddToContinentContext = new ToolStripMenuItem();
         AddToProvinceGroupContext = new ToolStripMenuItem();
         AddClaimsCoresContext = new ToolStripMenuItem();
         AddCoreSelectionContext = new ToolStripComboBox();
         AddPermaClaimContext = new ToolStripComboBox();
         AddClaimContext = new ToolStripComboBox();
         AddModifierMenuContext = new ToolStripMenuItem();
         AddProvinceModifierSelectionContext = new ToolStripComboBox();
         AddCountryModifierSelectionContext = new ToolStripComboBox();
         toolStripSeparator4 = new ToolStripSeparator();
         RemoveCoresClaimsContext = new ToolStripMenuItem();
         RemoveCoresSelectionContext = new ToolStripComboBox();
         RemovePermaClaimsContext = new ToolStripComboBox();
         RemoveClaimContext = new ToolStripComboBox();
         RemoveModifierMenuContext = new ToolStripMenuItem();
         RemoveProvinceModifierContext = new ToolStripComboBox();
         RemoveCountryModifierContext = new ToolStripComboBox();
         ColorChangeScopeContext = new ToolStripComboBox();
         toolStripMenuItem1 = new ToolStripMenuItem();
         toolStripSeparator2 = new ToolStripSeparator();
         ConfirmColorChangeContext = new ToolStripMenuItem();
         groupBox1 = new GroupBox();
         debugToolStripMenuItem = new ToolStripMenuItem();
         ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
         ((System.ComponentModel.ISupportInitialize)StepsizeMove).BeginInit();
         ((System.ComponentModel.ISupportInitialize)zoomTrackBar).BeginInit();
         menuStrip1.SuspendLayout();
         panel1.SuspendLayout();
         MainRightClickMenu.SuspendLayout();
         groupBox1.SuspendLayout();
         SuspendLayout();
         // 
         // Map
         // 
         Map.Location = new Point(0, 0);
         Map.Name = "Map";
         Map.Size = new Size(940, 720);
         Map.SizeMode = PictureBoxSizeMode.AutoSize;
         Map.TabIndex = 0;
         Map.TabStop = false;
         Map.MouseClick += Map_MouseClick;
         Map.MouseEnter += Map_MouseEnter;
         Map.MouseLeave += Map_MouseLeave;
         Map.MouseHover += Map_MouseHover;
         Map.MouseMove += Map_MouseMove;
         // 
         // StepsizeMove
         // 
         StepsizeMove.LargeChange = 1;
         StepsizeMove.Location = new Point(12, 46);
         StepsizeMove.Minimum = 1;
         StepsizeMove.Name = "StepsizeMove";
         StepsizeMove.Size = new Size(189, 45);
         StepsizeMove.TabIndex = 5;
         StepsizeMove.TabStop = false;
         StepsizeMove.TickStyle = TickStyle.None;
         StepsizeMove.Value = 1;
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Location = new Point(12, 29);
         label1.Name = "label1";
         label1.Size = new Size(146, 15);
         label1.TabIndex = 6;
         label1.Text = "Stepize of map movement";
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.Location = new Point(12, 72);
         label2.Name = "label2";
         label2.Size = new Size(66, 15);
         label2.TabIndex = 7;
         label2.Text = "Zoom level";
         // 
         // zoomTrackBar
         // 
         zoomTrackBar.Enabled = false;
         zoomTrackBar.LargeChange = 2;
         zoomTrackBar.Location = new Point(12, 90);
         zoomTrackBar.Maximum = 20;
         zoomTrackBar.Minimum = 6;
         zoomTrackBar.Name = "zoomTrackBar";
         zoomTrackBar.Size = new Size(189, 45);
         zoomTrackBar.SmallChange = 2;
         zoomTrackBar.TabIndex = 8;
         zoomTrackBar.TabStop = false;
         zoomTrackBar.TickStyle = TickStyle.None;
         zoomTrackBar.Value = 6;
         zoomTrackBar.ValueChanged += zoomTrackBar_ValueChanged;
         // 
         // _tt
         // 
         _tt.AutoPopDelay = 5000;
         _tt.InitialDelay = 500;
         _tt.ReshowDelay = 100;
         _tt.ShowAlways = true;
         _tt.UseAnimation = false;
         _tt.UseFading = false;
         // 
         // menuStrip1
         // 
         menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, mapmodesToolStripMenuItem, tooltipToolStripMenuItem, StatisticsTollStripMenuItem, selectionModeToolStripMenuItem, helpToolStripMenuItem });
         menuStrip1.Location = new Point(0, 0);
         menuStrip1.Name = "menuStrip1";
         menuStrip1.Size = new Size(1163, 24);
         menuStrip1.TabIndex = 9;
         menuStrip1.Text = "menuStrip1";
         // 
         // settingsToolStripMenuItem
         // 
         settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ReloadLocalisationToolStripMenuItem, keyMappingToolStripMenuItem, toolStripSeparator5, clearCacheToolStripMenuItem, clearAllUserDefinedFilesToolStripMenuItem, AlwayRenderProvinceOutlineSetting, debugToolStripMenuItem });
         settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
         settingsToolStripMenuItem.Size = new Size(61, 20);
         settingsToolStripMenuItem.Text = "Settings";
         // 
         // ReloadLocalisationToolStripMenuItem
         // 
         ReloadLocalisationToolStripMenuItem.Name = "ReloadLocalisationToolStripMenuItem";
         ReloadLocalisationToolStripMenuItem.Size = new Size(237, 22);
         ReloadLocalisationToolStripMenuItem.Text = "Reload Localisation";
         ReloadLocalisationToolStripMenuItem.Click += ReloadLocalizationToolStripMenuItem_Click;
         // 
         // keyMappingToolStripMenuItem
         // 
         keyMappingToolStripMenuItem.Name = "keyMappingToolStripMenuItem";
         keyMappingToolStripMenuItem.Size = new Size(237, 22);
         keyMappingToolStripMenuItem.Text = "Key Mapping";
         keyMappingToolStripMenuItem.Click += KeyMappingToolStripMenuItem_Click;
         // 
         // toolStripSeparator5
         // 
         toolStripSeparator5.Name = "toolStripSeparator5";
         toolStripSeparator5.Size = new Size(234, 6);
         // 
         // clearCacheToolStripMenuItem
         // 
         clearCacheToolStripMenuItem.Name = "clearCacheToolStripMenuItem";
         clearCacheToolStripMenuItem.Size = new Size(237, 22);
         clearCacheToolStripMenuItem.Text = "Clear Cache";
         clearCacheToolStripMenuItem.Click += ClearCacheToolStripMenuItem_Click;
         // 
         // clearAllUserDefinedFilesToolStripMenuItem
         // 
         clearAllUserDefinedFilesToolStripMenuItem.Name = "clearAllUserDefinedFilesToolStripMenuItem";
         clearAllUserDefinedFilesToolStripMenuItem.Size = new Size(237, 22);
         clearAllUserDefinedFilesToolStripMenuItem.Text = "Clear all user defined files";
         // 
         // AlwayRenderProvinceOutlineSetting
         // 
         AlwayRenderProvinceOutlineSetting.Checked = true;
         AlwayRenderProvinceOutlineSetting.CheckState = CheckState.Checked;
         AlwayRenderProvinceOutlineSetting.Name = "AlwayRenderProvinceOutlineSetting";
         AlwayRenderProvinceOutlineSetting.Size = new Size(237, 22);
         AlwayRenderProvinceOutlineSetting.Text = "Always render province outline";
         AlwayRenderProvinceOutlineSetting.Click += AlwaysRenderProvinceOutline_Click;
         // 
         // mapmodesToolStripMenuItem
         // 
         mapmodesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mapmodeSelectorToolStripMenuItem, ManageMapModesMenu, resetAllMapmodesToolStripMenuItem });
         mapmodesToolStripMenuItem.Name = "mapmodesToolStripMenuItem";
         mapmodesToolStripMenuItem.Size = new Size(79, 20);
         mapmodesToolStripMenuItem.Text = "Mapmodes";
         // 
         // mapmodeSelectorToolStripMenuItem
         // 
         mapmodeSelectorToolStripMenuItem.Name = "mapmodeSelectorToolStripMenuItem";
         mapmodeSelectorToolStripMenuItem.Size = new Size(180, 22);
         mapmodeSelectorToolStripMenuItem.Text = "Mapmode Selector";
         // 
         // ManageMapModesMenu
         // 
         ManageMapModesMenu.Name = "ManageMapModesMenu";
         ManageMapModesMenu.Size = new Size(180, 22);
         ManageMapModesMenu.Text = "Manage Mapmodes";
         ManageMapModesMenu.Click += ManageMapModesMenu_Click;
         // 
         // resetAllMapmodesToolStripMenuItem
         // 
         resetAllMapmodesToolStripMenuItem.Name = "resetAllMapmodesToolStripMenuItem";
         resetAllMapmodesToolStripMenuItem.Size = new Size(180, 22);
         resetAllMapmodesToolStripMenuItem.Text = "Reset all Mapmodes";
         // 
         // tooltipToolStripMenuItem
         // 
         tooltipToolStripMenuItem.Name = "tooltipToolStripMenuItem";
         tooltipToolStripMenuItem.Size = new Size(55, 20);
         tooltipToolStripMenuItem.Text = "Tooltip";
         tooltipToolStripMenuItem.Click += TooltipToolStripMenuItem_Click;
         // 
         // StatisticsTollStripMenuItem
         // 
         StatisticsTollStripMenuItem.Name = "StatisticsTollStripMenuItem";
         StatisticsTollStripMenuItem.Size = new Size(65, 20);
         StatisticsTollStripMenuItem.Text = "Statistics";
         StatisticsTollStripMenuItem.Click += StatisticsTollStripMenuItem_Click;
         // 
         // selectionModeToolStripMenuItem
         // 
         selectionModeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ProvinceSelectionModeMenuItem, AreaSelectionModeMenuItem, RegionSelectionModeMenuItem, OwnerSelectionModeMenuItem });
         selectionModeToolStripMenuItem.Name = "selectionModeToolStripMenuItem";
         selectionModeToolStripMenuItem.Size = new Size(101, 20);
         selectionModeToolStripMenuItem.Text = "Selection Mode";
         // 
         // ProvinceSelectionModeMenuItem
         // 
         ProvinceSelectionModeMenuItem.Checked = true;
         ProvinceSelectionModeMenuItem.CheckState = CheckState.Checked;
         ProvinceSelectionModeMenuItem.Name = "ProvinceSelectionModeMenuItem";
         ProvinceSelectionModeMenuItem.Size = new Size(180, 22);
         ProvinceSelectionModeMenuItem.Text = "Province";
         ProvinceSelectionModeMenuItem.Click += ProvinceSelectionModeMenuItem_Click;
         // 
         // AreaSelectionModeMenuItem
         // 
         AreaSelectionModeMenuItem.Name = "AreaSelectionModeMenuItem";
         AreaSelectionModeMenuItem.Size = new Size(180, 22);
         AreaSelectionModeMenuItem.Text = "Area";
         AreaSelectionModeMenuItem.Click += AreaSelectionModeMenuItem_Click;
         // 
         // RegionSelectionModeMenuItem
         // 
         RegionSelectionModeMenuItem.Name = "RegionSelectionModeMenuItem";
         RegionSelectionModeMenuItem.Size = new Size(180, 22);
         RegionSelectionModeMenuItem.Text = "Region";
         RegionSelectionModeMenuItem.Click += RegionSelectionModeMenuItem_Click;
         // 
         // OwnerSelectionModeMenuItem
         // 
         OwnerSelectionModeMenuItem.Name = "OwnerSelectionModeMenuItem";
         OwnerSelectionModeMenuItem.Size = new Size(180, 22);
         OwnerSelectionModeMenuItem.Text = "Owner (Country)";
         OwnerSelectionModeMenuItem.Click += OwnerSelectionModeMenuItem_Click;
         // 
         // helpToolStripMenuItem
         // 
         helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         helpToolStripMenuItem.Size = new Size(44, 20);
         helpToolStripMenuItem.Text = "Help";
         helpToolStripMenuItem.Click += Help_MenuItem_Click;
         // 
         // ZoomInButton
         // 
         ZoomInButton.Location = new Point(12, 112);
         ZoomInButton.Name = "ZoomInButton";
         ZoomInButton.Size = new Size(93, 23);
         ZoomInButton.TabIndex = 10;
         ZoomInButton.Text = "Zoom in";
         ZoomInButton.UseVisualStyleBackColor = true;
         ZoomInButton.Click += ZoomInButton_Click;
         // 
         // ZoomOutButton
         // 
         ZoomOutButton.Location = new Point(111, 112);
         ZoomOutButton.Name = "ZoomOutButton";
         ZoomOutButton.Size = new Size(90, 23);
         ZoomOutButton.TabIndex = 11;
         ZoomOutButton.Text = "Zoom out";
         ZoomOutButton.UseVisualStyleBackColor = true;
         ZoomOutButton.Click += ZoomOutButton_Click;
         // 
         // panel1
         // 
         panel1.Controls.Add(Map);
         panel1.Location = new Point(207, 29);
         panel1.Name = "panel1";
         panel1.Size = new Size(940, 720);
         panel1.TabIndex = 12;
         // 
         // MapModeButtons
         // 
         MapModeButtons.ColumnCount = 1;
         MapModeButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
         MapModeButtons.Location = new Point(6, 22);
         MapModeButtons.Name = "MapModeButtons";
         MapModeButtons.RowCount = 1;
         MapModeButtons.RowStyles.Add(new RowStyle());
         MapModeButtons.Size = new Size(186, 261);
         MapModeButtons.TabIndex = 13;
         // 
         // MainRightClickMenu
         // 
         MainRightClickMenu.Items.AddRange(new ToolStripItem[] { OpenProvinceFileContext, OpenCountryFileContext, toolStripSeparator1, QuickSelectionContext, AdvancedSelectionMenuContext, toolStripSeparator3, AddToContextCollection, AddClaimsCoresContext, AddModifierMenuContext, toolStripSeparator4, RemoveCoresClaimsContext, RemoveModifierMenuContext });
         MainRightClickMenu.Name = "contextMenuStrip1";
         MainRightClickMenu.Size = new Size(216, 220);
         MainRightClickMenu.Opening += MainRightClickMenu_Opening;
         // 
         // OpenProvinceFileContext
         // 
         OpenProvinceFileContext.Name = "OpenProvinceFileContext";
         OpenProvinceFileContext.Size = new Size(215, 22);
         OpenProvinceFileContext.Text = "Open Province File";
         // 
         // OpenCountryFileContext
         // 
         OpenCountryFileContext.Name = "OpenCountryFileContext";
         OpenCountryFileContext.Size = new Size(215, 22);
         OpenCountryFileContext.Text = "Open Country file";
         // 
         // toolStripSeparator1
         // 
         toolStripSeparator1.Name = "toolStripSeparator1";
         toolStripSeparator1.Size = new Size(212, 6);
         // 
         // QuickSelectionContext
         // 
         QuickSelectionContext.DropDownItems.AddRange(new ToolStripItem[] { SelectOwnerContext, SelectAllCoresContext, SelectAreaContext, SelectRegionContext, SelectTradenodeContext, SelectSuperRegionContext, SelectContinentContext, SelectReligionContext, CultureSelectionMenuContext });
         QuickSelectionContext.Name = "QuickSelectionContext";
         QuickSelectionContext.Size = new Size(215, 22);
         QuickSelectionContext.Text = "Quick Selection";
         // 
         // SelectOwnerContext
         // 
         SelectOwnerContext.Name = "SelectOwnerContext";
         SelectOwnerContext.Size = new Size(172, 22);
         SelectOwnerContext.Text = "Select Owner";
         SelectOwnerContext.Click += SelectOwnerContext_Click;
         // 
         // SelectAllCoresContext
         // 
         SelectAllCoresContext.DropDownItems.AddRange(new ToolStripItem[] { ProvinceCoresContext, CoreSelectionConfirmContext });
         SelectAllCoresContext.Name = "SelectAllCoresContext";
         SelectAllCoresContext.Size = new Size(172, 22);
         SelectAllCoresContext.Text = "Select all cores";
         // 
         // ProvinceCoresContext
         // 
         ProvinceCoresContext.DropDownStyle = ComboBoxStyle.DropDownList;
         ProvinceCoresContext.Name = "ProvinceCoresContext";
         ProvinceCoresContext.Size = new Size(121, 23);
         ProvinceCoresContext.Paint += ProvinceCoresContext_Paint;
         // 
         // CoreSelectionConfirmContext
         // 
         CoreSelectionConfirmContext.Name = "CoreSelectionConfirmContext";
         CoreSelectionConfirmContext.Size = new Size(181, 22);
         CoreSelectionConfirmContext.Text = "Confirm Selection";
         CoreSelectionConfirmContext.Click += CoreSelectionConfirmContext_Click;
         // 
         // SelectAreaContext
         // 
         SelectAreaContext.Name = "SelectAreaContext";
         SelectAreaContext.Size = new Size(172, 22);
         SelectAreaContext.Text = "Select Area";
         SelectAreaContext.Click += SelectAreaContext_Click;
         // 
         // SelectRegionContext
         // 
         SelectRegionContext.Name = "SelectRegionContext";
         SelectRegionContext.Size = new Size(172, 22);
         SelectRegionContext.Text = "Select Region";
         // 
         // SelectTradenodeContext
         // 
         SelectTradenodeContext.Name = "SelectTradenodeContext";
         SelectTradenodeContext.Size = new Size(172, 22);
         SelectTradenodeContext.Text = "Select Tradenode";
         // 
         // SelectSuperRegionContext
         // 
         SelectSuperRegionContext.Name = "SelectSuperRegionContext";
         SelectSuperRegionContext.Size = new Size(172, 22);
         SelectSuperRegionContext.Text = "Select Superregion";
         // 
         // SelectContinentContext
         // 
         SelectContinentContext.Name = "SelectContinentContext";
         SelectContinentContext.Size = new Size(172, 22);
         SelectContinentContext.Text = "Select Continent";
         // 
         // SelectReligionContext
         // 
         SelectReligionContext.DropDownItems.AddRange(new ToolStripItem[] { ReligionSelectionContext });
         SelectReligionContext.Name = "SelectReligionContext";
         SelectReligionContext.Size = new Size(172, 22);
         SelectReligionContext.Text = "Select Religion";
         // 
         // ReligionSelectionContext
         // 
         ReligionSelectionContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         ReligionSelectionContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         ReligionSelectionContext.Name = "ReligionSelectionContext";
         ReligionSelectionContext.Size = new Size(121, 23);
         ReligionSelectionContext.Text = "e.g. catholic";
         ReligionSelectionContext.ToolTipText = "Enter religion here";
         // 
         // CultureSelectionMenuContext
         // 
         CultureSelectionMenuContext.DropDownItems.AddRange(new ToolStripItem[] { SelectCultureContext });
         CultureSelectionMenuContext.Name = "CultureSelectionMenuContext";
         CultureSelectionMenuContext.Size = new Size(172, 22);
         CultureSelectionMenuContext.Text = "Select Culture";
         // 
         // SelectCultureContext
         // 
         SelectCultureContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         SelectCultureContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         SelectCultureContext.Name = "SelectCultureContext";
         SelectCultureContext.Size = new Size(121, 23);
         SelectCultureContext.Text = "e.g. saxon";
         SelectCultureContext.ToolTipText = "enter culture here";
         // 
         // AdvancedSelectionMenuContext
         // 
         AdvancedSelectionMenuContext.DropDownItems.AddRange(new ToolStripItem[] { CustomTriggerSelectionContext, RandomSelectionContext });
         AdvancedSelectionMenuContext.Name = "AdvancedSelectionMenuContext";
         AdvancedSelectionMenuContext.Size = new Size(215, 22);
         AdvancedSelectionMenuContext.Text = "Advanced Selection";
         // 
         // CustomTriggerSelectionContext
         // 
         CustomTriggerSelectionContext.Name = "CustomTriggerSelectionContext";
         CustomTriggerSelectionContext.Size = new Size(278, 22);
         CustomTriggerSelectionContext.Text = "Custom trigger selection";
         CustomTriggerSelectionContext.ToolTipText = "Selects all provinces applicable to a custom trigger";
         // 
         // RandomSelectionContext
         // 
         RandomSelectionContext.Name = "RandomSelectionContext";
         RandomSelectionContext.Size = new Size(278, 22);
         RandomSelectionContext.Text = "Select random province from selection";
         // 
         // toolStripSeparator3
         // 
         toolStripSeparator3.Name = "toolStripSeparator3";
         toolStripSeparator3.Size = new Size(212, 6);
         // 
         // AddToContextCollection
         // 
         AddToContextCollection.DropDownItems.AddRange(new ToolStripItem[] { AddToAreaContext, AddToRegionContext, AddToTradeNodeContext, AddToSuperRegionContext, AddToContinentContext, AddToProvinceGroupContext });
         AddToContextCollection.Name = "AddToContextCollection";
         AddToContextCollection.Size = new Size(215, 22);
         AddToContextCollection.Text = "Add to Collection";
         AddToContextCollection.ToolTipText = "Remove the selected provinces from all other collections of the selected type";
         // 
         // AddToAreaContext
         // 
         AddToAreaContext.Name = "AddToAreaContext";
         AddToAreaContext.Size = new Size(194, 22);
         AddToAreaContext.Text = "Add to Area";
         // 
         // AddToRegionContext
         // 
         AddToRegionContext.Name = "AddToRegionContext";
         AddToRegionContext.Size = new Size(194, 22);
         AddToRegionContext.Text = "Add to Region";
         // 
         // AddToTradeNodeContext
         // 
         AddToTradeNodeContext.Name = "AddToTradeNodeContext";
         AddToTradeNodeContext.Size = new Size(194, 22);
         AddToTradeNodeContext.Text = "Add to Tradenode";
         // 
         // AddToSuperRegionContext
         // 
         AddToSuperRegionContext.Name = "AddToSuperRegionContext";
         AddToSuperRegionContext.Size = new Size(194, 22);
         AddToSuperRegionContext.Text = "Add to Superregion";
         // 
         // AddToContinentContext
         // 
         AddToContinentContext.Name = "AddToContinentContext";
         AddToContinentContext.Size = new Size(194, 22);
         AddToContinentContext.Text = "Add to Continent";
         // 
         // AddToProvinceGroupContext
         // 
         AddToProvinceGroupContext.Name = "AddToProvinceGroupContext";
         AddToProvinceGroupContext.Size = new Size(194, 22);
         AddToProvinceGroupContext.Text = "Add to province group";
         // 
         // AddClaimsCoresContext
         // 
         AddClaimsCoresContext.DropDownItems.AddRange(new ToolStripItem[] { AddCoreSelectionContext, AddPermaClaimContext, AddClaimContext });
         AddClaimsCoresContext.Name = "AddClaimsCoresContext";
         AddClaimsCoresContext.Size = new Size(215, 22);
         AddClaimsCoresContext.Text = "Add Cores / Claims";
         // 
         // AddCoreSelectionContext
         // 
         AddCoreSelectionContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         AddCoreSelectionContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         AddCoreSelectionContext.Name = "AddCoreSelectionContext";
         AddCoreSelectionContext.Size = new Size(121, 23);
         AddCoreSelectionContext.Sorted = true;
         AddCoreSelectionContext.Text = "Core";
         AddCoreSelectionContext.ToolTipText = "Add a Core";
         // 
         // AddPermaClaimContext
         // 
         AddPermaClaimContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         AddPermaClaimContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         AddPermaClaimContext.Name = "AddPermaClaimContext";
         AddPermaClaimContext.Size = new Size(121, 23);
         AddPermaClaimContext.Text = "Permanent claim";
         AddPermaClaimContext.ToolTipText = "Add a permanent claim";
         // 
         // AddClaimContext
         // 
         AddClaimContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         AddClaimContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         AddClaimContext.Name = "AddClaimContext";
         AddClaimContext.Size = new Size(121, 23);
         AddClaimContext.Text = "Claim";
         AddClaimContext.ToolTipText = "Add a claim";
         // 
         // AddModifierMenuContext
         // 
         AddModifierMenuContext.DropDownItems.AddRange(new ToolStripItem[] { AddProvinceModifierSelectionContext, AddCountryModifierSelectionContext });
         AddModifierMenuContext.Name = "AddModifierMenuContext";
         AddModifierMenuContext.Size = new Size(215, 22);
         AddModifierMenuContext.Text = "Add Modifier (Duration -1)";
         // 
         // AddProvinceModifierSelectionContext
         // 
         AddProvinceModifierSelectionContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         AddProvinceModifierSelectionContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         AddProvinceModifierSelectionContext.Name = "AddProvinceModifierSelectionContext";
         AddProvinceModifierSelectionContext.Size = new Size(121, 23);
         AddProvinceModifierSelectionContext.Text = "Province scope";
         AddProvinceModifierSelectionContext.ToolTipText = "Add a province modifier";
         // 
         // AddCountryModifierSelectionContext
         // 
         AddCountryModifierSelectionContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         AddCountryModifierSelectionContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         AddCountryModifierSelectionContext.Name = "AddCountryModifierSelectionContext";
         AddCountryModifierSelectionContext.Size = new Size(121, 23);
         AddCountryModifierSelectionContext.Text = "Country scope";
         AddCountryModifierSelectionContext.ToolTipText = "Add a country modifier";
         // 
         // toolStripSeparator4
         // 
         toolStripSeparator4.Name = "toolStripSeparator4";
         toolStripSeparator4.Size = new Size(212, 6);
         // 
         // RemoveCoresClaimsContext
         // 
         RemoveCoresClaimsContext.DropDownItems.AddRange(new ToolStripItem[] { RemoveCoresSelectionContext, RemovePermaClaimsContext, RemoveClaimContext });
         RemoveCoresClaimsContext.Name = "RemoveCoresClaimsContext";
         RemoveCoresClaimsContext.Size = new Size(215, 22);
         RemoveCoresClaimsContext.Text = "Remove Cores / Claims";
         // 
         // RemoveCoresSelectionContext
         // 
         RemoveCoresSelectionContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         RemoveCoresSelectionContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         RemoveCoresSelectionContext.Name = "RemoveCoresSelectionContext";
         RemoveCoresSelectionContext.Size = new Size(121, 23);
         RemoveCoresSelectionContext.Text = "Core";
         RemoveCoresSelectionContext.ToolTipText = "Remove a core";
         // 
         // RemovePermaClaimsContext
         // 
         RemovePermaClaimsContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         RemovePermaClaimsContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         RemovePermaClaimsContext.Name = "RemovePermaClaimsContext";
         RemovePermaClaimsContext.Size = new Size(121, 23);
         RemovePermaClaimsContext.Text = "Permanent Claim";
         RemovePermaClaimsContext.ToolTipText = "Remove permanent claim";
         // 
         // RemoveClaimContext
         // 
         RemoveClaimContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         RemoveClaimContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         RemoveClaimContext.Name = "RemoveClaimContext";
         RemoveClaimContext.Size = new Size(121, 23);
         RemoveClaimContext.Text = "Claim";
         RemoveClaimContext.ToolTipText = "Remove claim";
         // 
         // RemoveModifierMenuContext
         // 
         RemoveModifierMenuContext.DropDownItems.AddRange(new ToolStripItem[] { RemoveProvinceModifierContext, RemoveCountryModifierContext });
         RemoveModifierMenuContext.Name = "RemoveModifierMenuContext";
         RemoveModifierMenuContext.Size = new Size(215, 22);
         RemoveModifierMenuContext.Text = "Remove Modifier";
         // 
         // RemoveProvinceModifierContext
         // 
         RemoveProvinceModifierContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         RemoveProvinceModifierContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         RemoveProvinceModifierContext.Name = "RemoveProvinceModifierContext";
         RemoveProvinceModifierContext.Size = new Size(121, 23);
         RemoveProvinceModifierContext.Text = "Province scope";
         RemoveProvinceModifierContext.ToolTipText = "Remove province modifier";
         // 
         // RemoveCountryModifierContext
         // 
         RemoveCountryModifierContext.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
         RemoveCountryModifierContext.AutoCompleteSource = AutoCompleteSource.ListItems;
         RemoveCountryModifierContext.Name = "RemoveCountryModifierContext";
         RemoveCountryModifierContext.Size = new Size(121, 23);
         RemoveCountryModifierContext.Text = "Country scope";
         RemoveCountryModifierContext.ToolTipText = "Remove country modifier";
         // 
         // ColorChangeScopeContext
         // 
         ColorChangeScopeContext.DropDownStyle = ComboBoxStyle.DropDownList;
         ColorChangeScopeContext.Items.AddRange(new object[] { "Area", "Region", "Superregion", "Tradenode", "Country" });
         ColorChangeScopeContext.Name = "ColorChangeScopeContext";
         ColorChangeScopeContext.Size = new Size(121, 23);
         // 
         // toolStripMenuItem1
         // 
         toolStripMenuItem1.Name = "toolStripMenuItem1";
         toolStripMenuItem1.Size = new Size(217, 22);
         toolStripMenuItem1.Text = "SelectColorChangeContext";
         // 
         // toolStripSeparator2
         // 
         toolStripSeparator2.Name = "toolStripSeparator2";
         toolStripSeparator2.Size = new Size(214, 6);
         // 
         // ConfirmColorChangeContext
         // 
         ConfirmColorChangeContext.Name = "ConfirmColorChangeContext";
         ConfirmColorChangeContext.Size = new Size(217, 22);
         ConfirmColorChangeContext.Text = "Confirm";
         // 
         // groupBox1
         // 
         groupBox1.Controls.Add(MapModeButtons);
         groupBox1.Location = new Point(5, 141);
         groupBox1.Name = "groupBox1";
         groupBox1.Size = new Size(198, 291);
         groupBox1.TabIndex = 16;
         groupBox1.TabStop = false;
         groupBox1.Text = "Map Modes";
         // 
         // debugToolStripMenuItem
         // 
         debugToolStripMenuItem.Name = "debugToolStripMenuItem";
         debugToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.D;
         debugToolStripMenuItem.Size = new Size(237, 22);
         debugToolStripMenuItem.Text = "Debug";
         debugToolStripMenuItem.Click += debugToolStripMenuItem_Click;
         // 
         // MainWindow
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(1163, 761);
         ContextMenuStrip = MainRightClickMenu;
         Controls.Add(groupBox1);
         Controls.Add(panel1);
         Controls.Add(ZoomOutButton);
         Controls.Add(ZoomInButton);
         Controls.Add(zoomTrackBar);
         Controls.Add(label2);
         Controls.Add(label1);
         Controls.Add(StepsizeMove);
         Controls.Add(menuStrip1);
         DoubleBuffered = true;
         KeyPreview = true;
         MainMenuStrip = menuStrip1;
         Name = "MainWindow";
         Opacity = 0D;
         Text = "Main Window";
         Load += MainWindow_Load;
         KeyDown += MainWindow_KeyDown;
         ((System.ComponentModel.ISupportInitialize)Map).EndInit();
         ((System.ComponentModel.ISupportInitialize)StepsizeMove).EndInit();
         ((System.ComponentModel.ISupportInitialize)zoomTrackBar).EndInit();
         menuStrip1.ResumeLayout(false);
         menuStrip1.PerformLayout();
         panel1.ResumeLayout(false);
         panel1.PerformLayout();
         MainRightClickMenu.ResumeLayout(false);
         groupBox1.ResumeLayout(false);
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion

      public PictureBox Map;
      private TrackBar StepsizeMove;
      private Label label1;
      private Label label2;
      private TrackBar zoomTrackBar;
      private ToolTip _tt;
      private MenuStrip menuStrip1;
      private ToolStripMenuItem settingsToolStripMenuItem;
      private ToolStripMenuItem mapmodesToolStripMenuItem;
      private ToolStripMenuItem tooltipToolStripMenuItem;
      private ToolStripMenuItem clearAllUserDefinedFilesToolStripMenuItem;
      private ToolStripMenuItem mapmodeSelectorToolStripMenuItem;
      private ToolStripMenuItem ManageMapModesMenu;
      private ToolStripMenuItem resetAllMapmodesToolStripMenuItem;
      private ToolStripMenuItem helpToolStripMenuItem;
      private Button ZoomInButton;
      private Button ZoomOutButton;
      private Panel panel1;
      public TableLayoutPanel MapModeButtons;
      private ToolStripMenuItem toolStripMenuItem4;
      private ToolStripMenuItem toolStripMenuItem5;
      private ToolStripMenuItem toolStripMenuItem6;
      private ToolStripMenuItem toolStripMenuItem7;
      private ContextMenuStrip MainRightClickMenu;
      private ToolStripMenuItem OpenProvinceFileContext;
      private ToolStripMenuItem OpenCountryFileContext;
      private ToolStripSeparator toolStripSeparator1;
      private ToolStripComboBox ColorChangeScopeContext;
      private ToolStripMenuItem toolStripMenuItem1;
      private ToolStripSeparator toolStripSeparator2;
      private ToolStripMenuItem ConfirmColorChangeContext;
      private ToolStripMenuItem QuickSelectionContext;
      private ToolStripMenuItem SelectOwnerContext;
      private ToolStripMenuItem SelectAllCoresContext;
      private ToolStripComboBox ProvinceCoresContext;
      private ToolStripMenuItem SelectAreaContext;
      private ToolStripMenuItem SelectRegionContext;
      private ToolStripMenuItem SelectTradenodeContext;
      private ToolStripMenuItem SelectSuperRegionContext;
      private ToolStripMenuItem SelectContinentContext;
      private ToolStripSeparator toolStripSeparator3;
      private ToolStripMenuItem AddToContextCollection;
      private ToolStripMenuItem AddToAreaContext;
      private ToolStripMenuItem AddToRegionContext;
      private ToolStripMenuItem AddToTradeNodeContext;
      private ToolStripMenuItem AddToSuperRegionContext;
      private ToolStripMenuItem AddToContinentContext;
      private ToolStripMenuItem AddClaimsCoresContext;
      private ToolStripComboBox AddCoreSelectionContext;
      private ToolStripComboBox AddPermaClaimContext;
      private ToolStripComboBox AddClaimContext;
      private ToolStripMenuItem AddModifierMenuContext;
      private ToolStripComboBox AddProvinceModifierSelectionContext;
      private ToolStripComboBox AddCountryModifierSelectionContext;
      private ToolStripSeparator toolStripSeparator4;
      private ToolStripMenuItem RemoveCoresClaimsContext;
      private ToolStripComboBox RemoveCoresSelectionContext;
      private ToolStripComboBox RemovePermaClaimsContext;
      private ToolStripComboBox RemoveClaimContext;
      private ToolStripMenuItem RemoveModifierMenuContext;
      private ToolStripComboBox RemoveProvinceModifierContext;
      private ToolStripComboBox RemoveCountryModifierContext;
      private ToolStripMenuItem StatisticsTollStripMenuItem;
      private ToolStripMenuItem SelectReligionContext;
      private ToolStripComboBox ReligionSelectionContext;
      private ToolStripMenuItem CultureSelectionMenuContext;
      private ToolStripComboBox SelectCultureContext;
      private ToolStripMenuItem AdvancedSelectionMenuContext;
      private ToolStripMenuItem CustomTriggerSelectionContext;
      private ToolStripMenuItem RandomSelectionContext;
      private ToolStripMenuItem AddToProvinceGroupContext;
      private ToolStripMenuItem ReloadLocalisationToolStripMenuItem;
      private GroupBox groupBox1;
      private ToolStripMenuItem clearCacheToolStripMenuItem;
      private ToolStripMenuItem keyMappingToolStripMenuItem;
      private ToolStripSeparator toolStripSeparator5;
      private ToolStripMenuItem AlwayRenderProvinceOutlineSetting;
      private ToolStripMenuItem CoreSelectionConfirmContext;
      private ToolStripMenuItem selectionModeToolStripMenuItem;
      private ToolStripMenuItem ProvinceSelectionModeMenuItem;
      private ToolStripMenuItem AreaSelectionModeMenuItem;
      private ToolStripMenuItem RegionSelectionModeMenuItem;
      private ToolStripMenuItem OwnerSelectionModeMenuItem;
      private ToolStripMenuItem debugToolStripMenuItem;
   }
}