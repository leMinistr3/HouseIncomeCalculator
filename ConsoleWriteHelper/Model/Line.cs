using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWriteHelper.Model
{
    public class Line
    {
        private List<Property> _properties;
        private int _length;
        public Line(List<Property> properties) 
        { 
            _properties = properties;
        }

        public override string ToString() 
        {
            return "";
        }
    }
}
