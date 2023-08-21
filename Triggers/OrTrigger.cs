using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class OrTrigger : IComplexTrigger
{
    public string Name { get; set; } = "OrTrigger";
    public bool isNegated { get; set; }
    public List<ITrigger> Triggers { get; set; }
    public OrTrigger(List<ITrigger> triggers)
    {
        Triggers = triggers;
    }

    public bool GetTriggerValue(object o)
    {
        return isNegated
            ? Triggers.Any(trigger => trigger.GetTrigger(o))
            : Triggers.Any(trigger => !trigger.GetTrigger(o));
    }

    public override string ToString()
    {
        return $"{Name}: Returns if any of the \'{Triggers.Count}\' triggers are true.";
    }

}
