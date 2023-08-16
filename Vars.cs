using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public static class Vars
    {
        public static int TotalProvinces = 0;

        public static float ZoomLevel = 1;

        public static Stopwatch stopwatch = new();

        public static Bitmap? Map;

        public static TimeSpan totalLoadTime = new ();

        public static List<string> TimeStamps = new();

        public static string AppPath = "";
        public static string ModFolder = "";
        public static string VanillaFolder = "";
        public static string language = "english";

        public static Dictionary<int, Province> Provinces = new ();
        public static Dictionary<int, Province> RnvProvinces = new ();
        public static Dictionary<int, Province> LakeProvinces = new ();
        public static Dictionary<int, Province> LandProvinces = new ();
        public static Dictionary<int, Province> SeaProvinces = new ();
        public static Dictionary<int, Province> CoastalProvinces = new ();
        public static Dictionary<string, Color> NotOnMapProvinces = new ();

        public static Dictionary<string, string> Localization = new ();
        
        public static Dictionary<string, int> LocalizationFiles = new ();

        public static Dictionary<Color, int> ColorIds = new ();

        public static Province? LastProvince;
        public static Province? CurProvince;
        public static List<Province> SelectedProvinces = new ();

        public static Dictionary<string, Area> Areas = new();

        // FORMS

        public static MainWindow? MainWindow;
        public static LoadingScreen? LoadingForm;
    }
}
