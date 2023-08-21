using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib.Interfaces;

public interface ITrigger
{
    //object Value { get; set; }
    Attribute Attribute { get; set; }
    bool IsNegated { get; set; }
    string Name { get; set; }

    public bool GetTriggerValue(Province p);
    public bool GetTriggerValue(Country c);
    public bool GetTrigger(object scope);

}