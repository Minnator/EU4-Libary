using System.Windows.Forms;

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
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StepsizeMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zoomTrackBar).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Map
            // 
            Map.Location = new Point(331, 32);
            Map.Name = "Map";
            Map.Size = new Size(940, 720);
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
            RightButton.Location = new Point(269, 63);
            RightButton.Name = "RightButton";
            RightButton.Size = new Size(56, 23);
            RightButton.TabIndex = 1;
            RightButton.Text = "right";
            RightButton.UseVisualStyleBackColor = true;
            RightButton.Click += RightButton_Click;
            // 
            // DownButton
            // 
            DownButton.Location = new Point(239, 92);
            DownButton.Name = "DownButton";
            DownButton.Size = new Size(56, 23);
            DownButton.TabIndex = 2;
            DownButton.Text = "down";
            DownButton.UseVisualStyleBackColor = true;
            DownButton.Click += DownButton_Click;
            // 
            // LeftButton
            // 
            LeftButton.Location = new Point(207, 63);
            LeftButton.Name = "LeftButton";
            LeftButton.Size = new Size(56, 23);
            LeftButton.TabIndex = 3;
            LeftButton.Text = "left";
            LeftButton.UseVisualStyleBackColor = true;
            LeftButton.Click += LeftButton_Click;
            // 
            // UpButton
            // 
            UpButton.Location = new Point(239, 34);
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
            StepsizeMove.Location = new Point(12, 41);
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
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 6;
            label1.Text = "Stepize of map movement";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 67);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 7;
            label2.Text = "Zoom level";
            // 
            // zoomTrackBar
            // 
            zoomTrackBar.Enabled = false;
            zoomTrackBar.LargeChange = 2;
            zoomTrackBar.Location = new Point(12, 85);
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
            menuStrip1.Size = new Size(1284, 24);
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 761);
            Controls.Add(zoomTrackBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(StepsizeMove);
            Controls.Add(UpButton);
            Controls.Add(LeftButton);
            Controls.Add(DownButton);
            Controls.Add(RightButton);
            Controls.Add(Map);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Map).EndInit();
            ((System.ComponentModel.ISupportInitialize)StepsizeMove).EndInit();
            ((System.ComponentModel.ISupportInitialize)zoomTrackBar).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
    }
}