using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4_Parse_Lib
{
    public static class ParseManager
    {
        public static void LoadAll()
        {
            Vars.LoadingForm.LoadingMapDataRB.BackColor = Color.Orange; 
            Vars.LoadingForm.ProgressBox.Text = "Loading Map Data";
            LoadMapData();
            //Vars.loadingForm.LoadingMapDataRB.ResetBackColor();
        }


        public static void LoadMapData()
        {
            Loading.LoadBitmap();
        }

    }
}
