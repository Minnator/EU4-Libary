namespace EU4_Parse_Lib
{
    public partial class WebBrowserForm : Form
    {
        public readonly WebBrowser WebBrowser = new();
        public WebBrowserForm()
        {
            InitializeComponent();
            WebBrowser.Dock = DockStyle.Fill;

            Controls.Add( WebBrowser );
        }
    }
}
