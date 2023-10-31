using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.DataClasses
{
   public class Province : IScope
   {

      public int Id { get; set; }
      public string Owner { get; set; }
      public string Controller { get; set; }
      public string Capital { get; set; }
      public string Culture { get; set; }
      public string Religion { get; set; }
      public List<string> Cores { get; set; }
      public List<string> Discoveries { get; set; }
      public short BaseTax { get; set; }
      public short BaseProduction { get; set; }
      public short BaseManpower { get; set; }
      public short ExtraCost { get; set; }
      public bool HRE { get; set; } = false;
      public bool IsCity { get; set; } = false;

      public TradeGood TradeGood { get; set; }
      public byte CenterOfTrade { get; set; }
      //public List<ProvinceHistoryEntry> History { get; set; }
      //Fort is missing

      public Color Color;

      public List<Point> Pixels = new();
      public List<Point> Border = new();

      public string Area { get; set; } = "-1";
      public string Name { get; set; } = "-1";

      private static readonly Dictionary<Attribute, Func<Province, object>> Attributes = new()
        {
            { Attribute.Id, province => province.Id },
            { Attribute.Area, province => province.Area },
            { Attribute.Name, province => province.Name },
        };

      private static readonly Dictionary<Scope, Func<Province, IScope>> Scopes = new()
      {
         {Scope.Owner, province => Vars.Countries[province.Owner]},
      };

      public IScope GetNextScope(Scope scope)
      {
         return Scopes[scope](this);
      }

      public object GetAttribute(Attribute att)
      {
         return Attributes[att](this);
      }

      public Province(Color col)
      {
         Color = col;
      }

      public override string ToString()
      {
         return $"ID: {Id,4}; area: {Area}";
      }

      public override bool Equals(object? obj)
      {
         return obj is Province province &&
                Id == province.Id;
      }

      public override int GetHashCode()
      {
         return HashCode.Combine(Id);
      }


   }
}
