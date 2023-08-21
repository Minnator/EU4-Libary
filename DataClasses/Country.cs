namespace EU4_Parse_Lib.DataClasses;

public class Country
{
    public readonly Dictionary<Attribute, Func<Province, object>> Attributes = new()
    {
        { Attribute.Id, province => province.Id },
        { Attribute.Area, province => province.Area },
        { Attribute.Name, province => province.Name },
    };


    public object GetAttribute(Attribute attribute)
    {
        throw new NotImplementedException();
    }
}