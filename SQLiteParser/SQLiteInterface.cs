using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace SQLiteParser
{
    class SQLiteInterface
    {
        public SQLiteInterface()
        {
            SQLiteParser parser = new SQLiteParser(@"F:\SQLite DBs\MMSSMS\mmssms.db", @"F:\SQLite DBs\MMSSMS\mmssms_c.db");
            ArrayList result=parser.UnAllocatedSpacesParser();
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"F:\SQLite DBs\MMSSMS\result.txt", FileMode.Create)))
            {
                foreach (string[] item in result)
                    writer.Write("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2] + "\r\n");
            }
        }
    }
}
