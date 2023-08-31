using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class MaxTrigger : ITrigger 
{
    private int Value { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public string Name { get; set; }
    public Scope Scope { get; set; }

    public MaxTrigger(Attribute attribute, int value, Scope scope, bool isNegated = false, string name = "MaxTrigger")
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
        if (p.GetAttribute(Attribute) is int num)
        {
            return num < Value;
        }
        return false;
    }
    
    public bool GetTriggerValue(Country c)
    {
        if (c.GetAttribute(Attribute) is int num)
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