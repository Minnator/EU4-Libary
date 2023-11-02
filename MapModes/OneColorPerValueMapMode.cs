using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.MapModes;
internal class OneColorPerValueMapMode : IMapMode
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

   public OneColorPerValueMapMode(string name, Scope scope, MType type, Attribute attribute, bool onlyLandProvinces, bool useGradient, bool userDefinedMapmode)
   {
      Name = name;
      MScope = scope;
      Type = type;
      Attribute = attribute;
      OnlyLandProvinces = onlyLandProvinces;
      UseGradient = useGradient;
      UserDefinedMapMode = userDefinedMapmode;
   }

   private Color GenerateColor(string val, int cnt)
   {
      return ColorTable.TryGetValue(val, out var col)
          ? col
          : Vars.RandomColors[cnt];
   }

   public void GetProvinceColor()
   {

      Vars.SelectedMapModeColorMap.Clear();
      Dictionary<int, Color> dic = new();

      switch (MScope)
      {
         case Scope.Province:
            ColorTable = new Dictionary<string, Color>(Vars.ColorIds.Count);
            if (OnlyLandProvinces)
            {
               //TODO confirm which method is faster and more efficient

               //Parallel.ForEach(Vars.LandProvinces.Values, (province, state, index) =>
               //{
               //    var attributeValue = province.GetAttribute(Attribute).ToString();
               //    var color = GenerateColor(attributeValue, (int)index);
               //    dic.TryAdd(province.Id, color);
               //});


               var cnt = 0;
               foreach (var province in Vars.LandProvinces.Values)
               {
                  dic.Add(province.Id, GenerateColor(province.GetAttribute(Attribute).ToString()!, cnt));
                  cnt++;
               }
               foreach (var seaProvince in Vars.SeaProvinces.Values)
               {
                  dic.Add(seaProvince.Id, seaProvince.Color);
               }
            }
            else
            {
               var cnt = 0;
               foreach (var province in Vars.Provinces.Values)
               {
                  dic.Add(province.Id, GenerateColor(province.GetAttribute(Attribute).ToString()!, cnt));
                  cnt++;
               }
            }
            break;
         case Scope.Country:
            ColorTable = new Dictionary<string, Color>(Vars.OnMapCountries.Count);
            Parallel.ForEach(Vars.OnMapCountries.Values, country =>
            {
               var col = Util.GetGradientColor(Min, Max, (int)country.GetAttribute(Attribute), NullColor, Null);
               foreach (var id in country.Provinces)
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
