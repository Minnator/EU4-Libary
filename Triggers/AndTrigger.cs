using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class AndTrigger : IComplexTrigger
{
    public string Name { get; set; } = "AndTrigger";
    public Scope Scope { get; set; }
    public List<ITrigger> Triggers { get; set; }
    public Attribute Attribute { get; set; }
    public bool IsNegated { get; set; }
    public AndTrigger(List<ITrigger> triggers, bool isNegated = false)
    {
        Triggers = triggers;
        IsNegated = isNegated;
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
        return $"{Name}: Returns if all \'{Triggers.Count}\' triggers are true.";
    }
}
