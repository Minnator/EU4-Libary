using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class MaxTrigger : ITrigger 
{
    public object Value { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public string Name { get; set; }
    public Scope Scope { get; set; }
    public string TName { get; set; } = "MaxTrigger";

    public MaxTrigger(Attribute attribute, object value, Scope scope, bool isNegated = false, string name = "-")
    {
        Attribute = attribute;
        Value = value;
        IsNegated = isNegated;
        Name = name;
        Scope = scope;
    }

    public bool GetTrigger(object obj)
    {
        switch (Scope)
        {
            case Scope.Province:
                Console.WriteLine("Scope is Province");
                return GetTriggerValue((Province)obj);
            case Scope.Country:
                Console.WriteLine("Scope is Country");
                throw new NotImplementedException();
                break;
            case Scope.Ruler:
                Console.WriteLine("Scope is Ruler");
                throw new NotImplementedException();
                break;
            case Scope.Unit:
                Console.WriteLine("Scope is Unit");
                throw new NotImplementedException();
                break;
            case Scope.Owner:
                Console.WriteLine("Scope is Owner");
                throw new NotImplementedException();
                break;
            case Scope.Controller:
                Console.WriteLine("Scope is Controller");
                throw new NotImplementedException();
                break;
            default:
                Console.WriteLine("Unknown Scope");
                return false;
        }
    }

    public bool GetTriggerValue(Province p)
    {
        try
        {
            if (p.GetAttribute(Attribute) is int num)
            {
                return num < (Value as int? ?? -1);
            }
        }
        catch
        {
            return false;
        }
        return false;
    }
    
    public bool GetTriggerValue(Country c)
    {
        try
        {
            if (c.GetAttribute(Attribute) is int num)
            {
                return num < (Value as int? ?? -1);
            }
        }
        catch
        {
            return false;
        }
        return false;
    }

    public override string ToString()
    {
        return IsNegated
            ? $"{Name}: [{Attribute}] < [{Value}]"
            : $"{Name}: [{Attribute}] > [{Value}]";
    }
}