using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib
{
   public enum MType
   {
      TriggerList, Gradient, ColorTable, OneColorPerValue
   }
   public enum Scope
   {
      Province, Country, Ruler, Unit, Owner, Controller, None
   }
   public enum ProvinceAtt
   {
      Id, Area, Name, Owner, Controller, BaseTax, BaseManpower, BaseProduction,
   }
   public enum CountryAtt
   {
      Tag, Cash, Prestige, Legitimacy, PowerProjection, Adm, Dip, Mil,
   }
   public enum Attribute
   {
      Id, Area, Name, Owner, Controller, BaseTax, BaseManpower, BaseProduction, Tag, Cash, Prestige,
      Legitimacy, PowerProjection, Adm, Dip, Mil,
   }

   public struct BorderPixel
   {
      public int start, end, length;

      public BorderPixel(int start, int end)  // start[ [end --> end not included
      {
         this.start = start;
         this.end = end;
         this.length = end - start;
      }
   }
   public static class Vars
   {
      public static ContextMenuStrip MapRightClickMenu = new();

      public static bool RenderCreatedMapModes = true; // TODO make settable in an interface aka settings
      public static bool DrawOutlineInMapModes = true; //TODO make this a setting
      
      public static float ZoomFactor = 1f;
      public static float ZoomIncrements = 0.25f;

      public static readonly Stopwatch Stopwatch = new();

      public static Bitmap? Map;
      public static Bitmap? DebugMapWithBorders;
      public static Bitmap? BorderlessMap;

      public static IMapMode? MapMode;

      public static TimeSpan TotalLoadTime = new();

      public static List<string> TimeStamps = new();
      public static List<string> CountryAttributeNames = new();
      public static List<string> ProvinceAttributeNames = new();
      public static List<string> ScopeNames = new();

      public static string User = "";
      public static string AppPath = "";
      public static string ModFolder = "";
      public static string VanillaFolder = "S:\\SteamLibrary\\steamapps\\common\\Europa Universalis IV";
      public static string Language = "english";
      public static string DataPath = "";

      public static int MapBorderPixelCount = 0;

      //Can be Changed in KeyBoardMapping Interface
      public static Dictionary<Keys, Button?> MapModeKeyMap = new();
      public static Dictionary<Keys, Button?> MapMovementKeyMap = new()
        {
            { Keys.W, new Button(){ Text = "Map Up" } },
            { Keys.A, new Button(){ Text = "Map Left" } },
            { Keys.S, new Button(){ Text = "Map Down" } },
            { Keys.D, new Button(){ Text = "Map Right" } },
        };

      public static int BorderPixelCount = new ();

      public static Point[]? BorderArray;
      public static Point[]? MapBorder;

      public static Dictionary<int, Province> Provinces = new();
      public static Dictionary<int, Province> RnvProvinces = new();
      public static Dictionary<int, Province> LakeProvinces = new();
      public static Dictionary<int, Province> LandProvinces = new();
      public static Dictionary<int, Province> SeaProvinces = new();
      public static Dictionary<int, Province> CoastalProvinces = new();

      public static Dictionary<int, Color> SelectedMapModeColorMap = new();

      public static Dictionary<Color, List<Point>> ColorsToPixelDictionary = new();

      public static Dictionary<string, Country> Countries = new();
      public static Dictionary<string, Country> OnMapCountries = new();

      public static Dictionary<string, Color> NotOnMapProvinces = new();

      public static Dictionary<Scope, Type> ScopeToType = new();

      public static Dictionary<string, IMapMode> MapModes = new();

      public static Dictionary<string, string> Localization = new(116000);
      public static Dictionary<string, string> LocalizationHashCollisions = new(); //TODO print and notify the user --> Notification system?

      public static Dictionary<string, int> LocalizationFiles = new();

      public static Dictionary<Color, int> ColorIds = new();

      public static Province? LastProvince;
      public static Province? CurProvince;
      public static List<Province> SelectedProvinces = new();

      public static Dictionary<string, Area> Areas = new();

      public static Dictionary<int, Color> RandomColors = new();

      public static Color HoverCursorProvinceColor ;


      // FORMS

      public static MainWindow? MainWindow;
      public static LoadingScreen? LoadingForm;
      public static WebBrowserForm? WebBrowserForm;
      public static ManageMapmodes? ManageMapmodes;
      public static FileEditor? FileEditor;
      public static SettingsForm? SettingsForm;

      public static object BitmapLock = new(); // Lock for bitmap access
   }
}
