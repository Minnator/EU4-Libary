using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using EU4_Parse_Lib.DataClasses;
namespace EU4_Parse_Lib
{
    public static class Loading
    {
        private static Dictionary<Color, List<Point>> _pixDic = new();

        public static void LoadAll()
        {
            var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
            var map = new Bitmap(combinedPath);
            Vars.Map = map;
            Vars.stopwatch.Start();
            _pixDic = GetAllPixels();
            InitProvinces(_pixDic);
            Vars.stopwatch.Stop();
            Vars.totalLoadTime += Vars.stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed Provinces:".PadRight(30) + $"| {Vars.stopwatch.Elapsed} |");
            Vars.stopwatch.Reset();
            LoadWithStopWatch("Borders", GenerateBorders);
            LoadWithStopWatch("default.map", LoadDefaultMap);
            LoadWithStopWatch("Areas", LoadAreas);
            LoadWithStopWatch("Localization", LoadAllLocalization);
            WriteDebugFiles();
            Debug.WriteLine("Finished Loading");
        }

        /// <summary>
        /// Executes a Method that DOES NOT have any parameters and logs the time it took.
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="action"></param>
        private static void LoadWithStopWatch(string taskName, Action action)
        {
            Vars.stopwatch.Start();
            action.Invoke();
            Vars.stopwatch.Stop();
            Vars.totalLoadTime += Vars.stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed {taskName}:".PadRight(30) + $"| {Vars.stopwatch.Elapsed} |");
            Vars.stopwatch.Reset();
        }

        private static void WriteDebugFiles()
        {
            Vars.stopwatch.Start();
            DebugPrints.PrintProvincesBorder(Vars.Provinces);
            DebugPrints.PrintProvColDirectory(_pixDic);
            DebugPrints.PrintProvincesEmpty(Vars.Provinces);
            DebugPrints.PrintProvincesUnused(Vars.NotOnMapProvinces);
            DebugPrints.PrintColorsToIds(Vars.ColorIds);
            DebugPrints.PrintAreas(Vars.Areas);
            DebugPrints.PrintProvincesContent(Vars.Provinces);
            DebugPrints.PrintProvinceList();
            DebugPrints.PritLocData(Vars.LocalizationFiles);
            Vars.stopwatch.Stop();
            Vars.totalLoadTime += Vars.stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed Debug Files:".PadRight(30) + $"| {Vars.stopwatch.Elapsed} |");
            Vars.stopwatch.Reset();
            Vars.TimeStamps.Add("------------------------------|------------------|");
            Vars.TimeStamps.Add($"Total Time Elapsed:".PadRight(30) + $"| {Vars.totalLoadTime} |");
            Saving.WriteLog(Vars.TimeStamps.ListToString(), "TimeComplexity");
        }

        public static string GetLoc(string key)
        {
            return Vars.Localization.TryGetValue(key, out var name) ? name : "-1";
        }
        /// <summary>
        /// first read in the mod localization and then the vanilla localization.
        /// Does not care about the number in loc yet - feature for future
        /// </summary>
        private static void LoadAllLocalization()
        {
            var ymlFiles = new List<string>();
            Vars.Localization.Clear();
            var path = Path.Combine(Vars.ModFolder, "localisation");
            if (Directory.Exists(path))
                ymlFiles.AddRange(Directory.GetFiles(path, $"*_l_{Vars.language}.yml").ToList());
            path = Path.Combine(Vars.VanillaFolder, "localisation");
            if (Directory.Exists(path))
                ymlFiles.AddRange(Directory.GetFiles(path, $"*_l_{Vars.language}.yml").ToList());
            if (ymlFiles.Count <= 0) return;
            Dictionary<string, string> loc = new();
            Dictionary<string, int> locFiles = new();

            foreach (var ymlPath in ymlFiles)
            {
                var lines = File.ReadAllLines(ymlPath);
                locFiles[ymlPath] = lines.Length;
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, @"(?<key>.*):\d\s+""(?<value>.*)""");
                    if (!match.Success) continue;
                    var id = match.Groups["key"].Value.Trim();
                    var name = match.Groups["value"].Value.Trim();
                    if (loc.ContainsKey(id)) continue;
                    loc[id] = name;
                }
            }

            Vars.Localization = loc;
            Vars.LocalizationFiles = locFiles;
        }
        private static void LoadAreas()
        {
            var path = Util.GetModOrVanillaPathFile(Path.Combine("map", "area.txt"));
            var newContent = Reading.ReadFileUTF8Lines(path);

            List<string> contentList = new();
            foreach (var line in newContent)
            {
                if (line != null && !(line.StartsWith('#') || line.Contains("color")))
                    contentList.Add(line);
            }

            var content = contentList.ListToString();
            //Saving.WriteLog(content, "AreaContent");

            Dictionary<string, Area> areas = new();

            var matches = Regex.Matches(content, @"(?<name>[A-Za-z_]*)\s*=\s*{.*(?<provinces>[^\}|^#]*)");
            foreach (var match in matches.Cast<Match>())
            {
                Area area = new();
                var name = match.Groups["name"].Value;
                area.Name = name;
                area.Provinces = Util.GetProvincesList(match.Groups["provinces"].Value);
                areas.Add(name, area);
                foreach (var province in area.Provinces)
                {
                    Vars.Provinces[province].Area = area.Name;
                }
            }
            Vars.Areas = areas;
        }
        private static void LoadDefaultMap()
        {
            var path = Util.GetModOrVanillaPathFile(Path.Combine("map", "default.map"));
            var content = Reading.ReadFileUTF8(path);
            const string pattern = @"\bmax_provinces\b\s+=\s+(?<maxProv>\d*)\s*\bsea_starts\b\s+=\s+{(?<seaProvs>[^\}]*)}[.\s\S]*\bonly_used_for_random\b\s+=\s+{(?<RnvProvs>[^\}]*)}[.\s\S]*\blakes\b\s+=\s+{(?<LakeProvs>[^\}]*)}[.\s\S]*\bforce_coastal\b\s+=\s+{(?<CostalProvs>[^\}]*)";

            Dictionary<int, Province> sea = new();
            Dictionary<int, Province> rnv = new();
            Dictionary<int, Province> lake = new();
            Dictionary<int, Province> coastal = new();
            Dictionary<int, Province> land = new();

            var match = Regex.Match(content, pattern);

            if (int.TryParse(match.Groups[1].Value, out var value))
                Vars.TotalProvinces = value;

            foreach (var item in Util.GetProvincesList(match.Groups["seaProvs"].Value))
            {
                if (Vars.Provinces.TryGetValue(item, out var province))
                    sea.Add(item, province);
                else
                    sea.Add(item, new Province(Color.FromArgb(255, 0, 0, 0)));
            }

            foreach (var item in Util.GetProvincesList(match.Groups["RnvProvs"].Value))
                if (Vars.Provinces.TryGetValue(item, out var province))
                    rnv.Add(item, province);
                else
                    rnv.Add(item, new Province(Color.FromArgb(255, 0, 0, 0)));

            foreach (var item in Util.GetProvincesList(match.Groups["LakeProvs"].Value))
                if (Vars.Provinces.TryGetValue(item, out var province))
                    lake.Add(item, province);
                else
                    lake.Add(item, new Province(Color.FromArgb(255, 0, 0, 0)));

            foreach (var item in Util.GetProvincesList(match.Groups["CostalProvs"].Value))
                if (Vars.Provinces.TryGetValue(item, out var province))
                    land.Add(item, province);
                else
                    land.Add(item, new Province(Color.FromArgb(255, 0, 0, 0)));


            foreach (var p in Vars.Provinces)
            {
                if (sea.ContainsKey(p.Key))
                    continue;
                if (rnv.ContainsKey(p.Key))
                    continue;
                if (lake.ContainsKey(p.Key))
                    continue;
                if (coastal.ContainsKey(p.Key))
                    continue;
                land.Add(p.Key, p.Value);
            }
            foreach (var p in rnv)
            {
                if (sea.ContainsKey(p.Key))
                    sea.Remove(p.Key);
                if (lake.ContainsKey(p.Key))
                    lake.Remove(p.Key);
                if (coastal.ContainsKey(p.Key))
                    coastal.Remove(p.Key);
                if (land.ContainsKey(p.Key))
                    land.Remove(p.Key);
            }
            Vars.SeaProvinces = sea;
            Vars.RnvProvinces = rnv;
            Vars.LakeProvinces = lake;
            Vars.CoastalProvinces = coastal;
            Vars.LandProvinces = land;
        }
        private static void GenerateBorders()
        {
            foreach (var province in Vars.Provinces)
            {
                var borderPixels = new List<Point>();

                foreach (var pixel in province.Value.Pixels)
                {
                    if (HasAdjacentPixelWithNewColor(pixel.X, pixel.Y, province.Value.Color))
                        borderPixels.Add(pixel);
                }

                province.Value.Border = borderPixels;
            }
        }
        private static bool HasAdjacentPixelWithNewColor(int x, int y, Color color)
        {
            if (Vars.Map == null)
                return false;
            var width = Vars.Map.Width;
            var height = Vars.Map.Height;

            // Calculate boundaries for iteration
            var startX = Math.Max(0, x - 1);
            var startY = Math.Max(0, y - 1);
            var endX = Math.Min(width - 1, x + 1);
            var endY = Math.Min(height - 1, y + 1);

            var bmpData = Vars.Map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Vars.Map.PixelFormat);

            var hasAdjacentColor = false;

            unsafe
            {
                for (var dx = startX; dx <= endX; dx++)
                {
                    for (var dy = startY; dy <= endY; dy++)
                    {
                        if (dx == x && dy == y)
                        {
                            continue; // Skip the current pixel
                        }

                        var pixelPtr = (byte*)bmpData.Scan0 + dy * bmpData.Stride + dx * 3; // Assuming 24bpp

                        var b = pixelPtr[0];
                        var g = pixelPtr[1];
                        var r = pixelPtr[2];

                        var adjacentColor = Color.FromArgb(255, r, g, b);

                        if (adjacentColor == color) continue;
                        hasAdjacentColor = true;
                        break; // Exit early if adjacent color is different from specified color
                    }
                }
            }

            Vars.Map.UnlockBits(bmpData);

            return hasAdjacentColor;
        }
        private static void InitProvinces(IReadOnlyDictionary<Color, List<Point>> dic)
        {
            Dictionary<int, Province> provinces = new();
            Dictionary<Color, int> colorIds = new();
            var lines = File.ReadAllLines(Util.GetModOrVanillaPathFile(Path.Combine("map", "definition.csv")));

            var matches = Regex.Matches(lines.ToArray().ArrayToString(), @"\s+(?:(\d+);(\d+);(\d+);(\d+);(.*);).*");

            foreach (var match in matches.Cast<Match>())
            {
                Color color;
                try
                {
                    color = Color.FromArgb(255, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                }
                catch (Exception)
                {
                    Util.ErrorPopUp("Corrupted definitions.csv", $"There is an invalid value in the definition of Province: {match.Groups[1].Value}");
                    throw;
                }
                Province p = new(color);
                if (dic.TryGetValue(color, out var entry))
                {
                    p.Pixels = entry;
                }
                else
                {
                    Vars.NotOnMapProvinces.Add(match.Groups[1].Value, color);
                    continue;
                }

                try
                {
                    p.Id = int.Parse(match.Groups[1].Value);
                    provinces.Add(p.Id, p);
                    colorIds.Add(color, int.Parse(match.Groups[1].Value));
                }
                catch (Exception)
                {
                    Util.ErrorPopUp("Corrupted definitions.csv", $"There is an invalid value in the definition of ProvinceID: {match.Groups[1].Value}");
                    throw;
                }
            }
            Vars.ColorIds = colorIds;
            Vars.Provinces = provinces;
        }
        private static Dictionary<Color, List<Point>> GetAllPixels()
        {
            Dictionary<Color, List<Point>> colors = new();
            if (Vars.Map == null)
                return colors;
            var width = Vars.Map.Width;
            var height = Vars.Map.Height;

            var bmpData = Vars.Map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Vars.Map.PixelFormat);

            var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;
            var stride = bmpData.Stride;

            unsafe
            {
                for (var y = 0; y < height; y++)
                {
                    var row = (byte*)bmpData.Scan0 + y * stride;

                    for (var x = 0; x < width; x++)
                    {
                        var pixelPtr = row + x * bytesPerPixel;

                        var b = pixelPtr[0];
                        var g = pixelPtr[1];
                        var r = pixelPtr[2];

                        var col = Color.FromArgb(255, r, g, b);

                        if (colors.TryGetValue(col, out var color))
                        {
                            color.Add(new Point(x, y));
                        }
                        else
                        {
                            colors.Add(col, new List<Point> { new Point(x, y) });
                        }
                    }
                }
            }

            Vars.Map.UnlockBits(bmpData);

            return colors;
        }

    }
}
