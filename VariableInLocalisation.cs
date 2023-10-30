using EU4_Parse_Lib.DataClasses;
using System.Text.RegularExpressions;

namespace EU4_Parse_Lib;

internal static class VariableInLocalisation
{
   /// <summary>
   /// Converts [Scope.Scope...Scope.Value] to its string and replaces the content inside the []
   /// Does NOT support multiple Scopes yet
   /// </summary>
   /// <param name="input"></param>
   /// <returns></returns>
   public static string GetCustomString(this string input, Province province)
   {
      return Regex.Replace(input, @"\[(.*?)\]", delegate (Match match)
      {
         var placeholder = match.Value;
         // Get the replacement value using the custom logic
         var replacement = GetCustomStringPairs(placeholder, province);
         // If a replacement value is available, use it; otherwise, keep the original placeholder
         return replacement;
      });
   }

   private static string GetCustomStringPairs(string command, Province province)
   {
      var parts = command.ToString().Split('.');
      if (parts.Length != 2)
         return string.Empty;

      if (!Enum.TryParse<Attribute>(parts[Index.End], out var attr))
      {
         //ErrorBox.Text += $"Invalid Attribute [{parts[1]}], please enter a valid!"; 
      }

      var kvp = new KeyValuePair<string, Attribute>(parts[1], attr);
      return ResolveCustomString(kvp, province);
   }

   private static string ResolveCustomString(KeyValuePair<string, Attribute> customString, Province province)
   {
      


      return customString.Key;
   }

}
