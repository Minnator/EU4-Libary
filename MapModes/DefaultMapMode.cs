using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.MapModes;
internal class DefaultMapMode : IMapMode
{
   public string Name { get; set; }
   public Scope MScope { get; set; }
   public MType Type { get; set; } = MType.OneColorPerValue;
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

   public DefaultMapMode(string name)
   {
      Name = name;
   }

   public void GetProvinceColor()
   {

   }

   public void RenderMapMode()
   {
      Vars.Map!.Dispose();
      Vars.Map = new Bitmap(Vars.DebugMapWithBorders!);
   }

   public override string ToString()
   {
      return "Default Map Mode";
   }

   public override int GetHashCode()
   {
      return Name.GetHashCode() ^ Type.GetHashCode();
   }
}
