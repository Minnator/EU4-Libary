using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class EqualsTrigger : ITrigger 
{
    private object Value { get;}
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public string Name { get; set; }
    public Scope Scope { get; set; }

    public EqualsTrigger(Attribute attribute, object value, Scope scope, bool isNegated = false, string name = "EqualsTrigger")
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
            case Scope.Ruler:
                Console.WriteLine("Scope is Ruler");
                throw new NotImplementedException();
            case Scope.Unit:
                Console.WriteLine("Scope is Unit");
                throw new NotImplementedException();
            case Scope.Owner:
                Console.WriteLine("Scope is Owner");
                throw new NotImplementedException();
            case Scope.Controller:
                Console.WriteLine("Scope is Controller");
                throw new NotImplementedException();
            default:
                Console.WriteLine("Unknown Scope");
                return false;
        }
    }

    public bool GetTriggerValue(Province p)
    {
        if (IsNegated)
            return !p.GetAttribute(Attribute).Equals(Value);
        return p.GetAttribute(Attribute).Equals(Value);
    }
    public bool GetTriggerValue(Country c)
    {
        if (IsNegated)
            return !c.GetAttribute(Attribute).Equals(Value);
        return c.GetAttribute(Attribute).Equals(Value);
    }

    public override string ToString()
    {
        return $"{Name}: Compares the \"{Attribute}\" to {Value}; Negated: {IsNegated}";
    }
}