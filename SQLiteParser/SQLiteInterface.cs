using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace SQLiteParser
{
    class SQLiteInterface
    {
        private SQLiteParser parser;
        public SQLiteInterface()
        {
            parser = new SQLiteParser(@"F:\SQLite DBs\MMSSMS\Seyed\mmssms.db", @"F:\SQLite DBs\MMSSMS\Seyed\mmssms_c.db");
            
        }

        public void unAllocatedSpases2File()
        {
            ArrayList result = parser.UnAllocatedSpacesParser();
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"F:\SQLite DBs\MMSSMS\Seyed\result.txt", FileMode.Create)))
            {
                foreach (string[] item in result)
                    writer.Write("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2] + "\r\n");
            }
        }

        public void readSMS()
        {
            ArrayList result = parser.readSMSs();
            string value = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(@"F:\SQLite DBs\MMSSMS\Seyed\result.txt", FileMode.Create)))
            {
                
                
                foreach (Dictionary<int, string> row in result)
                {
                    value = "ROW -> | ";
                    for (int i = 0; row.ContainsKey(i); i++)
                    {
                        value = value + "col: " + i + " -> " + row[i] + " | ";
                    }
                    writer.Write(value+ "\r\n");

                }
            }
            
        }
    }
}
