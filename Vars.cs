using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public static class Vars
    {
        public static float ZoomLevel = 1;
        public static Bitmap Map;

        public static Dictionary<int, Province> provinces = new ();
        public static Dictionary<string, Color> notOnMapProvs = new ();
        public static Dictionary<Color, int> colorIds = new ();

        public static Province LastProvince;
        public static Province CurProvince;
        public static List<Province> SelecteProvinces = new ();

        public static string appPath = "";
        public static string ModFolder = "";
        public static string VanillaFolder = "";

        // FORMS

        public static MainWindow? MainWindow;
        public static LoadingScreen? LoadingForm;
    }
}
