using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib.Interfaces
{
    public interface ITrigger
    {
        Attribute Attribute { get; }
        bool IsNegated { get; set; }
        string Name { get; set; }
        Scope Scope { get; set; }

        bool GetTriggerValue(Province p);
        bool GetTriggerValue(Country c);
        bool GetTrigger(object obj);
    }

    public interface IComplexTrigger : ITrigger
    {
        List<ITrigger> Triggers { get; set; }
    }
}