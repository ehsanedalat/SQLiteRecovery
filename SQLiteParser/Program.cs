using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteParser
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Write(Utils.getPageSize(@"F:\SQLite DBs\MMSSMS\mmssms.db"));
            System.Console.ReadLine();
        }
    }
}
