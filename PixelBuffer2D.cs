using ILGPU;
using ILGPU.Runtime;

namespace EU4_Parse_Lib;

public class PixelBuffer2D<T>
   : IDisposable
   where T : unmanaged
{
   public int width;
   public int height;

   private T[] _data;
   private readonly dPixelBuffer2D<T> _frameBuffer;
   private readonly MemoryBuffer1D<T, Stride1D.Dense> _memoryBuffer;

   public PixelBuffer2D(Accelerator device, int height, int width)
   {
      this.width = width;
      this.height = height;

      _data = new T[width * height * 3];
      _memoryBuffer = device.Allocate1D<T>(width * height * 3);
      _frameBuffer = new dPixelBuffer2D<T>(width, height, _memoryBuffer);
   }

   public ref T[] GetRawFrameData()
   {
      _memoryBuffer.CopyToCPU(_data);
      return ref _data;
   }

   public void SetRawFrameData(T[] data)
   {
      _data = data;
   }

   public dPixelBuffer2D<T> Get1DPixelBuffer()
   {
      _memoryBuffer.CopyFromCPU(_data);
      return _frameBuffer;
   }

   public T this[int index]
   {
      get
      {
         _memoryBuffer.CopyToCPU(_data);
         return _data[index];
      }

      set
      {
         _data[index] = value;
      }
   }

   public void Dispose()
   {
      _memoryBuffer.Dispose();
   }
}

public struct dPixelBuffer2D<T> where T : unmanaged
{
   public int width;
   public int height;
   public ArrayView1D<T, Stride1D.Dense> Frame;

   public dPixelBuffer2D(int width, int height, MemoryBuffer1D<T, Stride1D.Dense> frame)
   {
      this.width = width;
      this.height = height;
      Frame = frame.View;
   }

   public (int x, int y) GetPosFromIndex(int index)
   {
      var x = index % width;
      var y = index / width;

      return (x, y);
   }

   public int GetIndexFromPos(int x, int y)
   {
      return ((y * width) + x);
   }   
   
   public int GetIndexFromPos(Point p)
   {
      return ((p.Y * width) + p.X);
   }

   public (T r, T g, T b) ReadFrameBuffer(int index)
   {
      var subPixel = index * 3;
      return (Frame[subPixel], Frame[subPixel + 1], Frame[subPixel + 2]);
   }

   public (T x, T y, T z) ReadFrameBuffer(int x, int y)
   {
      var subPixel = GetIndexFromPos(x, y) * 3;
      return (Frame[subPixel], Frame[subPixel + 1], Frame[subPixel + 2]);
   }

   public void WriteFrameBuffer(int index, T r, T g, T b)
   {
      var subPixel = index * 3;
      Frame[subPixel] = r;
      Frame[subPixel + 1] = g;
      Frame[subPixel + 2] = b;
   }


   public void WriteFrameBuffer(int x, int y, T r, T g, T b)
   {
      var subPixel = GetIndexFromPos(x, y) * 3;
      Frame[subPixel] = r;
      Frame[subPixel + 1] = g;
      Frame[subPixel + 2] = b;
   }
}