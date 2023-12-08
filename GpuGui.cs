using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using EU4_Parse_Lib.DataClasses;
using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.CPU;
using ILGPU.Runtime.Cuda;


namespace EU4_Parse_Lib;

public static class GpuGui
{
   private static Context _context = null!;
   private static Accelerator _device = null!;

   public struct ProvinceGpu
   {
      public byte r, g, b;
      public int count;

      public ProvinceGpu(byte r, byte g, byte b, int c)
      {
         this.r = r;
         this.g = g;
         this.b = b;
         count = c;
      }
   }


   private static Action<Index1D, ArrayView<Point>, ArrayView<byte>, dPixelBuffer2D<byte>> _renderSelectionKernel = null!;

   private static Action<Index1D, dPixelBuffer2D<byte>, ArrayView<byte>, ArrayView<Point>, ArrayView<Point>, ArrayView<int>> _calculateBordersKernel = null!;

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
      _calculateBordersKernel = _device.LoadAutoGroupedStreamKernel<Index1D, dPixelBuffer2D<byte>, ArrayView<byte>, ArrayView<Point>, ArrayView<Point>, ArrayView<int>>(GetProvinceBorders);
   }


   private static void CopyExistingBitmapToFrameBuffer()
   {
      var bmpData = Vars.Map!.LockBits(new Rectangle(0, 0, Vars.Map.Width, Vars.Map.Height), ImageLockMode.ReadOnly, Vars.Map.PixelFormat);

      try
      {
         var stride = bmpData.Stride;
         var bytesPerPixel = Image.GetPixelFormatSize(Vars.Map.PixelFormat) / 8;

         if (bytesPerPixel != 3 && bytesPerPixel != 4)
         {
            throw new NotSupportedException("Unsupported pixel format");
         }

         var dest = FrameBuffer.GetRawFrameData();

         unsafe
         {
            fixed (byte* destPtr = dest)
            {
               var srcPtr = (byte*)bmpData.Scan0.ToPointer();
               var destOffset = 0;

               for (var y = 0; y < Vars.Map.Height; y++)
               {
                  // Calculate the start of the current row in memory
                  var rowStart = srcPtr + (y * stride);

                  // Copy the entire row (all pixels) to the destination buffer
                  Buffer.MemoryCopy(rowStart, destPtr + destOffset, Vars.Map.Width * bytesPerPixel, Vars.Map.Width * bytesPerPixel);

                  destOffset += Vars.Map.Width * bytesPerPixel;
               }
            }
         }
      }
      finally
      {
         Vars.Map.UnlockBits(bmpData);
      }
   }


   private static void DrawSelection(Index1D index, ArrayView<Point> border, ArrayView<byte> color,
      dPixelBuffer2D<byte> output)
   {
      //bgr format
      output.WriteFrameBuffer(border[index].X, border[index].Y, color[2], color[1], color[0]);
   }

   //Out array has same length as pixels are in the bitmap, province starts writing its border where its pixels would start. Thus there will be empty regions in the array, afterwards we can just remove them and they tell us which region belongs to which province and thus we can assign the pointers to where the province border starts and ends
   // called for every province
   private static void CalculateBorders(Index1D index, ArrayView<Point> pixels, dPixelBuffer2D<byte> output, ArrayView<Point> border)
   {
      
   }

   private static void GetProvinceBorders(Index1D index, dPixelBuffer2D<byte> bitmap, ArrayView<byte> color, ArrayView<Point> pixels, ArrayView<Point> border, ArrayView<int> offset)
   {
      //Check if the pixel north, south, east or west has a different color than color
      var currentPixel = pixels[index];
      var currentColor = bitmap.ReadFrameBuffer(currentPixel.X, currentPixel.Y);
      var northPixel = bitmap.ReadFrameBuffer(currentPixel.X, currentPixel.Y - 1);
      var southPixel = bitmap.ReadFrameBuffer(currentPixel.X, currentPixel.Y + 1);
      var eastPixel = bitmap.ReadFrameBuffer(currentPixel.X + 1, currentPixel.Y);
      var westPixel = bitmap.ReadFrameBuffer(currentPixel.X - 1, currentPixel.Y);
      // compare the pixel colors and if one of them is different 
      if (currentColor != northPixel || currentColor != southPixel || currentColor != eastPixel ||
          currentColor != westPixel)
      {
         border[index + offset[0]] = currentPixel;
      }
   }


   private static void ConvertProvincesToCpu()
   {
      var provinces = new ProvinceGpu[Vars.Provinces.Count];
      Parallel.For(0, Vars.Provinces.Count, i =>
      {
         var province = Vars.Provinces.ElementAt(i).Value;
         provinces[i] = new ProvinceGpu(province.Color.R, province.Color.G, province.Color.B, province.Pixels.Length);
      });

   }

   public static void SortProvinceColors()
   {
      var sw = new Stopwatch();
      sw.Start();
      CopyExistingBitmapToFrameBuffer();
      var borderArray = _device.Allocate1D(new Point[Vars.Map!.Height * Vars.Map.Width]);
      var offset = new[] { 0 };
      for (int j = 0; j < Vars.Provinces.Count; j++)
      {
         var offsetView = _device.Allocate1D(offset);
         var pixels = _device.Allocate1D(Vars.Provinces.ElementAt(j).Value.Pixels);
         var color = _device.Allocate1D(new[] { Vars.Provinces.ElementAt(j).Value.Color.R, Vars.Provinces.ElementAt(j).Value.Color.G, Vars.Provinces.ElementAt(j).Value.Color.B });
         _calculateBordersKernel(pixels.IntExtent - 1, FrameBuffer.Get1DPixelBuffer(), color.View, pixels.View, borderArray.View, offsetView.View);
         _device.Synchronize();
         offset[0] += pixels.IntExtent;
      }
      sw.Stop();
      //Debug.WriteLine($"Converted provinces to cpu in {sw.Elapsed}");
      /*
      sw.Start();
      CopyExistingBitmapToFrameBuffer();
      var pixelColor = _device.Allocate1D<PixelColor>(Vars.Map!.Width * Vars.Map.Height - 1);
      CopyExistingBitmapToFrameBuffer();
      _device.Synchronize();
      _sortColorsKernel(Vars.Map.Width * Vars.Map.Height - 1, FrameBuffer.Get1DPixelBuffer(), pixelColor.View);
      _device.Synchronize();
      Debug.WriteLine($"Sorted colors in {sw.Elapsed}");
      sw.Stop();
      
      //convert memory buffer to array
      var t = new PixelColor[pixelColor.IntExtent];
      pixelColor.CopyToCPU(t);
      var sb = new StringBuilder();

      for (var i = 0; i < t.Length; i++)
      {
         sb.Append($"{t[i].r,3} - {t[i].g,3} - {t[i].b,3} - {t[i].point.X,5} | {t[i].point.Y,5}\n");
      }
      File.WriteAllText(@"C:\Users\david\Downloads\GPU_Borders.txt", sb.ToString());
      */
      sw.Restart();
      for (int i = 0; i < 1000; i++)
      {
         //ConvertProvincesToCpu();
      }
      sw.Stop();
      Debug.WriteLine($"ConvertProvincesToCpu {sw.ElapsedMilliseconds / 1000.0}");
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