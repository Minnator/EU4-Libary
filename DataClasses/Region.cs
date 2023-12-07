using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.DataClasses;
public class Region : IScope, IProvCollection
{
   public string Name { get; set; } = "-1";
   public string SuperRegion { get; set; } = "-1";
   public List<string> Areas { get; set; } = new();


   private static readonly Dictionary<Attribute, Func<Region, object>> Attributes = new()
   {
      { Attribute.Name, region => region.Name },
   };

   private static readonly Dictionary<Scope, Func<Region, IScope>> Scopes = new()
   {
      //{Scope.Owner, region => Vars.Countries[region.Owner]},
   };

   public override string ToString()
   {
      return Name;
   }

   public override bool Equals(object? obj)
   {
      var objRegion = obj as Region;
      return Name.Equals(objRegion?.Name);
   }

   public override int GetHashCode()
   {
      return Name.GetHashCode();
   }

   public IScope GetNextScope(Scope scope)
   {
      return Scopes[scope](this);
   }

   public object GetAttribute(Attribute attr)
   {
      return Attributes[attr](this);
   }

   public List<int> GetProvinces()
   {
      List<int> provinces = new();
      foreach (var areaName in Areas)
      {
         if (!Vars.Areas.TryGetValue(areaName, out var area))
            continue;
         provinces.AddRange(area.Provinces);
      }
      return provinces;
   }
}
