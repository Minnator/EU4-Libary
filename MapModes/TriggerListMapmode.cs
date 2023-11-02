using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.MapModes;
internal class TriggerListMapmode : IMapMode
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
   public List<ITrigger> Triggers { get; set; }
   public Dictionary<string, Color> ColorTable { get; set; } = new();

   public TriggerListMapmode(string name, Scope scope, MType type, Attribute attribute, bool onlyLandProvinces, List<ITrigger> triggers, bool userDefinedMapmode)
   {
      Name = name;
      MScope = scope;
      Type = type;
      Attribute = attribute;
      OnlyLandProvinces = onlyLandProvinces;
      Triggers = triggers;
      UserDefinedMapMode = userDefinedMapmode;
   }

   public void GetProvinceColor()
   {

      Vars.SelectedMapModeColorMap.Clear();
      Dictionary<int, Color> dic = new();

      switch (MScope)
      {
         case Scope.Province:
            if (OnlyLandProvinces)
            {
               foreach (var province in Vars.LandProvinces.Values)
               {
                  foreach (var trigger in Triggers)
                  {
                     if (!trigger.GetTrigger(province))
                     {
                        dic.Add(province.Id, Color.FromArgb(255, 219, 27, 27));
                        break;
                     }
                     dic.Add(province.Id, Color.FromArgb(255, 0, 204, 0));
                  }
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
                  foreach (var trigger in Triggers)
                  {
                     if (!trigger.GetTrigger(province))
                     {
                        dic.Add(province.Id, Color.FromArgb(255, 219, 27, 27));
                        break;
                     }
                     dic.Add(province.Id, Color.FromArgb(255, 0, 204, 0));
                  }
               }
            }
            break;
         case Scope.Country:
            foreach (var countriesValue in Vars.Countries.Values)
            {
               foreach (var trigger in Triggers)
               {
                  if (!trigger.GetTrigger(countriesValue))
                  {
                     foreach (var provinceID in countriesValue.Provinces)
                     {
                        dic.Add(provinceID, Color.FromArgb(255, 219, 27, 27));

                     }
                     break;
                  }
                  foreach (var provinceID in countriesValue.Provinces)
                  {
                     dic.Add(provinceID, Color.FromArgb(255, 0, 204, 0));
                  }
               }
            }
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
            break;
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
}
