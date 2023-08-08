using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public static class Loading
    {
        private static Dictionary<Color, List<Point>> pixDic = new();

        public static void LoadBitmap()
        {
            var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
            var map = new Bitmap(combinedPath);
            Vars.Map = map;
            Vars.stopwatch.Start();
            pixDic = GetAllPixels();
            Vars.stopwatch.Stop ();
            Vars.TimeStamps.Add($"Time Elapsed Provinces:".PadRight(30) + $"{Vars.stopwatch.Elapsed}");
            Vars.stopwatch.Reset ();
            InitProvinces(pixDic);
            Vars.stopwatch.Start();
            GenerateBorders();
            Vars.stopwatch.Stop();
            Vars.TimeStamps.Add($"Time Elapsed Borders:".PadRight(30) + $"{Vars.stopwatch.Elapsed}");
            Vars.stopwatch.Reset();

            Vars.stopwatch.Start();
            DebugPrints.PrintProvincesBorder(Vars.provinces);
            DebugPrints.PrintProvColDirectory(pixDic);
            DebugPrints.PrintProvincesEmpty(Vars.provinces);
            DebugPrints.PrintProvincesUnused(Vars.notOnMapProvs);
            DebugPrints.PrintColorsToIds(Vars.colorIds);
            Vars.stopwatch.Stop();
            Vars.TimeStamps.Add($"Time Elapsed Debug Files:".PadRight(30) + $"{Vars.stopwatch.Elapsed}");
            Vars.stopwatch.Reset();
            Saving.WriteLog(Vars.TimeStamps.ListToString(), "TimeComplexity");
            return;
        }

        private static void GenerateBorders()
        {
            foreach (var province in Vars.provinces)
            {
                foreach (var pixel in province.Value.pixels)
                {
                    if (HasAdjacentPixelWithNewColor(pixel.X, pixel.Y, province.Value.color))
                        province.Value.border.Add(pixel);
                }
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
                    p.pixels = entry;
                }
                else
                {
                    Vars.notOnMapProvs.Add(match.Groups[1].Value, color);
                    continue;
                }
               
                try
                {
                    provinces.Add(int.Parse(match.Groups[1].Value), p);
                    colorIds.Add(color, int.Parse(match.Groups[1].Value));
                }
                catch (Exception)
                {
                    Util.ErrorPopUp("Corrupted definitions.csv", $"There is an invalid value in the definition of ProvinceID: {match.Groups[1].Value}");
                    throw;
                }
            }
            Debug.WriteLine("Finished Loading");
            Vars.colorIds = colorIds;
            Vars.provinces = provinces;
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
