using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWriteHelper.Model
{
    public class Section
    {
        private List<Line> _lines;

        public Section(List<Line> lines) 
        { 
            _lines = lines;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
