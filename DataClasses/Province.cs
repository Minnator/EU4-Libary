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
        
        public Color color;
        public Color currentColor;
        //Convert to Hashmaps
        public List<Point> pixels = new ();
        public List<Point> border = new ();

        public Province (Color col) 
        { 
            color = col;
            currentColor = color;
        }

        public void AddToPixels(IEnumerable<Point> p)
        { 
            pixels.AddRange(p);
        }

        public void AddToBorder(IEnumerable<Point> p)
        {
            border.AddRange(p);
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
