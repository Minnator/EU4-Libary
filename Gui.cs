using System.Diagnostics;
using System.Drawing.Imaging;

namespace EU4_Parse_Lib
{
    public static class Gui
    {
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
            Debug.WriteLine($"Creating map: {Vars.Stopwatch.Elapsed}");
            Vars.Stopwatch.Reset();
            mapBitmap.Save(file, ImageFormat.Bmp); //TODO remove on final version
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