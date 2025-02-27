using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWriteHelper.Model
{
    public class Property
    {
        private char _seperator { get; set; }

        public int Padding { get; set; }
        public required string Label { get; set; }
        public required string Value { get; set; }

        public Property(char seperator = ':') 
        { 
            _seperator = seperator;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
