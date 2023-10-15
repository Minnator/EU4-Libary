using System.Runtime.InteropServices;

namespace EU4_Parse_Lib;

public static class RichTextBoxExtensions
{
    // Import the SendMessage function from user32.dll
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    // ReSharper disable once IdentifierTypo
    // ReSharper disable once InconsistentNaming
    private const int EM_REPLACESEL = 0x00C2;

    public static void SetIcon(this RichTextBox richTextBox, Bitmap iconBitmap)
    {
        // Create a bitmap handle for the icon
        var hBitmap = iconBitmap.GetHbitmap();

        // Convert the bitmap handle to an icon handle
        var hIcon = Icon.FromHandle(hBitmap).Handle;

        // Convert the icon handle to a pointer
        var hPointer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(IntPtr)));
        Marshal.WriteIntPtr(hPointer, hIcon);

        // Insert the icon into the RichTextBox
        SendMessage(richTextBox.Handle, EM_REPLACESEL, (IntPtr)1, hPointer);

        // Clean up resources
        Marshal.FreeCoTaskMem(hPointer);
        DeleteObject(hBitmap);
    }

    // Import the DeleteObject function from gdi32.dll to release the icon handle
    [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DeleteObject(IntPtr hObject);
}