using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Collections;
using System.Diagnostics;

namespace SQLiteParser
{
    class Utils
    {
        private static string sqldiffToolName = "sqldiff.exe";
        private static string ToolFolder = @"..\Tools";

        internal static SQLiteConnection buildDBConnection(string dbPath)
        {
            SQLiteConnection connection;
            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = dbPath;
            connBuilder.Version = 3;
            connection = new SQLiteConnection(connBuilder.ToString());
            connection.Open();

            return connection;
        }

        internal static void copyFile(string destFilePath, string srcFilePath)
        {
            if (File.Exists(destFilePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(destFilePath, FileMode.Open)))
                {
                    long offset = 0;
                    Stream outStream = File.Open(srcFilePath, FileMode.Create);
                    while (offset != reader.BaseStream.Length)
                    {
                        
                         outStream.Write(reader.ReadBytes(512), 0, 512);
                         offset = offset + 512;
                         //reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                         //outStream.Seek(offset, SeekOrigin.Current);

                         

                    }
                    outStream.Flush();
                    outStream.Close();
                    
                    reader.Close();
                }
            }
        }

        internal static byte[] ReadingFromFile(string fileName, long offset, int length)
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
            else
            {
                throw new FileNotFoundException("There is not such a file in defined path.", fileName);
            }
            return null;
        }

        internal static long fileSize(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    long size = reader.BaseStream.Length;
                    reader.Close();
                    return size;
                }
            }
            else
            {
                throw new FileNotFoundException("There is not such a file in defined path.", fileName);
            }

        }

        internal static int getRootPageNumber(string tableName, string dbName)
        {
            SQLiteConnection connection = buildDBConnection(dbName);
            SQLiteCommand com = new SQLiteCommand("select rootpage from sqlite_master where type='table' and tbl_name='" + tableName + "';", connection);
            int result = Convert.ToInt32(com.ExecuteScalar());
            connection.Close();
            connection.Dispose();

            return result;
        }

        /// <summary>
        /// get name and root page number of all tables in input db.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns>ArrayList of string array with 2 item. first one is table name and second one is root page number.</returns>
        internal static ArrayList getAllTablesInfo(string dbName)
        {
            SQLiteConnection connection = buildDBConnection(dbName);

            SQLiteCommand com = new SQLiteCommand("select tbl_name,rootpage from sqlite_master where type='table';", connection);
            SQLiteDataReader reader = com.ExecuteReader();
            ArrayList result = new ArrayList();
            while (reader.Read())
            {
                result.Add(new string[] { (string)reader["tbl_name"], Convert.ToString(reader["rootpage"]) });
            }
            connection.Close();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="varIntArray">Array with the length of 8(1-9)</param>
        /// <param name="value">call by refrence of return value, primary value is zero</param>
        /// <returns>index of finished varInt type</returns>
        internal static int varInt2Int(byte[] varIntArray, ref long value)
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
                    Debug.WriteLine("index-i->: " + (index - i) + " | x:-> " + Convert.ToString(x, 2) + " | value:-> " + Convert.ToString(value, 2) + "value length:->" + Convert.ToString(value, 2).Length);
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
                    Debug.WriteLine("index-i->: " + (index - i) + " | x:-> " + Convert.ToString(x, 2) + " | value:-> " + Convert.ToString(value, 2) + "value length:->" + Convert.ToString(value, 2).Length);
                }
            }
            index++;
            return index;
        }
        /// <summary>
        /// with sqldiff.exe tool find the deleted or updated records.
        /// </summary>
        /// <param name="rolledBackDB">old db</param>
        /// <param name="currentDB"></param>
        /// <param name="result">result contains queries which can get deleted or updated records from old db.</param>
        internal static void getDataBaseDifferences(string rolledBackDB, string currentDB,ref Dictionary<string,Dictionary<string, ArrayList>> result)
        {
            ArrayList tableInfo=getAllTablesInfo(rolledBackDB);

            Process process = buildConnection2sqldiff();
            foreach (string[] item in tableInfo)
            {

                if (!result.ContainsKey(item[0]))
                {
                    ArrayList list = outputQueries(runCommand2cmd("--table " + item[0] + " \"" + rolledBackDB + "\" \"" + currentDB + "\"", process));
                    if (list.Count != 0)
                    {
                        Dictionary<string, ArrayList> res = new Dictionary<string, ArrayList>();
                        res.Add(rolledBackDB, list);
                        result.Add(item[0], res);
                    }
                }
                else
                {
                    Dictionary<string, ArrayList> list = result[item[0]];
                    ArrayList newList = outputQueries(runCommand2cmd("--table " + item[0] + " \"" + rolledBackDB + "\" \"" + currentDB + "\"", process));
                    foreach(string key in list.Keys)
                        foreach (string query in newList)
                        {
                            if (list[key].Contains(query))
                                newList.Remove(query);
                        }
                    if (newList.Count != 0)
                    {
                        result[item[0]].Add(rolledBackDB,newList);
                    }
                }
                
            }
            process.Close();

        }

        private static Process runCommand2cmd(String command, Process process)
        {
            process.StartInfo.Arguments = command;

            process.Start();
            return process;
        }

        private static Process buildConnection2sqldiff()
        {
            string path = ToolFolder;
            path = Path.GetFullPath(path);
            Process process = new Process();
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName =path+@"\sqldiff.exe";
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            return process;
        }

        private static ArrayList outputQueries(Process process)
        {
            StreamReader outputWriter = process.StandardOutput;
            String responce = outputWriter.ReadToEnd();
            
            ArrayList finalList = new ArrayList();

            Debug.WriteLine(getErrors(process));
            if (!String.IsNullOrEmpty(responce))
            {
                string[] queries = responce.Split(new string[] { ";\r\n" },StringSplitOptions.RemoveEmptyEntries);

                foreach (string query in queries)
                {
                    if (!query.ToLower().Contains("INSERT INTO".ToLower()))
                    {
                        string res = query;
                        if (query.ToLower().Contains("DELETE FROM".ToLower()))
                        {
                            res = query.Replace("DELETE","SELECT * ");
                        }
                        else if (query.ToLower().Contains("UPDATE".ToLower()))
                        {
                            res = query.Replace("UPDATE","SELECT * FROM");
                            int f=res.IndexOf("SET");
                            int l=res.IndexOf("WHERE");
                            res=res.Remove(f,l-f);
                        }
                        finalList.Add(res + ";");
                    }
                }
            }

            return finalList;
        }

        private static string getErrors(Process process)
        {
            return process.StandardError.ReadToEnd();
        }

        internal static DataTable getAllTableRecords(string dbName, string tableName, string filter)
        {
            SQLiteConnection connection = buildDBConnection(dbName);

            var cmd = new SQLiteCommand("select * from " + tableName, connection);
            var dr = cmd.ExecuteReader();
            ArrayList colNames = new ArrayList();
            for (var i = 0; i < dr.FieldCount; i++)
            {
                colNames.Add(dr.GetName(i));
            }
            string query = "SELECT * FROM "+tableName+";";

            if (!String.IsNullOrEmpty(filter))
            {
                query = "SELECT * FROM " + tableName + " WHERE ";

                foreach (string col in colNames)
                {
                    query = query + col + " LIKE '%" + filter + "%' OR ";
                }

                query = query + " 0 ;";
            }
            SQLiteCommand com = new SQLiteCommand(query, connection);
            SQLiteDataAdapter da = new SQLiteDataAdapter(com);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);
            return dataSet.Tables[0];
            /*SQLiteDataReader reader = com.ExecuteReader();

            Console.WriteLine("sms");
            foreach (string col in colNames)
            {
                Debug.Write(col + " | ");
            }
            Debug.WriteLine("\r\n");
            while (reader.Read())
            {
                foreach (string col in colNames)
                {
                    Debug.Write(reader[col] + " | ");
                }
                Debug.WriteLine("\r\n");
            }
            connection.Close();

            return null;*/
        }
    }
}
