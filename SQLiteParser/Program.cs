using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SQLiteParser
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteParser parser = new SQLiteParser(@"F:\SQLite DBs\MMSSMS\mmssms.db", @"F:\SQLite DBs\MMSSMS\mmssms_c.db",true,@"F:\SQLite DBs\MMSSMS\result.txt");
           
        }
    }
}
