using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EU4_Parse_Lib.DataClasses;

namespace EU4_Parse_Lib
{
    public static class Util
    {

        public static string GetModOrVanillaPathFolder(string path)
        {
            return Directory.Exists(Path.Combine(Vars.ModFolder, path))
                ? Path.Combine(Vars.ModFolder, path)
                : Path.Combine(Vars.VanillaFolder, path);
        }
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
            var oldColor = Vars.LastProvince.color;
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

            foreach (var point in p.border)
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
            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow.Map.Width, Vars.MainWindow.displayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.displayRect.Y));

            // Update the Image property of the PictureBox
            Vars.Map = new Bitmap(bmp);
            Vars.MainWindow.Map.Image = Vars.Map;

            // Move the bitmap to the same position as when the method was called
            Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.displayRect.X, offsetY - Vars.MainWindow.displayRect.Y);
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

            foreach (var point in p.pixels)
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
            var offsetX = Math.Max(0, Math.Min(Vars.Map.Width - Vars.MainWindow.Map.Width, Vars.MainWindow.displayRect.X));
            var offsetY = Math.Max(0, Math.Min(Vars.Map.Height - Vars.MainWindow.Map.Height, Vars.MainWindow.displayRect.Y));

            // Update the Image property of the PictureBox
            Vars.Map = new Bitmap(bmp);
            Vars.MainWindow.Map.Image = Vars.Map;

            // Move the bitmap to the same position as when the method was called
            Vars.MainWindow.MoveBitmap(offsetX - Vars.MainWindow.displayRect.X, offsetY - Vars.MainWindow.displayRect.Y);

            p.currentColor = color;
        }



    }
}
