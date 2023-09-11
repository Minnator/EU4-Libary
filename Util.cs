using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EU4_Parse_Lib.DataClasses;
using EU4_Parse_Lib.Interfaces;
using Markdig;

namespace EU4_Parse_Lib
{
    public static class Util
    {
        public static Color GetGradientColor(int minValue, int maxValue, int value)
        {
            var normalizedValue = (double)(value - minValue) / (maxValue - minValue);

            var red = (int)(255 * (1 - normalizedValue));
            var green = (int)(255 * normalizedValue);

            return Color.FromArgb(red, green, 0);
        }
        /// <summary>
        /// Populates a list from a given Enum consisting of the strings of the enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> EnumToList<T>()
        {
            List<string> enumNames = new ();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                enumNames.Add(value.ToString() ?? string.Empty);
            }
            return enumNames;
        }
        /// <summary>
        /// Checks whether a string is defined as a parameter in an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsStringInEnum<T>(string value) where T : Enum
        {
            return Enum.IsDefined(typeof(T), value);
        }


        public static void SetMapMode(IMapMode mapMode)
        {

        }
        /// <summary>
        /// RN it defaults to the Web browser in the Help window
        /// </summary>
        /// <param name="markdownFilePath"></param>
        public static void ConvertAndDisplayInWebBrowser(string markdownFilePath)
        {
            Debug.WriteLine(markdownFilePath);
            //markdownFilePath = Path.Combine("C:\\Users\\david\\source\\repos\\EU4 Parsed Lib\\bin\\Debug\\net6.0\\data",
            //    "README.md");
            var markdownContent = File.ReadAllText(markdownFilePath);
            var htmlContent = Markdown.ToHtml(markdownContent);
            
            Vars.WebBrowserForm!.WebBrowser.DocumentText = htmlContent;
        }

        public static void ChangePixelsColor(List<Point> pixels, Color newColor)
        {
            lock (Vars.BitmapLock)
            {
                using Bitmap bmp = new (Vars.Map!);
                var bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
                var bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
                var stride = bmpData.Stride;

                unsafe
                {
                    foreach (var point in pixels)
                    {
                        var x = point.X;
                        var y = point.Y;

                        if (x < 0 || x >= bmp.Width || y < 0 || y >= bmp.Height)
                            continue;

                        var pixelPtr = (byte*)bmpData.Scan0 + y * stride + x * bytesPerPixel;

                        pixelPtr[0] = newColor.B;
                        pixelPtr[1] = newColor.G;
                        pixelPtr[2] = newColor.R;
                    }
                }

                bmp.UnlockBits(bmpData);

                Vars.Map = new Bitmap(bmp);
                Vars.MainWindow!.Map.Image = Vars.Map;
            }
        }

        public static string GetModOrVanillaPathFolder(string path)
        {
            return Directory.Exists(Path.Combine(Vars.ModFolder, path))
                ? Path.Combine(Vars.ModFolder, path)
                : Path.Combine(Vars.VanillaFolder, path);
        }
        /// <summary>
        /// Returns a list of all numbers in that string, if they are separated by an empty space
        /// Has no error log yet for failed Parsing to int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<int> GetProvincesList(string str)
        {
            List<int> idList = new();

            var matches = Regex.Matches(str, @"\s*(\d+)");
            foreach (var match in matches)
            {
                if (int.TryParse(match.ToString(), out var value))
                    idList.Add(value);
                // else { ERROR message here }
            }
            return idList;
        }
        /// <summary>
        /// Get is existing the path from the mod if not defaults to vanilla
        /// if vanilla is faulty crash
        /// REQUIRES only path RELATIVE to game
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetModOrVanillaPathFile(string path)
        {
            return File.Exists(Path.Combine(Vars.ModFolder, path))
                ? Path.Combine(Vars.ModFolder, path)
                : Path.Combine(Vars.VanillaFolder, path);
        }
        public static string ArrayToString<T>(this IEnumerable<T> array)
        {
            StringBuilder sb = new();
            foreach (var item in array.Where(item => item != null))
            {
               sb.AppendLine(item?.ToString());
            }
            return sb.ToString();
        }

        public static string ListToString<T>(this IEnumerable<T> list)
        {
            StringBuilder sb = new();
            foreach (var item in list.Where(item => item != null))
            {
                sb.AppendLine(item?.ToString());
            }
            return sb.ToString();
        }
        public static void ErrorPopUp(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void NextProvince(Province province)
        {
            if (Vars.MainWindow == null) 
                return;
            Vars.CurProvince ??= province;
            Vars.LastProvince = Vars.CurProvince;
            var oldColor = Vars.LastProvince.Color;
            SetProvinceBorder(Vars.LastProvince, oldColor);
            Vars.CurProvince = province;
            SetProvinceBorder(province, Color.Black);
        }

        public static void SetProvinceBorder(Province p, Color color)
        {
            if (Vars.MainWindow == null || Vars.Map == null)
                return;
            using Bitmap bmp = new (Vars.Map);
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            var bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            var stride = bmpData.Stride;

            foreach (var point in p.Border)
            {
                var x = point.X;
                var y = point.Y;

                if (x < 0 || x >= bmp.Width || y < 0 || y >= bmp.Height) continue;
                var pixelPtr = bmpData.Scan0 + y * stride + x * bytesPerPixel;
                var pixelBytes = new byte[bytesPerPixel];

                Marshal.Copy(pixelPtr, pixelBytes, 0, bytesPerPixel);

                pixelBytes[0] = color.B;
                pixelBytes[1] = color.G;
                pixelBytes[2] = color.R;

                Marshal.Copy(pixelBytes, 0, pixelPtr, bytesPerPixel);
            }

            bmp.UnlockBits(bmpData);

            // Calculate the corresponding offset for the modified bmp
            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow.Map.Width, Vars.MainWindow.DisplayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

            // Update the Image property of the PictureBox
            Vars.Map = new Bitmap(bmp);
            Vars.MainWindow.Map.Image = Vars.Map;

            // Move the bitmap to the same position as when the method was called
            Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);
        }

        public static void SetProvincePixels(Province p, Color color)
        {
            if (Vars.MainWindow == null || Vars.Map == null)
                return;
            using Bitmap bmp = new (Vars.Map);
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            var bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            var stride = bmpData.Stride;

            foreach (var point in p.Pixels)
            {
                var x = point.X;
                var y = point.Y;

                if (x < 0 || x >= bmp.Width || y < 0 || y >= bmp.Height) continue;
                var pixelPtr = bmpData.Scan0 + y * stride + x * bytesPerPixel;
                var pixelBytes = new byte[bytesPerPixel];

                Marshal.Copy(pixelPtr, pixelBytes, 0, bytesPerPixel);

                pixelBytes[0] = color.B;
                pixelBytes[1] = color.G;
                pixelBytes[2] = color.R;

                Marshal.Copy(pixelBytes, 0, pixelPtr, bytesPerPixel);
            }

            bmp.UnlockBits(bmpData);

            // Calculate the corresponding offset for the modified bmp
            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow.Map.Width, Vars.MainWindow.DisplayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.DisplayRect.Y));

            // Update the Image property of the PictureBox
            Vars.Map = new Bitmap(bmp);
            Vars.MainWindow.Map.Image = Vars.Map;

            // Move the bitmap to the same position as when the method was called
            //Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.DisplayRect.X, offsetY - Vars.MainWindow.DisplayRect.Y);

            p.CurrentColor = color;
        }

        /*
        public static void PrintListToConsole(IEnumerable<object> list)
        {
            Debug.WriteLine($"Items: {list.Count()}");
            foreach (var o in list)
            {
                Debug.WriteLine(o.ToString());
            }
        }
        */
    }
}
