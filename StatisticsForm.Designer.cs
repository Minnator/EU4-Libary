namespace EU4_Parse_Lib;

partial class StatisticsForm
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
        contextMenuStrip1 = new ContextMenuStrip(components);
        backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
        SuspendLayout();
        // 
        // contextMenuStrip1
        // 
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new Size(61, 4);
        // 
        // StatisticsForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1023, 711);
        Name = "StatisticsForm";
        Text = "StatisticsForm";
        ResumeLayout(false);
    }

    #endregion

    private ContextMenuStrip contextMenuStrip1;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
}