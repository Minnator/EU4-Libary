using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU4_Parse_Lib.DataClasses
{
    public class Province
    {

        public int ID;
        
        public Color Color;
        public Color CurrentColor;

        public List<Point> Pixels = new ();
        public List<Point> Border = new ();

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

        public override bool Equals(object? obj)
        {
            return obj is Province province &&
                   ID == province.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }
    }
}
