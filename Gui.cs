﻿using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public static class Gui
    {
        /// <summary>
        /// Draws the black borders
        /// </summary>
        /// <param name="borderColor"></param>
        public static void DrawBorderAroundRegions()
        {
            Vars.Stopwatch.Start();
            var p = new Province(Color.FromArgb(0, 0, 0, 0));
            foreach (var province in Vars.Provinces.Values)
            {
                p.Border.AddRange(province.Border);
            }

            RenderSelection(p, p.Color);

            Vars.Map.Save("C:\\Users\\david\\Downloads\\justBorders.bmp", ImageFormat.Bmp);

            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

            Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);
            Vars.MainWindow.Map.Invalidate();

            Vars.Stopwatch.Stop();
            Vars.TimeStamps.Add($"Time Elapsed drawing Borders:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
            Vars.Stopwatch.Reset();
        }



        /// <summary>
        /// Renders the given mapmode in a very optimized way. Make sure ALL entries in the dictionary are valid. otherwise it will crash. 
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="file"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ColorMap(Dictionary<int, Color> colors, string file)
        {
            Vars.Stopwatch.Start();
            if (colors == null || colors.Count == 0)
            {
                throw new ArgumentException("The colors dictionary cannot be empty or null.");
            }
            var mapWidth = Vars.Map!.Width;
            var mapHeight = Vars.Map.Height;

            using var mapBitmap = new Bitmap(mapWidth, mapHeight);
            var bitmapData = mapBitmap.LockBits(new Rectangle(0, 0, mapWidth, mapHeight),
                ImageLockMode.WriteOnly, mapBitmap.PixelFormat);
            try
            {
                var bytesPerPixel = Image.GetPixelFormatSize(mapBitmap.PixelFormat) / 8;
                var stride = bitmapData.Stride;
                var pixelData = new byte[mapHeight * stride];
                var scan0 = bitmapData.Scan0;

                Parallel.ForEach(colors, entry =>
                {
                    var regionPoints = Vars.Provinces[entry.Key].Pixels;

                    foreach (var point in regionPoints)
                    {
                        var pixelOffset = point.Y * stride + point.X * bytesPerPixel;
                        var colorValue = entry.Value.ToArgb();
                        pixelData[pixelOffset + 0] = (byte)(colorValue);
                        pixelData[pixelOffset + 1] = (byte)(colorValue >> 8);
                        pixelData[pixelOffset + 2] = (byte)(colorValue >> 16);
                        pixelData[pixelOffset + 3] = (byte)(colorValue >> 24);
                    }
                });

                // Copy the modified pixel data back to the bitmap
                unsafe
                {
                    fixed (byte* pData = pixelData)
                    {
                        var pDest = (byte*)scan0.ToPointer();
                        for (var i = 0; i < mapHeight; i++)
                        {
                            Buffer.MemoryCopy(pData + i * stride, pDest + i * stride, stride, stride);
                        }
                    }
                }
            }
            finally
            {
                mapBitmap.UnlockBits(bitmapData);
            }
            Vars.Stopwatch.Stop();
            Vars.TotalLoadTime += Vars.Stopwatch.Elapsed;
            Vars.TimeStamps.Add($"Time Elapsed Creating Map:".PadRight(30) + $"| {Vars.Stopwatch.Elapsed} |");
            //Debug.WriteLine($"Creating map: {Vars.Stopwatch.Elapsed}");
            Vars.Stopwatch.Reset();
            mapBitmap.Save(file, ImageFormat.Bmp); //TODO remove on final version
        }

        /// <summary>
        /// Sets the border of the given province to a given color
        /// </summary>
        /// <param name="p"></param>
        /// <param name="color"></param>
        public static void RenderSelection(Province p, Color color)
        {
            if (Vars.Map == null)
            {
                return; // Check for null Map
            }

            var rect = new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height);
            var bmpData = Vars.Map.LockBits(rect, ImageLockMode.ReadWrite, Vars.Map.PixelFormat);

            try
            {
                var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;
                unsafe
                {
                    var ptr = (byte*)bmpData.Scan0.ToPointer();

                    Parallel.ForEach(p.Border, point =>
                    {
                        var pixelOffset = (point.Y * bmpData.Stride) + (point.X * bytesPerPixel);
                        var pixelPtr = ptr + pixelOffset;

                        pixelPtr[0] = color.B; // Blue component
                        pixelPtr[1] = color.G; // Green component
                        pixelPtr[2] = color.R; // Red component
                        pixelPtr[3] = color.A; // Alpha component (transparency)
                    });
                }
            }
            finally
            {
                Vars.Map.UnlockBits(bmpData);
            }

            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow!.Map.Width, Vars.MainWindow.DisplayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

            Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);
            Vars.MainWindow.Map.Invalidate();
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