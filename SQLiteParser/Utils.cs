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
        internal static byte[] ReadingFromFile(string fileName, int offset, int length)
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

        internal static int getRootPageNumber(string tableName, string dbPath)
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
        internal static ArrayList getAllTableInfo(string dbPath)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="varIntArray">Array with the length of 8(1-9)</param>
        /// <param name="value">call by refrence of return value, primary value is zero</param>
        /// <returns>index of finished varInt type</returns>
        internal static int vaiInt2Int(byte[] varIntArray, ref long value)
        {
            long[] result=new long[9];
            int index = 0;

            for (int i = 0; i < varIntArray.Length;i++ )
            {
                if (i < varIntArray.Length - 1)
                {
                    if ((varIntArray[i] & 0x80) != 0)// x and 1000 0000
                    {
                        result[i] = Convert.ToInt64((varIntArray[i] & 0x7F));// x and 0111 1111
                    }
                    else
                    {
                        result[i] = Convert.ToInt64(varIntArray[i]);
                        index = i;
                        break;
                    }
                }
                else
                {
                    result[i] = Convert.ToInt64(varIntArray[i]);
                    index = i;
                    break;
                }
            }
            if (index != 8)
                for (int i = 0; i <= index; i++)
                {
                    long x = ((long)result[index - i] << ((i) * 8 - i));
                    value = value | x;
                    //Console.WriteLine("index-i->: " + (index - i) + " | x:-> " + Convert.ToString(x, 2) + " | value:-> " + Convert.ToString(value, 2) + "value length:->" + Convert.ToString(value, 2).Length);
                }
            else
            {
                for (int i = 0; i <= index; i++)
                {
                    long x = 0;
                    if(i!=0)
                        x = ((long)result[index - i] << ((i) * 8 - (i - 1)));
                    else
                        x = (long)result[index - i];
                    value = value | x;
                    //Console.WriteLine("index-i->: " + (index - i) + " | x:-> " + Convert.ToString(x, 2) + " | value:-> " + Convert.ToString(value, 2) + "value length:->" + Convert.ToString(value, 2).Length);
                }
            }
            return index;
        }
    }
}
