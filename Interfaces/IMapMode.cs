using EU4_Parse_Lib.Triggers;

namespace EU4_Parse_Lib.Interfaces;

public interface IMapMode
{
    string Name { get; set; }
    bool UseGradient { get; set; }
    List<ITrigger> Triggers { get; set; }
    List<IComplexTrigger> ComplexTriggers { get; set; }
    //Type defines the type of value e.g. int, float, bool, string, Color the color for that according value
    Dictionary<Type, Color> ColorTable { get; set; }
    public Color GetProvinceColor();




}