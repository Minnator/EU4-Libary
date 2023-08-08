using System.Diagnostics;
using System.Text;

namespace EU4_Parse_Lib;

public static class Saving
{
    /// <summary>
    /// saves any kind of .txt file to the provide path!
    /// </summary>
    /// <param name="path"></param>
    /// <param name="text"></param>
    public static void SaveTextFile(string path, string text)
    {
        if (File.Exists(path) || text == null)
            return;
        Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
        using StreamWriter writer = new(path, false, Encoding.UTF8);
        writer.NewLine = "\n"; // Set the line ending to Unix-style LF
        writer.Write(text);
        writer.Flush();
        writer.Close();
    }
    public static void WriteLog(string text, string filename)
    {
        var streamWriter = new StreamWriter(Vars.appPath + $"{filename}.txt");
        streamWriter.WriteLine(text);
        streamWriter.Close();
    }
    private static void WriteFileANSI(string path, string text, bool append = false)
    {
        if (text == null)
            return;
        //Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
        using StreamWriter writer = new(path, append, Encoding.GetEncoding(1252));
        writer.Write(text);
    }
}