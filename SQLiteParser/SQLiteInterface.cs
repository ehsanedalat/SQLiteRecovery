﻿using System;
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

        public SQLiteInterface(string path,string dbFileName, string dbCopyFileName)
        {
            path = @"F:\SQLite DBs\MMSSMS\Seyed\";
            parser = new SQLiteParser(path+"mmssms.db", path+"mmssms_c.db");
            //path = @"F:\SQLite DBs\Browser\MyChrome\";
            //parser = new SQLiteParser(path+"History", path+"History_c");
            
        }

        public Dictionary<string, ArrayList> unAllocatedSpases2File()
        {
            Dictionary<string, ArrayList> result = parser.UnAllocatedSpacesParser();
            using (BinaryWriter writer = new BinaryWriter(File.Open(path+"result.txt", FileMode.Create)))
            {

                foreach (string tableInfo in result.Keys)
                {
                    writer.Write("Table #: " + tableInfo + "\r\n");
                    foreach (string[] item in (ArrayList)result[tableInfo])
                        writer.Write("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2] + "\r\n");
                }
            }
            return result;
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
