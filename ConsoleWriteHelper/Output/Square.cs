using ConsoleWriteHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWriteHelper.Output
{
    public class Square
    {
        private char _seperator;
        private string _title;

        private Section? _header;
        private Section? _body;
        private Section? _footer;

        public Square(string title, char seperator = '*') 
        { 
            _title = title;
            _seperator = seperator;
        }

        public void SetHeader(Section section)
        {
            _header = section;
        }
        public void SetBody(Section section)
        {
            _header = section;
        }
        public void SetFooter(Section section)
        {
            _header = section;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
