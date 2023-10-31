using System.Text;
using EU4_Parse_Lib.Interfaces;

namespace EU4_Parse_Lib.DataClasses;

public class Area : IScope
{
   public string Name = "undefined";
   public List<int> Provinces = new();
   public float Prosperity = 0;
   public string Edict = string.Empty;
   public bool IsStated = false;

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