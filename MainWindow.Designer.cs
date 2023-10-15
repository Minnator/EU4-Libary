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
            RightButton = new Button();
            DownButton = new Button();
            LeftButton = new Button();
            UpButton = new Button();
            StepsizeMove = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            zoomTrackBar = new TrackBar();
            _tt = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            clearAllUserDefinedFilesToolStripMenuItem = new ToolStripMenuItem();
            mapmodesToolStripMenuItem = new ToolStripMenuItem();
            mapmodeSelectorToolStripMenuItem = new ToolStripMenuItem();
            ManageMapModesMenu = new ToolStripMenuItem();
            resetAllMapmodesToolStripMenuItem = new ToolStripMenuItem();
            tooltipToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            ZoomInButton = new Button();
            ZoomOutButton = new Button();
            panel1 = new Panel();
            MapModeButtons = new TableLayoutPanel();
            MainRightClickMenu = new ContextMenuStrip(components);
            OpenProvinceFileContext = new ToolStripMenuItem();
            OpenCountryFileContext = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ChangeColorContext = new ToolStripMenuItem();
            ColorChangeScopeContext = new ToolStripComboBox();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            ConfirmColorChangeContext = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StepsizeMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zoomTrackBar).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            MainRightClickMenu.SuspendLayout();
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
            // RightButton
            // 
            RightButton.Location = new Point(110, 170);
            RightButton.Name = "RightButton";
            RightButton.Size = new Size(56, 23);
            RightButton.TabIndex = 1;
            RightButton.Text = "right";
            RightButton.UseVisualStyleBackColor = true;
            RightButton.Click += RightButton_Click;
            // 
            // DownButton
            // 
            DownButton.Location = new Point(80, 202);
            DownButton.Name = "DownButton";
            DownButton.Size = new Size(56, 23);
            DownButton.TabIndex = 2;
            DownButton.Text = "down";
            DownButton.UseVisualStyleBackColor = true;
            DownButton.Click += DownButton_Click;
            // 
            // LeftButton
            // 
            LeftButton.Location = new Point(48, 170);
            LeftButton.Name = "LeftButton";
            LeftButton.Size = new Size(56, 23);
            LeftButton.TabIndex = 3;
            LeftButton.Text = "left";
            LeftButton.UseVisualStyleBackColor = true;
            LeftButton.Click += LeftButton_Click;
            // 
            // UpButton
            // 
            UpButton.Location = new Point(80, 136);
            UpButton.Name = "UpButton";
            UpButton.Size = new Size(56, 23);
            UpButton.TabIndex = 4;
            UpButton.Text = "up";
            UpButton.UseVisualStyleBackColor = true;
            UpButton.Click += UpButton_Click;
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
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, mapmodesToolStripMenuItem, tooltipToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1163, 24);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { clearAllUserDefinedFilesToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // clearAllUserDefinedFilesToolStripMenuItem
            // 
            clearAllUserDefinedFilesToolStripMenuItem.Name = "clearAllUserDefinedFilesToolStripMenuItem";
            clearAllUserDefinedFilesToolStripMenuItem.Size = new Size(208, 22);
            clearAllUserDefinedFilesToolStripMenuItem.Text = "Clear all user defined files";
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
            MapModeButtons.Location = new Point(12, 231);
            MapModeButtons.Name = "MapModeButtons";
            MapModeButtons.RowCount = 1;
            MapModeButtons.RowStyles.Add(new RowStyle());
            MapModeButtons.Size = new Size(189, 261);
            MapModeButtons.TabIndex = 13;
            // 
            // MainRightClickMenu
            // 
            MainRightClickMenu.Items.AddRange(new ToolStripItem[] { OpenProvinceFileContext, OpenCountryFileContext, toolStripSeparator1, ChangeColorContext });
            MainRightClickMenu.Name = "contextMenuStrip1";
            MainRightClickMenu.Size = new Size(174, 76);
            MainRightClickMenu.Opening += MainRightClickMenu_Opening;
            // 
            // OpenProvinceFileContext
            // 
            OpenProvinceFileContext.Name = "OpenProvinceFileContext";
            OpenProvinceFileContext.Size = new Size(173, 22);
            OpenProvinceFileContext.Text = "Open Province File";
            // 
            // OpenCountryFileContext
            // 
            OpenCountryFileContext.Name = "OpenCountryFileContext";
            OpenCountryFileContext.Size = new Size(173, 22);
            OpenCountryFileContext.Text = "Open Country file";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(170, 6);
            // 
            // ChangeColorContext
            // 
            ChangeColorContext.DropDownItems.AddRange(new ToolStripItem[] { ColorChangeScopeContext, toolStripMenuItem1, toolStripSeparator2, ConfirmColorChangeContext });
            ChangeColorContext.Name = "ChangeColorContext";
            ChangeColorContext.Size = new Size(173, 22);
            ChangeColorContext.Text = "Change Color";
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1163, 761);
            ContextMenuStrip = MainRightClickMenu;
            Controls.Add(MapModeButtons);
            Controls.Add(panel1);
            Controls.Add(ZoomOutButton);
            Controls.Add(ZoomInButton);
            Controls.Add(zoomTrackBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(StepsizeMove);
            Controls.Add(UpButton);
            Controls.Add(LeftButton);
            Controls.Add(DownButton);
            Controls.Add(RightButton);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public PictureBox Map;
        private Button RightButton;
        private Button DownButton;
        private Button LeftButton;
        private Button UpButton;
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
        private ToolStripMenuItem ChangeColorContext;
        private ToolStripComboBox ColorChangeScopeContext;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem ConfirmColorChangeContext;
    }
}