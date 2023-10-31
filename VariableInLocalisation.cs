using EU4_Parse_Lib.DataClasses;
using System.Text.RegularExpressions;
using EU4_Parse_Lib.Interfaces;

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

   private static string GetCustomStringPairs(string command, IScope rootScope)
   {
      var parts = command.ToString().Split('.');
      if (parts.Length == 1)
         return string.Empty;

      var cnt = 0;
      IScope curScope = rootScope;
      while (cnt < parts.Length)
      {
         if (cnt < parts.Length - 1)
         {
            if (!Enum.TryParse<Scope>(parts[cnt], out var scope))
               return $"False Scope: {parts[cnt]}";

            curScope = curScope.GetNextScope(scope);
         }
         else
         {
            if (!Enum.TryParse<Attribute>(parts[Index.End], out var attribute))
               return $"False Attribute: {parts[cnt]}";

            return curScope.GetAttribute(attribute).ToString()!;
         }
         cnt++;
      }
      return $"Could not resolve custom loc: {command}";
   }

}
