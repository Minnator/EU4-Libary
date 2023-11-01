namespace EU4_Parse_Lib
{
   partial class LoadingScreen
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
         LoadGetVanillaPath = new Button();
         LoadGetVanillaPathIn = new TextBox();
         label2 = new Label();
         button1 = new Button();
         LoadModFolderPathIn = new TextBox();
         label1 = new Label();
         LoadAllFilesButton = new Button();
         LoadingMapDataRB = new RadioButton();
         CreateStatistics = new RadioButton();
         groupBox1 = new GroupBox();
         button2 = new Button();
         ProgressBox = new TextBox();
         groupBox2 = new GroupBox();
         LoadErrorLog = new TextBox();
         groupBox1.SuspendLayout();
         groupBox2.SuspendLayout();
         SuspendLayout();
         // 
         // LoadGetVanillaPath
         // 
         LoadGetVanillaPath.Location = new Point(532, 35);
         LoadGetVanillaPath.Name = "LoadGetVanillaPath";
         LoadGetVanillaPath.Size = new Size(83, 23);
         LoadGetVanillaPath.TabIndex = 13;
         LoadGetVanillaPath.Text = "Select Folder";
         LoadGetVanillaPath.UseVisualStyleBackColor = true;
         LoadGetVanillaPath.Click += LoadGetVanillaPath_Click;
         // 
         // LoadGetVanillaPathIn
         // 
         LoadGetVanillaPathIn.Location = new Point(152, 35);
         LoadGetVanillaPathIn.Name = "LoadGetVanillaPathIn";
         LoadGetVanillaPathIn.Size = new Size(374, 23);
         LoadGetVanillaPathIn.TabIndex = 12;
         LoadGetVanillaPathIn.Text = "S:\\SteamLibrary\\steamapps\\common\\Europa Universalis IV";
         LoadGetVanillaPathIn.TextChanged += LoadGetVanillaPathIn_TextChanged;
         // 
         // label2
         // 
         label2.AutoSize = true;
         label2.Location = new Point(12, 38);
         label2.Name = "label2";
         label2.Size = new Size(134, 15);
         label2.TabIndex = 11;
         label2.Text = "Enter Vanilla Folder Path";
         // 
         // button1
         // 
         button1.Location = new Point(532, 6);
         button1.Name = "button1";
         button1.Size = new Size(83, 23);
         button1.TabIndex = 10;
         button1.Text = "Select Folder";
         button1.UseVisualStyleBackColor = true;
         button1.Click += SelectModFolder_Click;
         // 
         // LoadModFolderPathIn
         // 
         LoadModFolderPathIn.Location = new Point(152, 6);
         LoadModFolderPathIn.Name = "LoadModFolderPathIn";
         LoadModFolderPathIn.Size = new Size(374, 23);
         LoadModFolderPathIn.TabIndex = 9;
         LoadModFolderPathIn.TextChanged += LoadModFolderPathIn_TextChanged;
         // 
         // label1
         // 
         label1.AutoSize = true;
         label1.Location = new Point(12, 9);
         label1.Name = "label1";
         label1.Size = new Size(125, 15);
         label1.TabIndex = 8;
         label1.Text = "Enter Mod Folder Path";
         // 
         // LoadAllFilesButton
         // 
         LoadAllFilesButton.Location = new Point(12, 67);
         LoadAllFilesButton.Name = "LoadAllFilesButton";
         LoadAllFilesButton.Size = new Size(603, 23);
         LoadAllFilesButton.TabIndex = 14;
         LoadAllFilesButton.Text = "Continue";
         LoadAllFilesButton.UseVisualStyleBackColor = true;
         LoadAllFilesButton.Click += LoadAllFilesButton_Click;
         // 
         // LoadingMapDataRB
         // 
         LoadingMapDataRB.AutoSize = true;
         LoadingMapDataRB.Location = new Point(6, 22);
         LoadingMapDataRB.Name = "LoadingMapDataRB";
         LoadingMapDataRB.Size = new Size(122, 19);
         LoadingMapDataRB.TabIndex = 15;
         LoadingMapDataRB.Text = "Loading Map Data";
         LoadingMapDataRB.UseVisualStyleBackColor = true;
         // 
         // CreateStatistics
         // 
         CreateStatistics.AutoSize = true;
         CreateStatistics.Location = new Point(477, 22);
         CreateStatistics.Name = "CreateStatistics";
         CreateStatistics.Size = new Size(119, 19);
         CreateStatistics.TabIndex = 16;
         CreateStatistics.Text = "Creating Statistics";
         CreateStatistics.UseVisualStyleBackColor = true;
         // 
         // groupBox1
         // 
         groupBox1.Controls.Add(button2);
         groupBox1.Controls.Add(LoadingMapDataRB);
         groupBox1.Controls.Add(CreateStatistics);
         groupBox1.Location = new Point(12, 96);
         groupBox1.Name = "groupBox1";
         groupBox1.Size = new Size(602, 322);
         groupBox1.TabIndex = 17;
         groupBox1.TabStop = false;
         groupBox1.Text = "Loading Steps";
         // 
         // button2
         // 
         button2.Location = new Point(136, 179);
         button2.Name = "button2";
         button2.Size = new Size(75, 23);
         button2.TabIndex = 17;
         button2.Text = "Debug";
         button2.UseVisualStyleBackColor = true;
         button2.Click += button2_Click;
         // 
         // ProgressBox
         // 
         ProgressBox.Location = new Point(12, 424);
         ProgressBox.Name = "ProgressBox";
         ProgressBox.Size = new Size(602, 23);
         ProgressBox.TabIndex = 18;
         ProgressBox.Text = "Loading";
         ProgressBox.TextAlign = HorizontalAlignment.Center;
         ProgressBox.TextChanged += ProgressBox_TextChanged;
         // 
         // groupBox2
         // 
         groupBox2.Controls.Add(LoadErrorLog);
         groupBox2.Location = new Point(12, 453);
         groupBox2.Name = "groupBox2";
         groupBox2.Size = new Size(602, 154);
         groupBox2.TabIndex = 19;
         groupBox2.TabStop = false;
         groupBox2.Text = "Error Log";
         // 
         // LoadErrorLog
         // 
         LoadErrorLog.Dock = DockStyle.Fill;
         LoadErrorLog.Location = new Point(3, 19);
         LoadErrorLog.Multiline = true;
         LoadErrorLog.Name = "LoadErrorLog";
         LoadErrorLog.Size = new Size(596, 132);
         LoadErrorLog.TabIndex = 0;
         // 
         // LoadingScreen
         // 
         AutoScaleDimensions = new SizeF(7F, 15F);
         AutoScaleMode = AutoScaleMode.Font;
         ClientSize = new Size(626, 619);
         Controls.Add(groupBox2);
         Controls.Add(ProgressBox);
         Controls.Add(groupBox1);
         Controls.Add(LoadAllFilesButton);
         Controls.Add(LoadGetVanillaPath);
         Controls.Add(LoadGetVanillaPathIn);
         Controls.Add(label2);
         Controls.Add(button1);
         Controls.Add(LoadModFolderPathIn);
         Controls.Add(label1);
         FormBorderStyle = FormBorderStyle.FixedSingle;
         MaximizeBox = false;
         MinimizeBox = false;
         Name = "LoadingScreen";
         Text = "LoadingScreen";
         FormClosed += LoadingScreen_FormClosed;
         groupBox1.ResumeLayout(false);
         groupBox1.PerformLayout();
         groupBox2.ResumeLayout(false);
         groupBox2.PerformLayout();
         ResumeLayout(false);
         PerformLayout();
      }

      #endregion
      private Button LoadGetVanillaPath;
      private TextBox LoadGetVanillaPathIn;
      private Label label2;
      private Button button1;
      private TextBox LoadModFolderPathIn;
      private Label label1;
      private Button LoadAllFilesButton;
      public RadioButton LoadingMapDataRB;
      public RadioButton CreateStatistics;
      private GroupBox groupBox1;
      public TextBox ProgressBox;
      private GroupBox groupBox2;
      public TextBox LoadErrorLog;
      private Button button2;
   }
}