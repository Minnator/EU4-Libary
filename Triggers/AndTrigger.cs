using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class AndTrigger : IComplexTrigger
{
    public string Name { get; set; } = "AndTrigger";
    public List<ITrigger> Triggers { get; set; }
    public bool isNegated { get; set; }
    public AndTrigger(List<ITrigger> triggers)
    {
        Triggers = triggers;
    }

    public bool GetTriggerValue(object o)
    {
        return isNegated
            ? Triggers.All(trigger => trigger.GetTrigger(o))
            : Triggers.All(trigger => !trigger.GetTrigger(o));
    }

    public override string ToString()
    {
        return $"{Name}: Returns if all \'{Triggers.Count}\' triggers are true.";
    }

}
