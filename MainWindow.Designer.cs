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
            Map = new PictureBox();
            RightButton = new Button();
            DownButton = new Button();
            LeftButton = new Button();
            UpButton = new Button();
            StepsizeMove = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            zoomTrackBar = new TrackBar();
            ((System.ComponentModel.ISupportInitialize)Map).BeginInit();
            ((System.ComponentModel.ISupportInitialize)StepsizeMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)zoomTrackBar).BeginInit();
            SuspendLayout();
            // 
            // Map
            // 
            Map.Location = new Point(331, 12);
            Map.Name = "Map";
            Map.Size = new Size(600, 600);
            Map.TabIndex = 0;
            Map.TabStop = false;
            Map.MouseClick += Map_MouseClick;
            // 
            // RightButton
            // 
            RightButton.Location = new Point(269, 43);
            RightButton.Name = "RightButton";
            RightButton.Size = new Size(56, 23);
            RightButton.TabIndex = 1;
            RightButton.Text = "right";
            RightButton.UseVisualStyleBackColor = true;
            RightButton.Click += RightButton_Click;
            // 
            // DownButton
            // 
            DownButton.Location = new Point(239, 72);
            DownButton.Name = "DownButton";
            DownButton.Size = new Size(56, 23);
            DownButton.TabIndex = 2;
            DownButton.Text = "down";
            DownButton.UseVisualStyleBackColor = true;
            DownButton.Click += DownButton_Click;
            // 
            // LeftButton
            // 
            LeftButton.Location = new Point(207, 43);
            LeftButton.Name = "LeftButton";
            LeftButton.Size = new Size(56, 23);
            LeftButton.TabIndex = 3;
            LeftButton.Text = "left";
            LeftButton.UseVisualStyleBackColor = true;
            LeftButton.Click += LeftButton_Click;
            // 
            // UpButton
            // 
            UpButton.Location = new Point(239, 14);
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
            StepsizeMove.Location = new Point(12, 21);
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
            label1.Location = new Point(12, 3);
            label1.Name = "label1";
            label1.Size = new Size(146, 15);
            label1.TabIndex = 6;
            label1.Text = "Stepize of map movement";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 7;
            label2.Text = "Zoom level";
            // 
            // zoomTrackBar
            // 
            zoomTrackBar.Enabled = false;
            zoomTrackBar.LargeChange = 2;
            zoomTrackBar.Location = new Point(12, 65);
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
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(943, 624);
            Controls.Add(zoomTrackBar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(StepsizeMove);
            Controls.Add(UpButton);
            Controls.Add(LeftButton);
            Controls.Add(DownButton);
            Controls.Add(RightButton);
            Controls.Add(Map);
            Name = "MainWindow";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)Map).EndInit();
            ((System.ComponentModel.ISupportInitialize)StepsizeMove).EndInit();
            ((System.ComponentModel.ISupportInitialize)zoomTrackBar).EndInit();
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
    }
}