using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        public static string ArrayToString(IEnumerable<string> array)
        {
            StringBuilder sb = new();
            foreach (var item in array)
            {
               sb.Append(item).Append(Environment.NewLine);
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
            SetBorder(Vars.LastProvince, oldColor);
            Vars.CurProvince = province;
            SetBorder(province, Color.Black);
        }

        public static void SetBorder(Province p, Color color)
        {
            foreach (var point in p.border)
            {
                Vars.MainWindow.SetPixel(point.X, point.Y, color);
            }
        }

    }
}
