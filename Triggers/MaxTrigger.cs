using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class MaxTrigger : ITrigger 
{
    public int Value { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public string Name { get; set; } = "MaxTrigger";

    public MaxTrigger(Attribute attribute, int value)
    {
        Attribute = attribute;
        Value = value;
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
        if ((object)p.Attributes[Attribute] is int num)
        {
            return num < Value;
        }
        return false;
    }
    
    public bool GetTriggerValue(Country c)
    {
        if ((object)c.Attributes[Attribute] is int num)
        {
            return num < Value;
        }
        return false;
    }

    public override string ToString()
    {
        return $"{Name}: Compares the \"{Attribute}\" to {Value}";
    }
}

//TODO Add other trigger types: complexTriggers (AND | OR)