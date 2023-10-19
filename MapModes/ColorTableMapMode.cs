using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.MapModes;
internal class ColorTableMapMode : IMapMode
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
    public Dictionary<string, Color> ColorTable { get; set; }

    public ColorTableMapMode(string name, Scope scope, MType type, Attribute attribute, bool onlyLandProvinces, bool useGradient, Dictionary<string, Color> colorTable, bool userDefinedMapmode)
    {
        Name = name;
        MScope = scope;
        Type = type;
        Attribute = attribute;
        OnlyLandProvinces = onlyLandProvinces;
        UseGradient = useGradient;
        ColorTable = colorTable;
        UserDefinedMapMode = userDefinedMapmode;
    }

    public Dictionary<int, Color> GetProvinceColor()
    {
        Dictionary<int, Color> dic = new();

        switch (MScope)
        {
            case Scope.Province:
                if (OnlyLandProvinces)
                {
                    foreach (var province in Vars.LandProvinces.Values)
                    {
                        dic.Add(province.Id,
                            ColorTable.TryGetValue(province.GetAttribute(Attribute).ToString()!, out var col)
                                ? col
                                : Color.FromArgb(255, 0, 0, 0));
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
                        dic.Add(province.Id,
                            ColorTable.TryGetValue(province.GetAttribute(Attribute).ToString()!, out var col)
                                ? col
                                : Color.FromArgb(255, 0, 0, 0));
                    }
                }
                break;
            case Scope.Country:
                foreach (var country in Vars.OnMapCountries.Values)
                {
                    if (ColorTable.TryGetValue(country.GetAttribute(Attribute).ToString()!, out var col))
                        foreach (var province in country.provinces.Keys)
                            dic.Add(province, col);
                    else
                        foreach (var province in country.provinces.Keys)
                            dic.Add(province, Color.FromArgb(255, 0, 0, 0));
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

        return dic;
    }

    public void RenderMapMode()
    {
        Gui.ColorMap(GetProvinceColor());
    }

    public override string ToString()
    {
        return $"Gradient Mapode: [{Name}] \nScope: [{MScope}] - Only land provinces: [{OnlyLandProvinces}]\nValues: nul [{Null}] min [{Min}] max [{Max}]";
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Type.GetHashCode();
    }
}
