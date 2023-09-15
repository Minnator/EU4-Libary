using EU4_Parse_Lib.Triggers;

namespace EU4_Parse_Lib.Interfaces;

public interface IMapMode
{
    string Name { get; set; }
    Scope MScope { get; set; }
    MType Type { get; set; }
    int Min { get; set; }
    int Max { get; set; }
    int Null { get; set; }
    Attribute Attribute { get; set; }
    Color NullColor { get; set; }
    bool OnlyLandProvinces { get; set; }
    bool UseGradient { get; set; }
    List<ITrigger> Triggers { get; set; }
    Dictionary<string, Color> ColorTable { get; set; }
    public Dictionary<int, Color> GetProvinceColor();

    public void RenderMapmode();

}