using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4_Parse_Lib.DataClasses
{
    public class Province
    {

        public int Id;
        
        public Color Color;
        public Color CurrentColor;

        public List<Point> Pixels = new ();
        public List<Point> Border = new ();

        public string Area = "-1";
        public string Name = "-1";

        public Province (Color col) 
        { 
            Color = col;
            CurrentColor = Color;
        }

        public void AddToPixels(IEnumerable<Point> p)
        { 
            Pixels.AddRange(p);
        }

        public void AddToBorder(IEnumerable<Point> p)
        {
            Border.AddRange(p);
        }

        public override string ToString()
        {
            return $"ID: {Id,4}; area: {Area}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Province province &&
                   Id == province.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
