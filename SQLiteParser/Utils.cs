using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections;

namespace SQLiteParser
{
    class Utils
    {
        public static byte[] ReadingFromFile(string fileName, int offset, int length)
        {
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    if (offset != reader.BaseStream.Length)
                    {
                        reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                        return reader.ReadBytes(length);

                    }
                    reader.Close();
                }
            }
            return null;
        }

        public static int getRootPageNumber(string tableName, string dbPath)
        {


            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = dbPath;
            connBuilder.Version = 3;
            using (SQLiteConnection connection = new SQLiteConnection(connBuilder.ToString()))
            {
                connection.Open();
                SQLiteCommand com = new SQLiteCommand("select rootpage from sqlite_master where type='table' and tbl_name='" + tableName + "';", connection);
                int result = Convert.ToInt32(com.ExecuteScalar());
                connection.Close();
                connection.Dispose();
                
                return result;
            }
        }
        public static ArrayList getAllTableInfo(string dbPath)
        {


            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = dbPath;
            connBuilder.Version = 3;
            using (SQLiteConnection connection = new SQLiteConnection(connBuilder.ToString()))
            {
                connection.Open();
                SQLiteCommand com = new SQLiteCommand("select tbl_name,rootpage from sqlite_master where type='table';", connection);
                SQLiteDataReader reader = com.ExecuteReader();
                ArrayList result = new ArrayList();
                while (reader.Read())
                {
                    result.Add(new string[] { (string)reader["tbl_name"], Convert.ToString(reader["rootpage"]) });
                }
                connection.Close();
                connection.Dispose();

                return result;
            }
        }
    }
}
