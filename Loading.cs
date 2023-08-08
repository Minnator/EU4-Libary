using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            pixDic = GetAllPixels();
            InitProvinces(pixDic);
            DebugPrints.PrintProvColDirectory(pixDic);
            DebugPrints.PrintProvincesEmpty(Vars.provinces);
            DebugPrints.PrintProvincesUnused(Vars.notOnMapProvs);
            DebugPrints.PrintColorsToIds(Vars.colorIds);
            GenerateBorders();
            DebugPrints.PrintProvincesBorder(Vars.provinces);
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
            var width = Vars.Map.Width;
            var height = Vars.Map.Height;

            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    if ((dx == 0 && dy == 0) || x + dx < 0 || x + dx >= width || y + dy < 0 || y + dy >= height)
                    {
                        continue; // Skip the current pixel and out-of-bounds pixels
                    }

                    var adjacentColor = Vars.Map.GetPixel(x + dx, y + dy);
                    if (adjacentColor != color)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void InitProvinces(IReadOnlyDictionary<Color, List<Point>> dic)
        {
            Dictionary<int, Province> provinces = new();
            Dictionary<Color, int> colorIds = new();
            var lines = File.ReadAllLines(Util.GetModOrVanillaPathFile(Path.Combine("map", "definition.csv")));
            
            var matches = Regex.Matches(Util.ArrayToString(lines.ToArray()), @"\s+(?:(\d+);(\d+);(\d+);(\d+);(.*);).*");

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
            var width = Vars.Map.Width;
            var height = Vars.Map.Height;
            Dictionary<Color, List<Point>> colors = new();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var col = Vars.Map.GetPixel(x, y);
                    col = Color.FromArgb(col.R, col.G, col.B);
                    if (colors.TryGetValue(col, out var color))
                    {
                        color.Add(new Point(x, y));
                    }
                    else
                        colors.Add(col, new List<Point>{ new(x, y) });
                }
            }
            return colors;
        }

    }
}
