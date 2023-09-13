namespace EU4_Parse_Lib.DataClasses;

public class Country
{
    string Tag { get; set; }
    public Dictionary<int, Province> provinces;


    public readonly Dictionary<Attribute, Func<Country, object>> Attributes = new()
    {
        { Attribute.Tag, country => country.Tag },
    };


    public Country(string tag)
    {
        Tag = tag;
    }

    public object GetAttribute(Attribute attribute)
    {
        return Attributes[attribute](this);
    }
}