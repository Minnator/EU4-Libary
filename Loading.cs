using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net.Http.Json;
using System.Text;
using System.Text.RegularExpressions;
using EU4_Parse_Lib.DataClasses;
using Newtonsoft.Json;

namespace EU4_Parse_Lib
{
   public static class Loading
   {
      private static Dictionary<Color, List<Point>> _pixDic = new();
      private static Mutex _mutex = new();

      public static void LoadAll()
      {
         try
         {
            var combinedPath = Util.GetModOrVanillaPathFile(Path.Combine("map", "provinces.bmp"));
            Vars.Map = new Bitmap(combinedPath);
            //Vars.DebugMapWithBorders = new Bitmap(combinedPath);
            Vars.SelectedMapMode = new Bitmap(combinedPath);
            Vars.ProvinceAttributeNames = Util.EnumToList<ProvinceAtt>();
            Vars.CountryAttributeNames = Util.EnumToList<CountryAtt>();
            Vars.ScopeNames = Util.EnumToList<Scope>();
            Vars.Stopwatch.Start();
            Stopwatch watcher = new();
            watcher.Start();
            GetAllPixels();
            InitProvinces();
            watcher.Stop();
            Debug.WriteLine($"Init Province Time: {watcher.Elapsed}");
            Vars.Stopwatch.Stop();
            Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed Provinces:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
            Vars.Stopwatch.Reset();
            //LoadWithStopWatch("bmp Test", ProcessBitmap);
            LoadWithStopWatch("Borders", GenerateBorders);
            LoadWithStopWatch("default.map", LoadDefaultMap);
            LoadWithStopWatch("Areas", LoadAreas);
            LoadWithStopWatch("Localization", LoadAllLocalization);
            Debug.WriteLine("Finished Loading");
         }
         catch (Exception ex)
         {
            throw new Exception("Error During loading. Try restarting ur application or contact a programmer.\n" + ex);
         }
      }

      public static void LoadJSONData()
      {
         LoadKeyMappings();
      }

      public static void LoadUserData()
      {

      }

      /// <summary>
      /// Tries loading custom keymapping, if unsuccesfull reverts to default values
      /// </summary>
      public static void LoadKeyMappings()
      {
         Dictionary<Keys, Button?> keyMap = new();
         try
         {
            string jsonContent = File.ReadAllText(Path.Combine(Vars.DataPath, "UserKeyMapping.json"));
            if (jsonContent == null)
               return;
            keyMap = JsonConvert.DeserializeObject<Dictionary<Keys, Button?>>(jsonContent)!;
            if (keyMap != null)
            {
               Vars.MapModeKeyMap = keyMap;
            }
         }
         catch (Exception)
         {
            DefaultValues.DefaulMapModesKeys();
         }
         if (keyMap == null || keyMap.Count != 10)
         {
            DefaultValues.DefaulMapModesKeys();
         }
      }

      /// <summary>
      /// Pre generate random colors for later use to speed up performance in other parts of the code
      /// </summary>
      public static void FillRandomColorsList()
      {
         // An additional 100 for safety and variability
         Vars.RandomColors = Util.GenerateRandomColors(Vars.ColorIds.Count + 100);
      }

      /// <summary>
      /// Executes a Method that DOES NOT have any parameters and logs the time it took.
      /// </summary>
      /// <param name="taskName"></param>
      /// <param name="action"></param>
      public static void LoadWithStopWatch(string taskName, Action action)
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
      public static object DeserializeJson(string path)
      {
         var json = File.ReadAllText(Path.Combine(Vars.AppPath, path));
         if (json == null)
            return new object();
         return JsonConvert.DeserializeObject<object>(json)!;
      }

      /// <summary>
      /// Writes all Debug files. TODO needs to be adjusted accordingly on release
      /// </summary>
      public static void WriteDebugFiles()
      {
         Vars.Stopwatch.Start();
         DebugPrints.PrintProvincesBorder(Vars.Provinces);
         DebugPrints.PrintProvColDirectory(_pixDic);
         DebugPrints.PrintProvincesEmpty(Vars.Provinces);
         DebugPrints.PrintProvincesUnused(Vars.NotOnMapProvinces);
         DebugPrints.PrintColorsToIds(Vars.ColorIds);
         DebugPrints.PrintAreas(Vars.Areas);
         DebugPrints.PrintProvincesContent(Vars.Provinces);
         DebugPrints.PrintProvinceList();
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
         return Vars.Localization.TryGetValue(key, out var name)
             ? name
             : $"missing [{Vars.Language}] localization";
      }

      /// <summary>
      /// first read in the mod localization and then the vanilla localization.
      /// Does not care about the number in loc yet - feature for future
      /// </summary>
      public static void LoadAllLocalization()
      {
         //TODO Leaks memory
         var ymlFiles = new List<string>();
         Vars.Localization.Clear();
         Vars.LocalizationFiles.Clear();
         Vars.LocalizationHashCollisions.Clear();

         // Combine the paths for localization folders
         var modLocalizationPath = Path.Combine(Vars.ModFolder, "localisation");
         var vanillaLocalizationPath = Path.Combine(Vars.VanillaFolder, "localisation");
         if (Directory.Exists(modLocalizationPath))
         {
            ymlFiles.AddRange(Directory.GetFiles(modLocalizationPath, $"*_l_{Vars.Language}.yml").ToList());
         }
         if (Directory.Exists(vanillaLocalizationPath))
         {
            ymlFiles.AddRange(Directory.GetFiles(vanillaLocalizationPath, $"*_l_{Vars.Language}.yml").ToList());
         }

         if (ymlFiles.Count <= 0)
            return;
         {
            // Use a compiled regex pattern
            var regex = new Regex(@"(?<key>.*):\d\s+""(?<value>.*)""", RegexOptions.Compiled);

            var localizationHashCollisions = new Dictionary<string, string>();
            var loc = new Dictionary<string, string>(120000);
            var locFiles = new Dictionary<string, int>(130);

            // Use Parallel.ForEach to process files in parallel
            Parallel.ForEach(ymlFiles, ymlPath =>
            {
               try
               {
                  var lines = File.ReadAllLines(ymlPath);

                  locFiles[ymlPath] = lines.Length;

                  foreach (var line in lines)
                  {
                     var match = regex.Match(line);
                     if (!match.Success)
                        continue;
                     var id = match.Groups["key"].Value.Trim();
                     var name = match.Groups["value"].Value.Trim();

                     if (loc.ContainsKey(id))
                     {
                        var nameBuilder = new StringBuilder(name);

                        lock (localizationHashCollisions)
                           localizationHashCollisions[id] = nameBuilder.ToString();
                     }
                     else
                        lock (loc)
                           loc[id] = name;
                  }
               }
               catch (Exception ex)
               {
                  Console.WriteLine($"An error occurred while processing {ymlPath}: {ex.Message}");
               }
            });

            Vars.Localization = loc;
            Vars.LocalizationFiles = locFiles;
            Vars.LocalizationHashCollisions = localizationHashCollisions;
         }
      }

      private static void LoadAreas()
      {
         var path = Util.GetModOrVanillaPathFile(Path.Combine("map", "area.txt"));
         var newContent = Reading.ReadFileUTF8Lines(path);
         var stringBuilder = new StringBuilder();

         //Filtering Comments and optional that are not important to the areas themselves
         foreach (var line in newContent)
         {
            if (string.IsNullOrEmpty(line) || line.StartsWith('#') || line.Contains("color"))
               continue;
            stringBuilder.AppendLine(Util.RemoveCommentFromLine(line));
         }

         var content = stringBuilder.ToString();
         // Saving.WriteLog(content, "AreaContent");

         var areaDictionary = new Dictionary<string, Area>();
         var provinceRegex = new Regex(@"(?<name>[A-Za-z_]*)\s*=\s*{.*?(?<provinces>[^\}|^#]*)", RegexOptions.Singleline);
         var matches = provinceRegex.Matches(content);

         foreach (Match match in matches)
         {
            var area = new Area
            {
               Name = match.Groups["name"].Value,
               Provinces = Util.GetProvincesList(match.Groups["provinces"].Value),
               Prosperity = 0,
               Edict = "-1",
               IsStated = false
            };
            areaDictionary.Add(area.Name, area);

            foreach (var provinceId in area.Provinces)
            {
               if (!Vars.Provinces.TryGetValue(provinceId, out var province))
                  continue;
               province.Area = area.Name;
            }
         }
         Vars.Areas.Clear();
         Vars.Areas = areaDictionary;
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

         AddProvincesToDictionary(match.Groups["seaProvs"].Value, sea);
         AddProvincesToDictionary(match.Groups["RnvProvs"].Value, rnv);
         AddProvincesToDictionary(match.Groups["LakeProvs"].Value, lake);
         AddProvincesToDictionary(match.Groups["CostalProvs"].Value, land);

         foreach (var p in Vars.Provinces)
         {
            if (sea.ContainsKey(p.Key) || rnv.ContainsKey(p.Key) || lake.ContainsKey(p.Key) || coastal.ContainsKey(p.Key))
               continue;

            land.Add(p.Key, p.Value);
         }

         foreach (var p in rnv.Keys)
         {
            sea.Remove(p);
            lake.Remove(p);
            coastal.Remove(p);
            land.Remove(p);
         }

         Vars.SeaProvinces = sea;
         Vars.RnvProvinces = rnv;
         Vars.LakeProvinces = lake;
         Vars.CoastalProvinces = coastal;
         Vars.LandProvinces = land;
      }

      private static void AddProvincesToDictionary(string provinceList, IDictionary<int, Province> dictionary)
      {
         foreach (var item in Util.GetProvincesList(provinceList))
         {
            dictionary.Add(item,
                Vars.Provinces.TryGetValue(item, out var province)
                    ? province
                    : new Province(Color.FromArgb(255, 0, 0, 0)));
         }
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

               // Check N, S, W, E pixels only
               for (var dx = startX; dx <= endX; dx++)
               {
                  for (var dy = startY; dy <= endY; dy++)
                  {
                     if (dx == pixel.X && dy == pixel.Y)
                     {
                        continue; // Skip the current pixel
                     }

                     // Check only N, S, W, E neighbors
                     if (dx != pixel.X && dy != pixel.Y)
                     {
                        continue;
                     }

                     var pixelPtr = (byte*)bmpData.Scan0 + dy * bmpData.Stride + dx * 3; // Assuming 24bpp

                     var b = pixelPtr[0];
                     var g = pixelPtr[1];
                     var r = pixelPtr[2];

                     var adjacentColor = Color.FromArgb(255, r, g, b);

                     if (adjacentColor == province.Color)
                     {
                        continue;
                     }

                     hasAdjacentColor = true;
                     break; // Exit early if adjacent color is different from specified color
                  }
                  if (hasAdjacentColor)
                  {
                     break; // Exit the inner loop if adjacent color is found
                  }
               }

               if (hasAdjacentColor)
               {
                  borderPixels.Add(pixel);
               }
            }

            _mutex.WaitOne();
            Vars.BorderPixelCount += borderPixels.Count;
            _mutex.ReleaseMutex();

            province.Border = borderPixels;
         });

         Vars.BorderArray = new Point[Vars.BorderPixelCount];
         var pointer = 0;
         var cnt = 0;
         foreach (var province in Vars.Provinces.Values)
         {
            foreach (var point in province.Border)
            {
               Vars.BorderArray[cnt++] = point;
            }
            province.BorderPixel = new(pointer, pointer + province.Border.Count);
            pointer += province.Border.Count;
            province.Border.Clear(); 
         }

         DebugPrints.PrintBorderPixels();

         Vars.Map.UnlockBits(bmpData);
         
      }

      //public static unsafe void 



      //changed to Vars.SelectedMapMode from a given bitamp
      public static unsafe Bitmap ProcessBitmap(Bitmap bitmap)
      {
         var width = bitmap.Width;
         var height = bitmap.Height;
         var processedBitmap = new Bitmap(width, height);

         var bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
         var processedData = processedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
         try
         {
            const int batchSize = 16; // Process pixels in batches

            Parallel.For(0, height, y =>
            {
               for (var x = 0; x < width; x += batchSize)
               {
                  var batchEndX = Math.Min(x + batchSize, width);

                  for (var batchX = x; batchX < batchEndX; batchX++)
                  {
                     var pixelPtr = (byte*)bmpData.Scan0 + y * bmpData.Stride + batchX * 3; // Assuming 24bpp

                     var b = pixelPtr[0];
                     var g = pixelPtr[1];
                     var r = pixelPtr[2];

                     if (b == 0 && g == 0 && r == 0)
                        continue; // Skip fully black pixels

                     var hasDifferentColorNeighbor = false;
                     for (var dx = -1; dx <= 1 && !hasDifferentColorNeighbor; dx++)
                     {
                        for (var dy = -1; dy <= 1 && !hasDifferentColorNeighbor; dy++)
                        {
                           var neighborX = batchX + dx;
                           var neighborY = y + dy;

                           if (neighborX < 0 || neighborX >= width ||
                                   neighborY < 0 || neighborY >= height ||
                                   dx == 0 && dy == 0)
                              continue; // Skip the current pixel

                           var neighborPtr = (byte*)bmpData.Scan0 + neighborY * bmpData.Stride + neighborX * 3;
                           var nb = neighborPtr[0];
                           var ng = neighborPtr[1];
                           var nr = neighborPtr[2];

                           if (nb == 0 && ng == 0 && nr == 0)
                              continue; // Skip fully black neighbors

                           if (nb != b || ng != g || nr != r)
                           {
                              hasDifferentColorNeighbor = true;
                           }
                        }
                     }
                     if (hasDifferentColorNeighbor)
                        continue;

                     // Set the pixel in the processed bitmap
                     var processedPixelPtr = (byte*)processedData.Scan0 + y * processedData.Stride + batchX * 3;
                     processedPixelPtr[0] = b; // B
                     processedPixelPtr[1] = g; // G
                     processedPixelPtr[2] = r; // R

                  }
               }
            });
         }
         finally
         {
            bitmap.UnlockBits(bmpData);
            processedBitmap.UnlockBits(processedData);
            processedBitmap.Save("C:\\Users\\david\\Downloads\\bmp.bmp", ImageFormat.Bmp); // TODO: Remove on the final version
         }
         return processedBitmap;
      }
      private static void InitProvinces()
      {
         var provinces = new Dictionary<int, Province>(5000);
         var colorIds = new Dictionary<Color, int>(5000);

         var lines = File.ReadAllLines(Util.GetModOrVanillaPathFile(Path.Combine("map", "definition.csv")));

         foreach (var line in lines)
         {
            var match = Regex.Match(line, @"\s*(?:(\d+);(\d+);(\d+);(\d+);(.*);).*");

            if (!match.Success)
               continue;

            if (!int.TryParse(match.Groups[1].Value, out var id) ||
                !int.TryParse(match.Groups[2].Value, out var r) ||
                !int.TryParse(match.Groups[3].Value, out var g) ||
                !int.TryParse(match.Groups[4].Value, out var b))
            {
               Util.ErrorPopUp("Corrupted definitions.csv", $"Invalid values in the definition line: {line}");
               throw new Exception("Corrupted definitions.csv");
            }

            var color = Color.FromArgb(255, r, g, b);
            var p = new Province(color);

            if (Vars.ColorsToPixelDictionary.TryGetValue(color, out var entry))
               p.Pixels = entry.ToArray();
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
         Vars.ColorsToPixelDictionary.Clear();
      }
      private static void GetAllPixels()
      {
         Stopwatch sw = new();
         sw.Start();
         if (Vars.Map == null)
            return;
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

                  if (Vars.ColorsToPixelDictionary.TryGetValue(col, out var color))
                  {
                     color.Add(new Point(x, y));
                  }
                  else
                  {
                     Vars.ColorsToPixelDictionary.Add(col, new List<Point> { new(x, y) });
                  }
               }
            }
         }

         Vars.Map.UnlockBits(bmpData);

         sw.Stop();
         Debug.WriteLine($"Reading in Pixels: {sw.Elapsed}");

         Vars.Stopwatch.Stop();
         Vars.TimeStamps.Add("Time Elapsed drawing Borders:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
         Vars.Stopwatch.Reset();
      }

   }
}
