using EU4_Parse_Lib.DataClasses;
using System.Diagnostics;
using System.Text;

namespace EU4_Parse_Lib
{
    public static class DebugPrints
    {

        public static void PrintProvColDirectory(Dictionary<Color, List<Point>> dic)
        {
            StringBuilder sb = new ();
            var total = 0;
            foreach (var kvp in dic)
            {
                sb.Append ($"{kvp.Key.ToString(),12} - {kvp.Value.Count}").
                    Append(Environment.NewLine);
                total += kvp.Value.Count;
            }
            sb.Append (Environment.NewLine);
            sb.Append($"Total Pixels: {total.ToString("#,###.###")}");
            Saving.WriteLog(sb.ToString(), "ProvCols");
        }

        public static void PrintProvincesEmpty(Dictionary<int, Province> dic)
        {
            StringBuilder sb = new();
            foreach (var kvp in dic)
            {
                sb.AppendLine(
                    $"Province ID: {kvp.Key,4}, {kvp.Value.color,35}, Pixels: {kvp.Value.pixels.Count,6}");
            }
            sb.Append(Environment.NewLine);
            sb.Append($"Total Provinces: {dic.Count.ToString("#,###.###")}");
            Saving.WriteLog(sb.ToString(), "ProvEmpty");
        }
        public static void PrintProvincesUnused(Dictionary<string, Color> dic)
        {
            StringBuilder sb = new();
            foreach (var kvp in dic)
            {
                sb.AppendLine(
                    $"Province ID: {kvp.Key,4}, Color: {kvp.Value}");
            }
            sb.Append(Environment.NewLine);
            sb.Append($"Total Unused Provinces: {dic.Count.ToString("#,###.###")}");
            Saving.WriteLog(sb.ToString(), "ProvUnused");
        }
        public static void PrintProvincesBorder(Dictionary<int, Province> dic)
        {
            StringBuilder sb = new();
            foreach (var kvp in dic)
            {
                sb.AppendLine(
                    $"Province ID: {kvp.Key,4}, Number of Border Pixels: {kvp.Value.border.Count, 5}");
            }
            Saving.WriteLog(sb.ToString(), "ProvBorders");
        }
        public static void PrintColorsToIds(Dictionary<Color, int> dic)
        {
            StringBuilder sb = new();
            foreach (var kvp in dic)
            {
                sb.AppendLine(
                    $"Province ID: {kvp.Key,36}, ID: {kvp.Value, 4}");
            }
            Saving.WriteLog(sb.ToString(), "ColorsToIds");
        }

        
    }
}
