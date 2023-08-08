using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4_Parse_Lib
{
    internal class FolderBrowsing
    {

        public static string GetFolderPath(string title = "Please select a Folder.", string rootFolder = "")
        {
            var selectedFolderPath = string.Empty;
            using FolderBrowserDialog folderBrowserDialog = new ();
            // Set the title of the dialog
            folderBrowserDialog.Description = title;
            folderBrowserDialog.UseDescriptionForTitle = true;
            folderBrowserDialog.SelectedPath = rootFolder;
            // Show the dialog and capture the result
            var result = folderBrowserDialog.ShowDialog();
            
            if (result != DialogResult.OK) 
                return selectedFolderPath;
            // Get the selected folder path
            selectedFolderPath = folderBrowserDialog.SelectedPath;
            
            return selectedFolderPath;
        }

    }
}
