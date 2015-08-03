using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MySQLLibrary
{
    class MySQLException : Exception
    {
        public MySQLException()
        : base() { }
    
    public MySQLException(string message)
        : base(message) { }
    
    public MySQLException(string format, params object[] args)
        : base(string.Format(format, args)) { }
    
    public MySQLException(string message, Exception innerException)
        : base(message, innerException) { }
    
    public MySQLException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }

    protected MySQLException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
    }
}
