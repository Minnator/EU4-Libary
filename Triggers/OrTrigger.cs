using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class OrTrigger : IComplexTrigger
{
    public Attribute Attribute { get; set; }
    public string Name { get; set; }
    public Scope Scope { get; set; }
    public string TName { get; set; } = "OrTrigger";
    public bool IsNegated { get; set; }
    public object Value { get; } = -1;
    public List<ITrigger> Triggers { get; set; }
    public OrTrigger(List<ITrigger> triggers, bool isNegated = false, string name = "-")
    {
        Triggers = triggers;
        IsNegated = isNegated;
        Name = name;
    }
    
    public override string ToString()
    {
        return $"{Name}: At least one of [{Triggers.Count}] trigger is true";
    }


    public bool GetTriggerValue(Province p)
    {
        return IsNegated
            ? Triggers.Any(trigger => !trigger.GetTrigger(p))
            : Triggers.Any(trigger => trigger.GetTrigger(p));
    }

    public bool GetTriggerValue(Country c)
    {
        return IsNegated
            ? Triggers.Any(trigger => !trigger.GetTrigger(c))
            : Triggers.Any(trigger => trigger.GetTrigger(c));
    }

    public bool GetTrigger(object obj)
    {
        if (obj.GetType() == typeof(Province))
            return GetTriggerValue((Province)obj);
        if (obj.GetType() == typeof(Country))
            return GetTriggerValue((Country)obj);
        return false;
    }
}
