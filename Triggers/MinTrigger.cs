using System.Diagnostics;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.Triggers;

public class MinTrigger : ITrigger
{
   public object Value { get; }
   public Attribute Attribute { get; set; }
   public bool IsNegated { get; set; }
   public string Name { get; set; }
   public Scope Scope { get; set; }
   public string TName { get; set; } = "MinTrigger";

   public MinTrigger(Attribute attribute, object value, Scope scope = Scope.None, bool isNegated = false, string name = "-")
   {
      Attribute = attribute;
      Value = value;
      Scope = scope;
      IsNegated = isNegated;
      Name = name;
   }
   public bool GetTrigger(object obj)
   {
      switch (Scope)
      {
         case Scope.Province:
            return obj is Province p && GetTriggerValue(p);
         case Scope.Country:
            Console.WriteLine("Scope is Country");
            throw new NotImplementedException();
            break;
         case Scope.Ruler:
            Console.WriteLine("Scope is Ruler");
            throw new NotImplementedException();
            break;
         case Scope.Unit:
            Console.WriteLine("Scope is Unit");
            throw new NotImplementedException();
            break;
         case Scope.Owner:
            Console.WriteLine("Scope is Owner");
            throw new NotImplementedException();
            break;
         case Scope.Controller:
            Console.WriteLine("Scope is Controller");
            throw new NotImplementedException();
            break;
         case Scope.None:
            Console.WriteLine("Using Parent Scope went wrong");
            return false;
         default:
            Console.WriteLine("Unknown Scope");
            return false;
      }
   }

   public bool GetTriggerValue(Province p)
   {
      try
      {
         if (p.GetAttribute(Attribute) is int num)
         {
            //Debug.WriteLine($"Is Number {num} > {Value}: {num > (Value as int? ?? -1)}");
            return num > (Value as int? ?? -1);
         }
      }
      catch
      {
         return false;
      }
      return false;
   }

   public bool GetTriggerValue(Country c)
   {
      try
      {
         if (c.GetAttribute(Attribute) is int num)
         {
            return num > (Value as int? ?? -1);
         }
      }
      catch
      {
         return false;
      }
      return false;
   }

   public override string ToString()
   {
      return IsNegated
          ? $"{Name}: [{Attribute}] > [{Value}]"
          : $"{Name}: [{Attribute}] < [{Value}]";

   }
}

//TODO Add other trigger types: > | complexTriggers (AND | OR)