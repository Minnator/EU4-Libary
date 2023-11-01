using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.MapModes;
internal class GradientMapMode : IMapMode
{
   public string Name { get; set; }
   public Scope MScope { get; set; }
   public MType Type { get; set; }
   public int Min { get; set; }
   public int Max { get; set; }
   public int Null { get; set; }
   public Attribute Attribute { get; set; }
   public Color NullColor { get; set; }
   public bool OnlyLandProvinces { get; set; }
   public bool UseGradient { get; set; }
   public bool UserDefinedMapMode { get; set; }
   public List<ITrigger> Triggers { get; set; } = new();
   public Dictionary<string, Color> ColorTable { get; set; } = new();

   public GradientMapMode(string name, Scope scope, MType type, Attribute attribute, bool onlyLandProvinces, int min, int max, int nul, bool useGradient, bool userDefinedMapmode)
   {
      Name = name;
      MScope = scope;
      Type = type;
      Attribute = attribute;
      OnlyLandProvinces = onlyLandProvinces;
      Min = min;
      Max = max;
      Null = nul;
      UseGradient = useGradient;
      UserDefinedMapMode = userDefinedMapmode;
   }

   public void GetProvinceColor()
   {
      Stopwatch sw = Stopwatch.StartNew();
      Dictionary<int, Color> dic = new();

      switch (MScope)
      {
         case Scope.Province:
            if (OnlyLandProvinces)
            {
               foreach (var province in Vars.LandProvinces.Values)
               {
                  dic.Add(province.Id, Util.GetGradientColor(Min, Max, (int)province.GetAttribute(Attribute), NullColor, Null));
               }
               foreach (var seaProvince in Vars.SeaProvinces.Values)
               {
                  dic.Add(seaProvince.Id, seaProvince.Color);
               }
            }
            else
            {
               foreach (var province in Vars.Provinces.Values)
               {
                  dic.Add(province.Id, Util.GetGradientColor(Min, Max, (int)province.GetAttribute(Attribute), NullColor, Null));
               }
            }
            break;
         case Scope.Country:
            Parallel.ForEach(Vars.OnMapCountries.Values, country =>
            {
               var col = Util.GetGradientColor(Min, Max, (int)country.GetAttribute(Attribute), NullColor, Null);
               foreach (var id in country.provinces.Keys)
               {
                  dic.TryAdd(id, col);
               }
            });
            break;
         case Scope.Ruler:
            throw new NotImplementedException();
            break;
         case Scope.Unit:
            throw new NotImplementedException();
            break;
         case Scope.Owner:
            throw new NotImplementedException();
            break;
         case Scope.Controller:
            throw new NotImplementedException();
            break;
         case Scope.None:
            throw new NotImplementedException();
         default:
            throw new ArgumentOutOfRangeException();
      }

      Vars.SelectedMapModeColorMap = dic;

      Debug.WriteLine($"Creating Color map: {sw.Elapsed}");
   }

   public void RenderMapMode()
   {
      GetProvinceColor();
      Gui.ColorMap();
   }

   public override string ToString()
   {
      return $"Gradient Mapmode: [{Name}] \nScope: [{MScope}] - Only land provinces: [{OnlyLandProvinces}]\nValues: nul [{Null}] min [{Min}] max [{Max}]";
   }

   public override int GetHashCode()
   {
      return Name.GetHashCode() ^ Type.GetHashCode();
   }
}
