using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class AndTrigger : IComplexTrigger
{
    public string Name { get; set; }
    public Scope Scope { get; set; }
    public string TName { get; set; } = "AndTrigger";
    public object Value { get; } = -1;
    public List<ITrigger> Triggers { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public AndTrigger(List<ITrigger> triggers, bool isNegated = false, string name = "-")
    {
        Triggers = triggers;
        IsNegated = isNegated;
        Name = name;
    }
    public bool GetTriggerValue(Province p)
    {
        return IsNegated
            ? Triggers.All(trigger => !trigger.GetTriggerValue(p))
            : Triggers.All(trigger => trigger.GetTriggerValue(p));
    }

    public bool GetTriggerValue(Country c)
    {
        return IsNegated
            ? Triggers.All(trigger => !trigger.GetTriggerValue(c))
            : Triggers.All(trigger => trigger.GetTriggerValue(c));
    }

    public bool GetTrigger(object obj)
    {
        if (obj.GetType() == typeof(Province))
            return GetTriggerValue((Province)obj);
        if (obj.GetType() == typeof(Country))
            return GetTriggerValue((Country)obj);
        return false;
    }
    public override string ToString()
    {
        return $"{Name}: All [{Triggers.Count}] trigger are true";
    }
}
