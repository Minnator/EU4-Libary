using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class EqualsTrigger : ITrigger 
{
    public object Value { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public string Name { get; set; } = "EqualsTrigger";

    public EqualsTrigger(Attribute attribute, object value, bool isNegated)
    {
        Attribute = attribute;
        Value = value;
        IsNegated = isNegated;
    }
    public bool GetTrigger(object scope)
    {
        if (scope.GetType() == typeof(Province))
            return GetTriggerValue((Province)scope);
        if (scope.GetType() == typeof(Country))
            return GetTriggerValue((Country)scope);
        return false;
    }

    public bool GetTriggerValue(Province p)
    {
        if (IsNegated)
            return !p.Attributes[Attribute].Equals(Value);
        return p.Attributes[Attribute].Equals(Value);
    }
    public bool GetTriggerValue(Country c)
    {
        if (IsNegated)
            return !c.Attributes[Attribute].Equals(Value);
        return c.Attributes[Attribute].Equals(Value);
    }

    public override string ToString()
    {
        return $"{Name}: Compares the \"{Attribute}\" to {Value}; Negated: {IsNegated}";
    }
}

//TODO Add other trigger types: < | > | >< | <> | complexTriggers (AND | OR)