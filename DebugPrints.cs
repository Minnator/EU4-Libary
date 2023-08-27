using EU4_Parse_Lib.DataClasses;
using System.Diagnostics;
using System.Text;
using EU4_Parse_Lib.Interfaces;
using EU4_Parse_Lib.Triggers;

namespace EU4_Parse_Lib
{
    public static class DebugPrints
    {
        public static void PrintTestTriggerValue()
        {
            StringBuilder sb = new();
            MinTrigger minTrigger = new (Attribute.Id, 40, Scope.Province);
            List<ITrigger> list = new() { minTrigger };
            OrTrigger andTrigger = new(list);
            foreach (var landProvince in Vars.LandProvinces)
            {
                if (andTrigger.GetTrigger(landProvince.Value))
                    sb.AppendLine($"Id: {landProvince.Value.Id,4},  true");
                else
                    sb.AppendLine($"Id: {landProvince.Value.Id,4}, false");
            }
            Saving.WriteLog(sb.ToString(), "TriggerTest");
        }

        public static void PrintAttributes(Dictionary<int, Province> dic)
        {
            StringBuilder sb = new();
            foreach (var p in dic)
            {
                sb.AppendLine($"{p.Key,4} | {p.Value.GetAttribute(Attribute.Area)}");
            }
            Saving.WriteLog(sb.ToString(), "AttributeDebug");
        }
        public static void PrintLocData(Dictionary<string, int> dic)
        {
            StringBuilder sb = new();
            sb.AppendLine($"Total Localization files: {dic.Count.ToString("###,###,###")}");
            sb.AppendLine($"Total Localization Entries: {Vars.Localization.Count.ToString("###,###,###")}");
            sb.AppendLine($"Num of Entries | file name");
            foreach (var file in dic)
            {
                sb.AppendLine($"[{file.Value.ToString("###,###,###"),6}] - {file.Key}");
            }
            Saving.WriteLog(sb.ToString(), "LocalizationData");
        }
        public static void PrintProvincesContent(Dictionary<int, Province> dic)
        {
            StringBuilder sb = new();
            foreach (var province in dic)
            {
                sb.AppendLine(province.ToString());
            }
            Saving.WriteLog(sb.ToString(), "ProvinceContent");
        }
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
                    $"Province ID: {kvp.Key,4}, {kvp.Value.Color,35}, Pixels: {kvp.Value.Pixels.Count,6}");
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
                    $"Province ID: {kvp.Key,4}, Number of Border Pixels: {kvp.Value.Border.Count, 5}");
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

        public static void PrintProvinceList()
        {
            StringBuilder sb = new();
            sb.Append($"Coastal Provinces:".PadRight(30) + $"{Vars.CoastalProvinces.Count,4}\n" +
                      $"Sea Provinces:".PadRight(30) + $"{Vars.SeaProvinces.Count,4}\n" +
                      $"Lake Provinces:".PadRight(30) + $"{Vars.LakeProvinces.Count,4}\n" +
                      $"Random new World Provinces:".PadRight(30) + $"{Vars.RnvProvinces.Count,5}\n" +
                      $"Land Provinces:".PadRight(30) + $"{Vars.LandProvinces.Count,4}\n" +
                      $"Total Provinces:".PadRight(30) + $"{(Vars.CoastalProvinces.Count + Vars.SeaProvinces.Count + Vars.LakeProvinces.Count + Vars.RnvProvinces.Count + Vars.LandProvinces.Count),4}\n");
            sb.Append($"\n----------sea----------\n{PrintIdList(Vars.SeaProvinces)}");
            sb.Append($"\n----------coastal----------\n{PrintIdList(Vars.CoastalProvinces)}");
            sb.Append($"\n----------lake----------\n{PrintIdList(Vars.LakeProvinces)}");
            sb.Append($"\n----------random new world----------\n{PrintIdList(Vars.RnvProvinces)}");
            sb.Append($"\n----------land----------\n{PrintIdList(Vars.LandProvinces)}");
            Saving.WriteLog(sb.ToString(), "ProvinceIds");
        }

        private static string PrintIdList(Dictionary<int, Province> dic)
        {
            StringBuilder sb = new();
            var cnt = 0;
            foreach (var p in dic)
            {
                if (cnt == 25)
                {
                    sb.Append(Environment.NewLine);
                    cnt = 0;
                }
                sb.Append($"{p.Key,4} ");
                cnt++;
            }
            return sb.ToString();
        }

        public static void PrintAreas(Dictionary<string, Area> dic)
        {
            StringBuilder sb = new();
            sb.AppendLine($"Number of Areas: {dic.Count.ToString()}");
            foreach (var area in dic)
            {
                sb.AppendLine($"{area.Key.PadRight(30)}|{area.Value.Provinces.Count,2}| {area.Value.ToProvString()}");
            }
            Saving.WriteLog(sb.ToString(), "Areas");
        }
        //TODO Provinces need to know in which area they are, areas in which Region etc...
    }
}
