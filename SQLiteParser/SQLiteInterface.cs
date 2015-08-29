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
        private JournalFileParser journalParser;
        private string path;

        public SQLiteInterface()
        {
            //path = @"F:\SQLite DBs\MMSSMS\Seyed\";
            //parser = new SQLiteParser(path+"mmssms.db", path+"mmssms_c.db");
            path = @"F:\SQLite DBs\Browser\MyChrome\";
            parser = new SQLiteParser(path+"History", path+"History_c");
            
        }

        public void unAllocatedSpases2File()
        {
            ArrayList result = parser.UnAllocatedSpacesParser();
            using (BinaryWriter writer = new BinaryWriter(File.Open(path+"result.txt", FileMode.Create)))
            {
                foreach (string[] item in result)
                    writer.Write("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2] + "\r\n");
            }
        }

        public void readSMS()
        {
            ArrayList result = parser.readSMSs();
            string value = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path+"result.txt", FileMode.Create)))
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

        public void freeListRetrival()
        {
            ArrayList result = parser.FreeListPagesParser(); 
            string value = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path+"result.txt", FileMode.Create)))
            {


                foreach (Dictionary<int, string> row in result)
                {
                    value = "ROW -> | ";
                    for (int i = 0; row.ContainsKey(i); i++)
                    {
                        value = value + "col: " + i + " -> " + row[i] + " | ";
                    }
                    writer.Write(value + "\r\n");

                }
            }
        }
    }
}
