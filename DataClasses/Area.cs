using System.Text;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.DataClasses;

public class Area : IScope, IProvCollection
{
   public string Region { get; set; } = "-1";
   public string Name { get; set; } = "-1";
   public string Edict { get; set; } = string.Empty;

   public List<int> Provinces { get; set; } = new();

   public float Prosperity { get; set; } = 0;

   public bool IsStated { get; set; }

   public List<KeyValuePair<string, float>> GetOwnerPercentage()
   {
      Dictionary<string, int> counts = new ();

      foreach (var province in Provinces)
      {
         var temp = Vars.Provinces[province];
         if (counts.ContainsKey(temp.Owner))
            counts[temp.Owner]++;
         else
            counts[temp.Owner] = 1;
      }

      List<KeyValuePair<string, float>> percentages = new ();

      foreach (var count in counts)
      {
         percentages.Add(new KeyValuePair<string, float>(count.Key, (float)count.Value / Provinces.Count));
      }

      return percentages;
   }


   public override int GetHashCode()
   {
      return Name.GetHashCode();
   }

   public List<int> GetProvinces()
   {
      return Provinces;
   }

   public override bool Equals(object? obj)
   {
      var objArea = obj as Area;
      return Name.Equals(objArea?.Name);
   }

   public string ToProvString()
   {
      StringBuilder sb = new();
      foreach (var province in Provinces)
      {
         sb.Append($@"{province,4} ");
      }
      return sb.ToString();
   }


   public IScope GetNextScope(Scope scope)
   {
      throw new NotImplementedException();
   }

   public object GetAttribute(Attribute attr)
   {
      throw new NotImplementedException();
   }
}