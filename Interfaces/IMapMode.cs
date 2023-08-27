﻿using EU4_Parse_Lib.Triggers;

namespace EU4_Parse_Lib.Interfaces;

public interface IMapMode
{
    string Name { get; set; }
    Scope Scope { get; set; }
    string Type { get; set; }
    int Min { get; set; }
    int Max { get; set; }
    int Null { get; set; }
    Attribute Attribute { get; set; }
    Color NullColor { get; set; }
    bool OnlyLandProvinces { get; set; }
    bool UseGradient { get; set; }
    List<ITrigger> Triggers { get; set; }
    Dictionary<object, Color> ColorTable { get; set; }
    public Color GetProvinceColor();
    
}