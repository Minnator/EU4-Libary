using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

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
    /// Saves an object in JSON formatting to a file relative to the app path appending if false by default
    /// </summary>
    /// <param name="list"></param>
    /// <param name="path"></param>
    /// <param name="append"></param>
    public static void SaveListToJson<T>(List<T> list, string path, bool append = false)
    {
        var fullPath = Path.Combine(Vars.AppPath, path);
        var json = JsonConvert.SerializeObject(list, Formatting.Indented);

        if (append)
        {
            File.AppendAllText(fullPath, json);
        }
        else
        {
            File.WriteAllText(fullPath, json);
        }
    }

    public static void SaveObjectToJson(object obj, string path, bool append = false)
    {
        var fullPath = Path.Combine(Vars.AppPath, path);
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);

        if (append)
            File.AppendAllText(fullPath, json);
        else
            File.WriteAllText(fullPath, json);
    }



    // EOF

    //--------Scraped
    ///// <summary>
    ///// Saves a .bmp image as an optimized .png image with meta specified meta data
    ///// </summary>
    ///// <param name="metaData"></param>
    ///// <param name="bmp"></param>
    ///// <param name="path must contain a full path ending in .png"></param>
    //public static void SavePNGWithMetaData(string metaData, Bitmap bmp, string path)
    //{
    //    using var bitmap = new Bitmap(bmp); // Copy the pixel data from the original bmp

    //    // Set standard Exif metadata (you can add more properties as needed)
    //    SetExifMetadata(bitmap, metaData);

    //    // Save the PNG image to a file
    //    bitmap.Save(path, ImageFormat.Png);


    //    //Try this:
    //    // https://learn.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/how-to-write-metadata-to-a-bitmap?view=netframeworkdesktop-4.8
    //    // Stream pngStream = new System.IO.FileStream("smiley.png", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
    //    // PngBitmapDecoder pngDecoder = new PngBitmapDecoder(pngStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
    //    // BitmapFrame pngFrame = pngDecoder.Frames[0];
    //    // InPlaceBitmapMetadataWriter pngInplace = pngFrame.CreateInPlaceBitmapMetadataWriter();
    //    // if (pngInplace.TrySave() == true)
    //    // { pngInplace.SetQuery("/Text/Description", "Have a nice day."); }
    //    // pngStream.Close();
    //}
    //-------Scraped
    //static void SetExifMetadata(Image image, string metadata)
    //{
    //    using (MemoryStream stream = new MemoryStream())
    //    {
    //        // Encode the metadata string as UTF-16 and add a null terminator
    //        byte[] metadataBytes = Encoding.Unicode.GetBytes(metadata + '\0');

    //        // Create an Exif tag for the metadata
    //        ushort exifTag = 0x9286; // A custom Exif tag for UserComment

    //        // Write the Exif tag to the stream
    //        stream.Write(BitConverter.GetBytes((ushort)metadataBytes.Length), 0, 2);
    //        stream.Write(BitConverter.GetBytes(exifTag), 0, 2);
    //        stream.Write(metadataBytes, 0, metadataBytes.Length);

    //        // Create a new PropertyItem and set its value to the stream data
    //        PropertyItem propertyItem = (PropertyItem)FormatterServices.GetUninitializedObject(typeof(PropertyItem));
    //        propertyItem.Id = 0x9286; // UserComment tag
    //        propertyItem.Type = 2;    // ASCII
    //        propertyItem.Len = metadataBytes.Length;
    //        propertyItem.Value = stream.ToArray();

    //        // Add the property item to the image's PropertyItems collection
    //        image.SetPropertyItem(propertyItem);
    //    }
    //}



}