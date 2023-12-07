using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;


namespace EU4_Parse_Lib;

public static class GpuGui
{
   private static Context _context = null!;
   private static Accelerator _device = null!;
   
   private static Action<Index1D, ArrayView<Point>, ArrayView<byte>, dPixelBuffer2D<byte>> _renderSelectionKernel = null!;

   private static readonly PixelBuffer2D<byte> FrameBuffer;


   static GpuGui()
   {
      InitGpu(false);
      FrameBuffer = new PixelBuffer2D<byte>(_device!, Vars.Map!.Height, Vars.Map.Width);
   }


   private static void InitGpu(bool forceCpu)
   {
      _context = Context.Create(builder => builder.Cuda().CPU().EnableAlgorithms());
      _device = _context.GetPreferredDevice(forceCpu).CreateAccelerator(_context);


      _renderSelectionKernel = _device.LoadAutoGroupedStreamKernel<Index1D, ArrayView<Point>, ArrayView<byte>, dPixelBuffer2D<byte>>(DrawSelection);
   }

   private static void DrawSelection(Index1D index, ArrayView<Point> border, ArrayView<byte> color,
      dPixelBuffer2D<byte> output)
   {
      //bgr format
      output.WriteFrameBuffer(border[index].X, border[index].Y, color[2], color[1], color[0]);
   }

   private static void CopyExistingBitmapToFrameBuffer()
   {
      var bmpData = Vars.Map!.LockBits(new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height), ImageLockMode.ReadOnly, Vars.Map.PixelFormat);
      //Copy the data from the bitmap to the array
      unsafe
      {
         var src = (byte*)bmpData.Scan0.ToPointer(); 
         var dest = FrameBuffer.GetRawFrameData();
         for (var i = 0; i < dest.Length; i++)
            dest[i] = src[i];
      }
      Vars.Map.UnlockBits(bmpData);
   }

   public static void RenderSelection(Point[] points, Color color)
   {
      var sw = new Stopwatch();
      sw.Start();
      var borderView = _device.Allocate1D(points);
      CopyExistingBitmapToFrameBuffer();
      var colorRgb = _device.Allocate1D(new[] { color.R, color.G, color.B });

      _renderSelectionKernel(points.Length - 1, borderView.View, colorRgb.View, FrameBuffer.Get1DPixelBuffer());
      _device.Synchronize();
      Debug.WriteLine($"Rendered Selection in {sw.Elapsed}");
      UpdateMap(ref FrameBuffer.GetRawFrameData());
      sw.Stop();
   }

   public static void Start(Color color)
   {
      var sw = new Stopwatch();
      sw.Start();
      var borderView = _device.Allocate1D(Vars.BorderArray!);
      CopyExistingBitmapToFrameBuffer();
      var colorRgb = _device.Allocate1D(new [] { color.R, color.G, color.B });

      _renderSelectionKernel(Vars.BorderArray!.Length - 1, borderView.View, colorRgb.View, FrameBuffer.Get1DPixelBuffer());
      _device.Synchronize();
      Debug.WriteLine($"Rendered frame in {sw.Elapsed}");
      UpdateMap(ref FrameBuffer.GetRawFrameData());
      sw.Stop();
      //Vars.Map!.Save(@"C:\Users\david\Downloads\GPU_Borders.bmp");
   }

   private static void UpdateMap(ref byte[] data)
   {
      unsafe
      {
         if (Vars.Map == null) 
            return;
         var bmpData = Vars.Map.LockBits(new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height), ImageLockMode.WriteOnly, Vars.Map.PixelFormat);
         if (data.Length != Vars.Map.Width * Vars.Map.Height * 3) 
            return;
         var ptr = (IntPtr)bmpData.Scan0.ToPointer();
         Marshal.Copy(data, 0, ptr, data.Length);
         Vars.Map.UnlockBits(bmpData);
      }
      
   }

   
   public static Rectangle DetectEditedRectangle(ref Point[] borderArray)
   {
      if (borderArray.Length == 0)
         return Rectangle.Empty;

      var minX = 1;
      var minY = 1;
      var maxX = 1;
      var maxY = 1;

      foreach (var point in borderArray)
      {
         minX = Math.Min(minX, point.X);
         minY = Math.Min(minY, point.Y);
         maxX = Math.Max(maxX, point.X);
         maxY = Math.Max(maxY, point.Y);
      }

      return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
   }
}