using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APA.XmlParser
{
    class UncompatibleSizeException : Exception
    {
        public String message
        {
            get;
            set;
        }
        public UncompatibleSizeException(String message)
        {
            this.message = message;
        }
    }
}
