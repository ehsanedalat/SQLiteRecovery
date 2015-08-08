using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APA.XmlParser
{
    class UndefinedSizeException : Exception
    {
        private string message
        {
            get;
            set;
        }

        public UndefinedSizeException(string p)
        {
            // TODO: Complete member initialization
            this.message = p;
        }
    }
}
