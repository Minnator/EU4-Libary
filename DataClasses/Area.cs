using System.Text;

namespace EU4_Parse_Lib.DataClasses;

public class Area
{
    public string Name = "undefined";
    public List<int> Provinces = new();
    public float Prosperity = 0;
    public string Edict = string.Empty;
    public bool IsStated = false;

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        var objArea = obj as Area;
        return Name.Equals(objArea?.Name);
    }

    public string ToProvString()
    {
        StringBuilder sb = new ();
        foreach (var province in Provinces)
        {
            sb.Append($@"{province,4} ");
        }
        return sb.ToString();
    }
}