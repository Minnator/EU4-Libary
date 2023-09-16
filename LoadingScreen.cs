namespace EU4_Parse_Lib
{
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            Application.EnableVisualStyles();
            InitializeComponent();

            Saving.WriteLog(string.Empty, "TimeComplexity");

            Vars.AppPath = Application.ExecutablePath.Replace("EU4 Parse Lib.exe", "");
            Vars.DataPath = Path.Combine(Vars.AppPath, "data");
            Vars.User = Path.Combine(Vars.AppPath, "User");

            Vars.LoadingForm = this;
        }

        private void SelectModFolder_Click(object sender, EventArgs e)
        {
            Vars.ModFolder = FolderBrowsing.GetFolderPath("Please Select Mod Directory!");
            LoadModFolderPathIn.Text = Vars.ModFolder;
            LoadModFolderPathIn.SetCursorToEnd();
        }

        private void LoadModFolderPathIn_TextChanged(object sender, EventArgs e)
        {
            LoadModFolderPathIn.SelectAllOnFocus();
        }

        private void LoadGetVanillaPath_Click(object sender, EventArgs e)
        {
            Vars.VanillaFolder = FolderBrowsing.GetFolderPath("Please Select Vanilla Directory!");
            LoadGetVanillaPathIn.Text = Vars.VanillaFolder;
            LoadGetVanillaPathIn.SetCursorToEnd();
        }

        private void LoadGetVanillaPathIn_TextChanged(object sender, EventArgs e)
        {
            LoadGetVanillaPathIn.SelectAllOnFocus();
        }

        private void LoadAllFilesButton_Click(object sender, EventArgs e)
        {
            LoadAllFilesButton.Enabled = false;
            Vars.MainWindow = Gui.ShowForm<MainWindow>();
            //Vars.MainWindow.Location = (Point)new Size(Location.X + Width, Location.Y);
            ParseManager.LoadAll();
        }

        private void LoadingScreen_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ProgressBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vars.ManageMapmodes = Gui.ShowForm<ManageMapmodes>();
        }
    }
}
