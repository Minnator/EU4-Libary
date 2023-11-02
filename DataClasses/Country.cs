using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.DataClasses;

public class Country : IScope
{
    public string Tag { get; set; }
    public List<int> Provinces { get; set; }


    public readonly Dictionary<Attribute, Func<Country, object>> Attributes = new()
    {
        { Attribute.Tag, country => country.Tag },
    };


    public Country(string tag)
    {
        Tag = tag;
    }

    public IScope GetNextScope(Scope scope)
    {
       throw new NotImplementedException();
    }

    public object GetAttribute(Attribute attribute)
    {
        return Attributes[attribute](this);
    }
}