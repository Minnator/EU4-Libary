using System.Text;

namespace EU4_Parse_Lib;

public static class Reading
{
    /// <summary>
    /// Read all text of the given file in ANSI format
    /// Does NOT check if file exists
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ReadFileANSI(string filePath)
    {
        using StreamReader reader = new(filePath, Encoding.GetEncoding(1252));
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Reads all text as UTF-8 and returns it as a string
    /// Does NOT check if file exists
    /// </summary>
    /// <param name="path"></param>
    public static string ReadFileUTF8(string path)
    {
        using StreamReader reader = new(path, Encoding.UTF8);
        return reader.ReadToEnd();
    }
    /// <summary>
    /// Reads all text as UTF-8 and returns it as a string
    /// Does check if file exists
    /// </summary>
    /// <param name="path"></param>
    public static List<string> ReadFileUTF8Lines(string path)
    {
        if (!File.Exists(path))
            return new List<string>();
        using StreamReader reader = new (path, Encoding.UTF8);
        List<string> lines = new ();

        string? line;
        while ((line = reader.ReadLine()) != null)
            lines.Add(line);
        return lines;
    }

}
