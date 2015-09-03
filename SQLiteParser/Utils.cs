﻿using System;
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
        private static SQLiteConnection connection;
        private static string sqldiffToolName = "sqldiff.exe";
        private static string ToolFolder = @"..\Tools";

        internal static void buildDBConnection(string dbPath)
        {
            SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            connBuilder.DataSource = dbPath;
            connBuilder.Version = 3;
            connection = new SQLiteConnection(connBuilder.ToString());
            connection.Open();
            
        }

        internal static void closeDBConnection()
        {
            connection.Close();
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

        internal static int getRootPageNumber(string tableName)
        {
            if (connection != null)
            {
                SQLiteCommand com = new SQLiteCommand("select rootpage from sqlite_master where type='table' and tbl_name='" + tableName + "';", connection);
                int result = Convert.ToInt32(com.ExecuteScalar());
                connection.Close();
                connection.Dispose();
                return result;
            }
            else
            {
                throw new EntryPointNotFoundException("Connection should be stablished!!");
            }
            
        }


        internal static ArrayList getAllTablesInfo()
        {
            if (connection != null)
            {
                SQLiteCommand com = new SQLiteCommand("select tbl_name,rootpage from sqlite_master where type='table';", connection);
                SQLiteDataReader reader = com.ExecuteReader();
                ArrayList result = new ArrayList();
                while (reader.Read())
                {
                    result.Add(new string[] { (string)reader["tbl_name"], Convert.ToString(reader["rootpage"]) });
                }
                return result;
            }
            else
            {
                throw new EntryPointNotFoundException("Connection should be stablished!!");
            }
        }

            /*foreach (string[] item in tableList)
            {
                var cmd = new SQLiteCommand("select * from " + item[0], connection);
                var dr = cmd.ExecuteReader();
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    Console.WriteLine(dr.GetName(i));
                }

            }*/
        

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
            index++;
            return index;
        }

        internal static void getDataBaseDifferences(string rolledBackDB, string currentDB,ref Dictionary<string,ArrayList> result)
        {
            ArrayList tableInfo=getAllTablesInfo();

            Process process = buildConnection2sqldiff();
            foreach (string[] item in tableInfo)
            {
            
                if (!result.ContainsKey(item[0]))
                {
                    result.Add(item[0], outputQueries(runCommand2cmd("--table " + item[0] + " \"" + rolledBackDB + "\" \"" + currentDB + "\"", process)));   
                }
                else
                {
                    result[item[0]].Add(outputQueries(runCommand2cmd("--table " + item[0] + " \"" + rolledBackDB + "\" \"" + currentDB+"\"", process)));
                }
                
            }
            process.Close();

        }

        private static Process runCommand2cmd(String command, Process process)
        {
            

            //process.StartInfo.Arguments = path.ElementAt(0)+": &";
            //process.StartInfo.Arguments += " cd "+path.Substring(3)+" &";
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
                    if (!query.ToLower().Contains("INSERT".ToLower()))
                        finalList.Add(query + ";");
                }
            }

            return finalList;
        }

        private static string getErrors(Process process)
        {
            return process.StandardError.ReadToEnd();
        }
    }
}
