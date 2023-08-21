using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Xml;

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
        if (File.Exists(path))
            return;
        Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
        using StreamWriter writer = new(path, false, Encoding.UTF8);
        writer.NewLine = "\n"; // Set the line ending to Unix-style LF
        writer.Write(text);
        writer.Flush();
        writer.Close();
    }
    /// <summary>
    /// dumps all given text into a rudimentary file.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="filename"></param>
    /// <param name="append"></param>
    public static void WriteLog(string text, string filename, bool append = false)
    {
        var streamWriter = new StreamWriter(Vars.AppPath + $"{filename}.txt", append);
        streamWriter.WriteLine(text);
        streamWriter.Close();
    }
    /// <summary>
    /// saves a file in the ANSI encoding to preserve all special characters
    /// </summary>
    /// <param name="path"></param>
    /// <param name="text"></param>
    /// <param name="append"></param>
    private static void WriteFileAnsi(string path, string text, bool append = false)
    {
        //Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
        using StreamWriter writer = new(path, append, Encoding.GetEncoding(1252));
        writer.Write(text);
    }
    /// <summary>
    /// Saves an object in JSON formatting to a file relative to the app path
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    public static void SaveToJson(object obj, string path)
    {
        File.WriteAllText(Path.Combine(Vars.AppPath, path), JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented));
    }
}