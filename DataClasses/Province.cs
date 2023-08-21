using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.MonthCalendar;

namespace EU4_Parse_Lib.DataClasses
{
    public class Province
    {

        public int Id { get; set; }
        
        public Color Color;
        public Color CurrentColor;

        public List<Point> Pixels = new ();
        public List<Point> Border = new ();

        public string Area { get; set; } = "-1";
        public string Name { get; set; } = "-1";

        public readonly Dictionary<Attribute, Func<Province, object>> Attributes = new ()
        {
            { Attribute.Id, province => province.Id },
            { Attribute.Area, province => province.Area },
            { Attribute.Name, province => province.Name },
        };

        public object GetAttribute(Attribute att)
        {
            return Attributes[att](this);
        }
        
        public Province (Color col) 
        { 
            Color = col;
            CurrentColor = Color;
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
