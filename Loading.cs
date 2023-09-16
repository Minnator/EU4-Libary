using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Emgu.CV.Reg;
using EU4_Parse_Lib.DataClasses;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace EU4_Parse_Lib
{
    public static class Loading
    {
        private static Dictionary<Color, List<Point>> _pixDic = new();

        public static void LoadAll()
        {
            try
            {
                var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
                var map = new Bitmap(combinedPath);
                Vars.Map = map;
                Vars.ProvinceAttributeNames = Util.EnumToList<ProvinceAtt>();
                Vars.CountryAttributeNames = Util.EnumToList<CountryAtt>();
                Vars.ScopeNames = Util.EnumToList<Scope>();
                Vars.Stopwatch.Start();
                _pixDic = GetAllPixels();
                InitProvinces(_pixDic);
                Vars.Stopwatch.Stop();
                Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
                Vars.TimeStamps.Add($"Time Elapsed Provinces:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
                Vars.Stopwatch.Reset();
                LoadWithStopWatch("Borders", GenerateBorders);
                LoadWithStopWatch("default.map", LoadDefaultMap);
                LoadWithStopWatch("Areas", LoadAreas);
                LoadWithStopWatch("Localization", LoadAllLocalization);
                Gui.DrawBorderAroundRegions();
                LoadWithStopWatch("Random Colors", FillRandomColorsList);
                WriteDebugFiles();
                Debug.WriteLine("Finished Loading");
            }
            catch (InvalidCastException ex)
            {
                throw new Exception("fuuuuuuuuuuuuuuuuuuuuuuuuck" + ex);
            }
        }

        private static void FillRandomColorsList()
        {
            // An additional 100 for safety and variability
            Vars.RandomColors = Util.GenerateRandomColors(Vars.ColorIds.Count + 100);
        }

        /// <summary>
        /// Executes a Method that DOES NOT have any parameters and logs the time it took.
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="action"></param>
        private static void LoadWithStopWatch(string taskName, Action action)
        {
            Vars.Stopwatch.Start();
            action.Invoke();
            Vars.Stopwatch.Stop();
            Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed {taskName}:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
            Vars.Stopwatch.Reset();
        }
        /// <summary>
        /// Deserialize any kind of object form JSON of the given path relative to app path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<object> DeserializeJson(string path)
        {
            var json = File.ReadAllText(Path.Combine(Vars.AppPath, path));
            return JsonConvert.DeserializeObject<List<object>>(json) ?? new List<object>();
        }
        private static void WriteDebugFiles()
        {
            Vars.Stopwatch.Start();
            //DebugPrints.PrintProvincesBorder(Vars.Provinces);
            //DebugPrints.PrintProvColDirectory(_pixDic);
            //DebugPrints.PrintProvincesEmpty(Vars.Provinces);
            //DebugPrints.PrintProvincesUnused(Vars.NotOnMapProvinces);
            //DebugPrints.PrintColorsToIds(Vars.ColorIds);
            //DebugPrints.PrintAreas(Vars.Areas);
            //DebugPrints.PrintProvincesContent(Vars.Provinces);
            //DebugPrints.PrintProvinceList();
            DebugPrints.PrintLocData(Vars.LocalizationFiles);
            //DebugPrints.PrintAttributes(Vars.Provinces);
            DebugPrints.PrintTestTriggerValue();
            DebugPrints.PrintRandomColors();
            Vars.Stopwatch.Stop();
            Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed Debug Files:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
            Vars.Stopwatch.Reset();
            Vars.TimeStamps.Add("------------------------------|------------------|");
            Vars.TimeStamps.Add($"Total Time Elapsed:".PadRight(30) + $"| {Vars.TotalLoadTime} |");
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
                ymlFiles.AddRange(Directory.GetFiles(path, $"*_l_{Vars.Language}.yml").ToList());
            path = Path.Combine(Vars.VanillaFolder, "localisation");
            if (Directory.Exists(path))
                ymlFiles.AddRange(Directory.GetFiles(path, $"*_l_{Vars.Language}.yml").ToList());

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

            var contentBuilder = new StringBuilder();
            foreach (var line in newContent)
            {
                if (!string.IsNullOrEmpty(line) && !line.StartsWith('#') && !line.Contains("color"))
                {
                    contentBuilder.AppendLine(line);
                }
            }

            var content = contentBuilder.ToString();
            // Saving.WriteLog(content, "AreaContent");

            var areas = new Dictionary<string, Area>();
            var provinceRegex = new Regex(@"(?<name>[A-Za-z_]*)\s*=\s*{.*?(?<provinces>[^\}|^#]*)", RegexOptions.Singleline);
            var matches = provinceRegex.Matches(content);

            foreach (Match match in matches)
            {
                var area = new Area();
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
            
            foreach (var item in Util.GetProvincesList(match.Groups["seaProvs"].Value))
            {
                sea.Add(item,
                    Vars.Provinces.TryGetValue(item, out var province)
                        ? province
                        : new Province(Color.FromArgb(255, 0, 0, 0)));
            }

            foreach (var item in Util.GetProvincesList(match.Groups["RnvProvs"].Value))
                rnv.Add(item,
                    Vars.Provinces.TryGetValue(item, out var province)
                        ? province
                        : new Province(Color.FromArgb(255, 0, 0, 0)));

            foreach (var item in Util.GetProvincesList(match.Groups["LakeProvs"].Value))
                lake.Add(item,
                    Vars.Provinces.TryGetValue(item, out var province)
                        ? province
                        : new Province(Color.FromArgb(255, 0, 0, 0)));

            foreach (var item in Util.GetProvincesList(match.Groups["CostalProvs"].Value))
                land.Add(item,
                    Vars.Provinces.TryGetValue(item, out var province)
                        ? province
                        : new Province(Color.FromArgb(255, 0, 0, 0)));

            
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

        private static unsafe void GenerateBorders()
        {
            if (Vars.Map == null)
                return;
            var width = Vars.Map.Width;
            var height = Vars.Map.Height;

            var bmpData = Vars.Map.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Vars.Map.PixelFormat);

            Parallel.ForEach(Vars.Provinces.Values, province =>
            {
                var borderPixels = new List<Point>();

                foreach (var pixel in province.Pixels)
                {
                    var startX = Math.Max(0, pixel.X - 1);
                    var startY = Math.Max(0, pixel.Y - 1);
                    var endX = Math.Min(width - 1, pixel.X + 1);
                    var endY = Math.Min(height - 1, pixel.Y + 1);

                    var hasAdjacentColor = false;

                    for (var dx = startX; dx <= endX; dx++)
                    {
                        for (var dy = startY; dy <= endY; dy++)
                        {
                            if (dx == pixel.X && dy == pixel.Y)
                            {
                                continue; // Skip the current pixel
                            }

                            var pixelPtr = (byte*)bmpData.Scan0 + dy * bmpData.Stride + dx * 3; // Assuming 24bpp

                            var b = pixelPtr[0];
                            var g = pixelPtr[1];
                            var r = pixelPtr[2];

                            var adjacentColor = Color.FromArgb(255, r, g, b);

                            if (adjacentColor != province.Color)
                            {
                                hasAdjacentColor = true;
                                break; // Exit early if adjacent color is different from specified color
                            }
                        }
                        if (hasAdjacentColor)
                            break; // Exit the inner loop if adjacent color is found
                    }

                    if (hasAdjacentColor)
                        borderPixels.Add(pixel);
                }

                province.Border = borderPixels;
            });

            Vars.Map.UnlockBits(bmpData);
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
            Dictionary<int, Province> provinces = new Dictionary<int, Province>();
            Dictionary<Color, int> colorIds = new Dictionary<Color, int>();

            var lines = File.ReadAllLines(Util.GetModOrVanillaPathFile(Path.Combine("map", "definition.csv")));

            foreach (var line in lines)
            {
                var match = Regex.Match(line, @"\s*(?:(\d+);(\d+);(\d+);(\d+);(.*);).*");

                if (!match.Success)
                {
                    continue;
                }

                int id, r, g, b;

                if (!int.TryParse(match.Groups[1].Value, out id) ||
                    !int.TryParse(match.Groups[2].Value, out r) ||
                    !int.TryParse(match.Groups[3].Value, out g) ||
                    !int.TryParse(match.Groups[4].Value, out b))
                {
                    Util.ErrorPopUp("Corrupted definitions.csv", $"Invalid values in the definition line: {line}");
                    throw new Exception("Corrupted definitions.csv");
                }

                Color color = Color.FromArgb(255, r, g, b);
                Province p = new Province(color);

                if (dic.TryGetValue(color, out var entry))
                {
                    p.Pixels = entry;
                }
                else
                {
                    Vars.NotOnMapProvinces.Add(id.ToString(), color);
                    continue;
                }

                p.Id = id;
                provinces.Add(id, p);
                colorIds.Add(color, id);
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
                            colors.Add(col, new List<Point> { new(x, y) });
                        }
                    }
                }
            }

            Vars.Map.UnlockBits(bmpData);

            return colors;
        }


    }
}
