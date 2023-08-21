using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib.Interfaces;

public interface IComplexTrigger
{
    List<ITrigger> Triggers { get; set; }

    string Name { get; set; }
    public bool isNegated { get; set; }

    public bool GetTriggerValue(object o);


}