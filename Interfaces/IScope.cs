namespace EU4_Parse_Lib.Interfaces;


public interface IScope
{
   public IScope GetNextScope(Scope scope);
   public object GetAttribute(Attribute attr);
}
