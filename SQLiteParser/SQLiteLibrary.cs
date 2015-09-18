using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Data;

namespace SQLiteParser
{
    public class SQLiteLibrary
    {
        private SQLiteParser parser;
        private JournalFileParser journalParser;
        private string workSpacePath;
        private string dbFilePath;
        private string dbCopyFilePath;
        public string sqliteName { get; set; }

        public SQLiteLibrary(string workSpacePath,string dbFilePath,string name)
        {
            if (File.Exists(dbFilePath) && Utils.fileSize(dbFilePath) > 0)
            {
                this.workSpacePath = workSpacePath;
                this.dbFilePath = dbFilePath;
                this.sqliteName = name;
                parser = new SQLiteParser(dbFilePath);
                this.dbCopyFilePath = parser.dbCopyFilePath;
            }
            
        }
        /// <summary>
        /// recover deleted or updated records from journal file.
        /// </summary>
        /// <param name="journalFilePath">file Address of journal file.</param>
        /// <returns>Dictionary witch its key is table name and value is ArrayList of records. </returns>
        public Dictionary<string, ArrayList> journalRecovery(string journalFilePath)
        {
            if (File.Exists(journalFilePath) && Utils.fileSize(journalFilePath) > 0)
            {
                journalParser = new JournalFileParser(journalFilePath, dbFilePath, workSpacePath);
                return journalParser.getDeletedRecords();
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// find deleted records from unallocated space of db.
        /// </summary>
        /// <returns>Dictinary with table name key and ArrayList of retrieved records as value. each item in ArrayList is string array with length of 3. first item is page number, second one is type(unallocated or freeblock) and third one is data.</returns>
        public Dictionary<string, ArrayList> unAllocatedSpases()
        {
            if (Utils.fileSize(dbFilePath) > 0)
            {
                Dictionary<string, ArrayList> result = parser.UnAllocatedSpacesParser();
                /*using (StreamWriter writer = new StreamWriter(File.Open(workSpacePath + "result.txt", FileMode.Create)))
                {

                    foreach (string tableInfo in result.Keys)
                    {
                        writer.WriteLine("Table #: " + tableInfo);
                        foreach (string[] item in (ArrayList)result[tableInfo])
                            writer.WriteLine("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2]);
                    }
                }*/
                return result;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// get All records from given table. filter string limit output records.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="filter"></param>
        public DataTable getAllTableRecords(string tableName, string filter)
        {
            if (Utils.fileSize(dbFilePath) > 0)
            {
                return Utils.getAllTableRecords(dbCopyFilePath, tableName, filter);
            }
            else
                return null;
        }

        public ArrayList getAllTableNames()
        {
            if (Utils.fileSize(dbFilePath) > 0)
            {
                return Utils.getAllTableNames(dbCopyFilePath);
            }
            else return null;
        }

        internal void readSMS()
        {
            ArrayList result = parser.readSMSs();
            string value = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(workSpacePath+"result.txt", FileMode.Create)))
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

        internal void freeListRetrival()
        {
            ArrayList result = parser.FreeListPagesParser(); 
            string value = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(workSpacePath+"result.txt", FileMode.Create)))
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
