using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using Emgu.CV.Saliency;
using EU4_Parse_Lib.DataClasses;
using ScottPlot.Plottable.AxisManagers;

namespace EU4_Parse_Lib
{
   public static class Gui
   {
      private static int _mapModeButtonsCount;
      private static ToolTip _mapModeHoverToolTip = new();

      public static void PopulateMainWindowMapModes()
      {
         //Resetting the button bindings
         foreach (var value in Vars.MapModeKeyMap)
         {
            Vars.MapModeKeyMap[value.Key] = null;
         }
         Vars.MainWindow!.MapModeButtons.Controls.Clear();
         _mapModeButtonsCount = 0;

         var it = false;
         var leftButtonText = string.Empty;
         foreach (var mapMode in Vars.MapModes.Values)
         {
            if (!it)
            {
               leftButtonText = mapMode.Name;
               it = true;
            }
            else
            {
               CreateMapModeButtons(leftButtonText, mapMode.Name);
               it = false;
               leftButtonText = string.Empty;
            }
         }

         if (leftButtonText == string.Empty)
            return;
         TableLayoutPanel rowPanel = new()
         {
            ColumnCount = 2,
            Height = 25 // TODO fix the height and the drop down of the already created mapmodes on load
         };

         Button button1 = new()
         {
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Text = leftButtonText,
         };
         _mapModeHoverToolTip.SetToolTip(button1, leftButtonText);
         button1.Click += Vars.MainWindow.OnMapModeSelection!;
         AddToMapModeKeyMap(button1);
         rowPanel.Controls.Add(button1);

         Vars.MainWindow.MapModeButtons.Controls.Add(rowPanel);
      }

      private static void CreateMapModeButtons(string leftButtonText, string rightButtonText)
      {
         TableLayoutPanel rowPanel = new()
         {
            ColumnCount = 2,
            Height = 25
         };

         Button button1 = new()
         {
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Text = leftButtonText
         };
         _mapModeHoverToolTip.SetToolTip(button1, leftButtonText);
         button1.Click += Vars.MainWindow!.OnMapModeSelection!;
         AddToMapModeKeyMap(button1);

         Button button2 = new()
         {
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Text = rightButtonText
         };
         _mapModeHoverToolTip.SetToolTip(button2, rightButtonText);
         button2.Click += Vars.MainWindow.OnMapModeSelection!;
         AddToMapModeKeyMap(button2);

         rowPanel.Controls.Add(button1);
         rowPanel.Controls.Add(button2);

         Vars.MainWindow.MapModeButtons.Controls.Add(rowPanel);
      }


      private static void AddToMapModeKeyMap(Button mapButton)
      {
         var cnt = 0;
         foreach (var kvp in Vars.MapModeKeyMap)
         {
            if (cnt == _mapModeButtonsCount)
            {
               Vars.MapModeKeyMap[kvp.Key] = mapButton;
               mapButton.Tag = $"Map Mode {cnt}";
               Debug.WriteLine($"Mapped {mapButton} to {kvp.Key}");
               _mapModeButtonsCount++;
               return;
            }
            cnt++;
         }

      }

      /// <summary>
      /// Loads the currently selected MapMode. If an error occurs a pop up is issued with according information
      /// </summary>
      public static void ChangeMapMode()
      {
         if (Vars.MapMode == null || Vars.MainWindow!.Map == null)
         {
            Util.ErrorPopUp("Critical Error",
                $"Failed to load {Vars.MapMode} - Map Mode. Consider resetting the map, check the mapmode for errors in {Vars.DataPath}\\userMapModes.json, or restarting the application!");
            return;
         }

         Vars.MapMode.RenderMapMode();

         Vars.MainWindow.Map.Image = Vars.SelectedMapMode;
         Vars.Map!.Dispose();
         Vars.Map = new Bitmap(Vars.SelectedMapMode!);

         Vars.MainWindow.UpdateDisplayedImage();
      }


      /// <summary>
      /// Draws the black borders
      /// </summary>
      public static void DrawBorderAroundRegions()
      {
         Vars.Stopwatch.Start();
         var p = new Province(Color.FromArgb(0, 0, 0, 0));
         foreach (var province in Vars.Provinces.Values)
         {
            p.Border.AddRange(province.Border);
         }

         RenderSelection(p, p.Color);
         Vars.DebugMapWithBorders = new Bitmap(Vars.Map!);
         //Vars.ProvincesBmp!.Save("C:\\Users\\david\\Downloads\\provinces.bmp", ImageFormat.Bmp);

         var offsetX = Math.Max(0, Math.Min(Vars.Map!.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
         var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

         Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);
         Vars.MainWindow.Map.Invalidate();

         Vars.Stopwatch.Stop();
         Vars.TimeStamps.Add("Time Elapsed drawing Borders:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
         Vars.Stopwatch.Reset();
      }


      /// <summary>
      /// Creates an abstract mapmode Deprecated and Appreciated
      /// </summary>
      /// <param name="colors"></param>
      /// <param name="outputPath"></param>
      /// <returns></returns>
      public static Bitmap AbstractColorMap(List<Color> colors, string outputPath)
      {
         // Create a blank bitmap to draw the map
         var mapBitmap = new Bitmap(Vars.Map!.Width, Vars.Map.Height);

         using (var graphics = Graphics.FromImage(mapBitmap))
         {
            // Clear the bitmap with a background color (e.g., white)
            graphics.Clear(Color.White);

            var cnt = 0;
            // Draw each region with its respective color
            foreach (var kvp in Vars.LandProvinces.Values)
            {
               var regionPoints = kvp.Pixels;
               var regionColor = colors[cnt];
               cnt++;
               using var brush = new SolidBrush(regionColor);
               //var points = regionPoints.ConvertAll(p => new PointF(p.X, p.Y)).ToArray();
               //graphics.FillPolygon(brush, points);
            }
         }
         mapBitmap.Save(outputPath, ImageFormat.Bmp);
         return mapBitmap;
      }

      /// <summary>
      /// Renders the given mapmode in a very optimized way. Make sure ALL entries in the dictionary are valid. otherwise it will crash. 
      /// </summary>
      /// <exception cref="ArgumentException"></exception>
      public static void ColorMap()
      {
         Vars.Stopwatch.Start();
         if (Vars.SelectedMapModeColorMap == null || Vars.SelectedMapModeColorMap.Count == 0)
         {
            throw new ArgumentException("The colors dictionary cannot be empty or null.");
         }
         var rect = new Rectangle(0, 0, Vars.SelectedMapMode!.Width, Vars.SelectedMapMode.Height);
         var bmpData = Vars.SelectedMapMode.LockBits(rect, ImageLockMode.ReadWrite, Vars.SelectedMapMode.PixelFormat);
         Stopwatch sw = Stopwatch.StartNew();
         try
         {
            var bytesPerPixel = Image.GetPixelFormatSize(Vars.SelectedMapMode.PixelFormat) / 8;
            var stride = bmpData.Stride;
            unsafe
            {
               var ptr = (byte*)bmpData.Scan0.ToPointer();
               Parallel.ForEach(Vars.SelectedMapModeColorMap, entry =>
               {
                  var color = entry.Value;
                  foreach (var point in Vars.Provinces[entry.Key].Pixels)
                  {
                     var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                     var pixelPtr = ptr + pixelOffset;
                     pixelPtr[0] = color.B; // Blue component
                     pixelPtr[1] = color.G; // Green component
                     pixelPtr[2] = color.R; // Red component
                  }
               });
            }
         }
         finally
         {

            Vars.SelectedMapMode.UnlockBits(bmpData);
         }
         sw.Stop();
         Debug.WriteLine($"Rendering ColorMap: {sw.Elapsed}");
         //Vars.SelectedMapMode.Save("C:\\Users\\david\\Downloads\\Smap.bmp", ImageFormat.Bmp); // TODO: Remove on the final version
         Vars.Stopwatch.Stop();
         UpdateBorder();
         Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
         Vars.TimeStamps.Add($"Time Elapsed Creating Map NEW:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
         Vars.Stopwatch.Reset();
      }
      
      /// <summary>
      /// Sets the border of the given province to a given color
      /// </summary>
      /// <param name="color"></param>
      public static void RenderBorders(Color color)
      {
         Vars.Stopwatch.Start();
         if (Vars.Map == null || Vars.BorderArray!.Length < 1 || Vars.MapBorder!.Length < 1)
         {
            return; // Check for null Map
         }

         var rect = new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height);
         var bmpData = Vars.Map.LockBits(rect, ImageLockMode.ReadWrite, Vars.Map.PixelFormat);
         var stride = bmpData.Stride;
         try
         {
            var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;
            unsafe
            {
               var ptr = (byte*)bmpData.Scan0.ToPointer();

               Parallel.ForEach(Vars.BorderArray, point =>
               {
                  var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                  var pixelPtr = ptr + pixelOffset;

                  pixelPtr[0] = color.B; // Blue component
                  pixelPtr[1] = color.G; // Green component
                  pixelPtr[2] = color.R; // Red component
               });

               Parallel.ForEach(Vars.MapBorder, point =>
               {
                  var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                  var pixelPtr = ptr + pixelOffset;

                  pixelPtr[0] = color.B; // Blue component
                  pixelPtr[1] = color.G; // Green component
                  pixelPtr[2] = color.R; // Red component
               });
            }
         }
         finally
         {
            Vars.Map.UnlockBits(bmpData);
         }
         Vars.Stopwatch.Stop();
         Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
         //Debug.WriteLine($"Creating map NEW: {Vars.Stopwatch.Elapsed}");

         //Vars.Map.Save("C:\\Users\\david\\Downloads\\DrawnBorders.bmp", ImageFormat.Bmp);
         var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
         var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

         Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);

         Vars.MainWindow.Map.Invalidate();
      }

      /// <summary>
      /// Not Multi Threaded
      /// Map must not be null
      /// </summary>
      /// <param name="points"></param>
      /// <param name="color"></param>
      public static unsafe void DrawPointsOnMap(ref Point[] points, Color color)
      {
         var rect = new Rectangle(0, 0, Vars.Map!.Width, Vars.Map.Height);
         var bmpData = Vars.Map.LockBits(rect, ImageLockMode.ReadWrite, Vars.Map.PixelFormat);
         var stride = bmpData.Stride;
         try
         {
            var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;
            var ptr = (byte*)bmpData.Scan0.ToPointer();

            foreach (var point in points)
            {
               var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
               var pixelPtr = ptr + pixelOffset;

               pixelPtr[0] = color.B; // Blue component
               pixelPtr[1] = color.G; // Green component
               pixelPtr[2] = color.R; // Red component
            }
         }
         finally
         {
            Vars.Map.UnlockBits(bmpData);
         }
      }



      public static void UpdateBorder()
      {
         Vars.Stopwatch.Start();
         Stopwatch sw = new();
         sw.Start();
         if (Vars.SelectedMapMode == null || Vars.BorderArray!.Length < 1)
         {
            return; // Check for null Map
         }

         var rect = new Rectangle(0, 0, Vars.SelectedMapMode!.Width, Vars.SelectedMapMode.Height);
         var bmpData = Vars.SelectedMapMode.LockBits(rect, ImageLockMode.ReadWrite, Vars.SelectedMapMode.PixelFormat);
         var stride = bmpData.Stride;
         var bytesPerPixel = Image.GetPixelFormatSize(Vars.SelectedMapMode.PixelFormat) / 8;

         unsafe
         {
            var ptr = (byte*)bmpData.Scan0.ToPointer();
            try
            {
               Parallel.ForEach(Vars.Provinces.Values, province =>
               {
                  if (!Vars.SelectedMapModeColorMap.TryGetValue(province.Id, out var currentColor))
                  {
                     return;
                  }

                  foreach (var kvp in province.BorderProvinces)
                  {
                     if (!Vars.SelectedMapModeColorMap.TryGetValue(Vars.ColorIds[kvp.Key], out var color) || !color.Equals(currentColor))
                     {
                        var points = new Point[kvp.Value.length];
                        Array.Copy(province.BorderPixels, kvp.Value.start, points, 0, kvp.Value.length);
                        //DrawPointsOnMap(ref points, Color.FromArgb(255,0,0,0));

                        foreach (var point in points)
                        {
                           var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                           var pixelPtr = ptr + pixelOffset;

                           pixelPtr[0] = 0; // Blue component
                           pixelPtr[1] = 0; // Green component
                           pixelPtr[2] = 0; // Red component
                        }

                     }
                  }
               });
            }
            finally
            {
               Vars.SelectedMapMode.UnlockBits(bmpData);
            }
         }

         
         
         sw.Stop();
         Debug.WriteLine($"Cast and Shift: {sw.Elapsed}");
         Vars.Stopwatch.Stop();
         Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;


         //Vars.Map.Save("C:\\Users\\david\\Downloads\\BorderUpdate.bmp", ImageFormat.Bmp);
         var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
         var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

         Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);

         Vars.MainWindow.Map.Invalidate();
      }

      public static void UpdateProvinceBorder(ref List<Province> provinces)
      {
         Vars.Stopwatch.Start();
         Stopwatch sw = new();
         sw.Start();
         if (Vars.Map == null || Vars.BorderArray!.Length < 1)
         {
            return; // Check for null Map
         }

         var rect = new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height);
         var bmpData = Vars.Map.LockBits(rect, ImageLockMode.ReadWrite, Vars.Map.PixelFormat);
         var stride = bmpData.Stride;
         var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;

         unsafe
         {
            var ptr = (byte*)bmpData.Scan0.ToPointer();
            try
            {
               Parallel.ForEach(provinces, province =>
               {
                  if (!Vars.SelectedMapModeColorMap.TryGetValue(province.Id, out var currentColor))
                  {
                     return;
                  }

                  foreach (var kvp in province.BorderProvinces)
                  {
                     if (!Vars.SelectedMapModeColorMap.TryGetValue(Vars.ColorIds[kvp.Key], out var color) || !color.Equals(currentColor))
                     {
                        var points = new Point[kvp.Value.length];
                        Array.Copy(province.BorderPixels, kvp.Value.start, points, 0, kvp.Value.length);

                        foreach (var point in points)
                        {
                           var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                           var pixelPtr = ptr + pixelOffset;

                           pixelPtr[0] = 0; // Blue component
                           pixelPtr[1] = 0; // Green component
                           pixelPtr[2] = 0; // Red component
                        }
                     }
                     else //Draw the color of Province back to the map
                     {
                        var points = new Point[kvp.Value.length];
                        Array.Copy(province.BorderPixels, kvp.Value.start, points, 0, kvp.Value.length);
                        foreach (var point in points)
                        {
                           var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                           var pixelPtr = ptr + pixelOffset;

                           pixelPtr[0] = currentColor.B; // Blue component
                           pixelPtr[1] = currentColor.G; // Green component
                           pixelPtr[2] = currentColor.R; // Red component
                        }
                     }
                  }
               });
            }
            finally
            {
               Vars.Map.UnlockBits(bmpData);
            }
         }

         sw.Stop();
         Debug.WriteLine($"Deleting Previous Selection: {sw.Elapsed}");
         Vars.Stopwatch.Stop();
         Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;

         Vars.Map.Save("C:\\Users\\david\\Downloads\\BorderUpdate.bmp", ImageFormat.Bmp);
         var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
         var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

         Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);

         Vars.MainWindow.Map.Invalidate();
      }


      /// <summary>
      /// Sets the border of the given province to a given color
      /// </summary>
      /// <param name="p"></param>
      /// <param name="color"></param>
      public static void RenderSelection(Province p, Color color)
      {
         Vars.Stopwatch.Start();
         if (Vars.Map == null || Vars.BorderArray!.Length < 1)
         {
            return; // Check for null Map
         }

         var rect = new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height);
         var bmpData = Vars.Map.LockBits(rect, ImageLockMode.ReadWrite, Vars.Map.PixelFormat);
         var stride = bmpData.Stride;
         try
         {
            var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;
            unsafe
            {
               var ptr = (byte*)bmpData.Scan0.ToPointer();
               var points = new Point[p.BorderPixel.length];
               Array.Copy(Vars.BorderArray, p.BorderPixel.start, points, 0, points.Length);
               Parallel.ForEach(points, point =>
               {
                  var pixelOffset = (point.Y * stride) + (point.X * bytesPerPixel);
                  var pixelPtr = ptr + pixelOffset;

                  pixelPtr[0] = color.B; // Blue component
                  pixelPtr[1] = color.G; // Green component
                  pixelPtr[2] = color.R; // Red component
               });
            }
         }
         finally
         {
            Vars.Map.UnlockBits(bmpData);
         }
         Vars.Stopwatch.Stop();
         //Debug.WriteLine($"Time Selecting province: {Vars.Stopwatch.Elapsed}");
         Vars.Stopwatch.Reset();
         Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;

         //Vars.Map.Save("C:\\Users\\david\\Downloads\\test.bmp", ImageFormat.Bmp);
         var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
         var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

         Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);

         Vars.MainWindow.Map.Invalidate();
      }

      public static void SelectProvinceCollection(List<int> provinceList)
      {
         Vars.SelectedProvinces.Clear();
         foreach (var province in provinceList)
         {
            var p = Vars.Provinces[province];
            RenderSelection(p, Color.FromArgb(255, 255, 255, 255));
            Vars.SelectedProvinces.Add(p);
         }
      }

      /// <summary>
      /// Checks if any Form is shown or disposed and will either bring it
      /// to the front or create it. Use to prevent several instances of one
      /// form from being opened.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public static T ShowForm<T>() where T : Form, new()
      {
         Form? form = Application.OpenForms.OfType<T>().FirstOrDefault();
         if (form == null || form.IsDisposed)
         {
            form = new T();
            form.Show();
            form.BringToFront();
         }
         else
            form.BringToFront();
         return (T)form;
      }
      /// <summary>
      /// Selects everything inside of a given text box
      /// </summary>
      /// <param name="textBox"></param>
      public static void SelectOnEnter(TextBox textBox)
      {
         textBox.BeginInvoke((MethodInvoker)textBox.SelectAll);
      }
      /// <summary>
      /// Selects everything inside of a given combo box
      /// </summary>
      /// <param name="textBox"></param>
      public static void SelectAllOnFocus(this TextBox textBox)
      {
         textBox.Enter += (sender, e) =>
         {
            textBox.BeginInvoke((MethodInvoker)textBox.SelectAll);
         };
      }
      /// <summary>
      /// Puts the cursor at the end of the text. Useful for texts longer that the Texbox can show
      /// </summary>
      /// <param name="textBox"></param>
      public static void SetCursorToEnd(this TextBox textBox)
      {
         textBox.SelectionStart = textBox.Text.Length;
      }
   }
}