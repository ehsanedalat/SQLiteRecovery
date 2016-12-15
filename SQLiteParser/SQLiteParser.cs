using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data.SQLite;

namespace SQLiteParser
{
    class SQLiteParser
    {
        private const int internalNodeTypeValue = 5;
        private const int leafNodeTypeValue = 13;
        private int pageSize;
#pragma warning disable CS0169 // The field 'SQLiteParser.rootPageNum' is never used
        private int rootPageNum;
#pragma warning restore CS0169 // The field 'SQLiteParser.rootPageNum' is never used
        private const int leafPageHeaderLength = 8;
        private const int internalPageHeaderLength = 12;
        private const int pageSizeOffsetValue = 16;
        private const int pageSizeLengthValue = 2;
        private const string headerString="SQLite format 3\0";
        private byte[] currentPage;
        private byte[] headerBytesOfSQLiteFile;
        private const int dbHeaderSize = 100;
        private string dbFilePath;
        private const int internalPageCellsPionerLength = 4;
        private ArrayList tableInfo;
        private ArrayList UnallocatedSpaceDeletedRecords { get; set; }
        private Dictionary<int, ArrayList> sqliteTypes;

        private ArrayList records = new ArrayList();//TODO change dataset!
        internal string dbCopyFilePath;

        internal SQLiteParser(string dbFilePath)
        {
            this.dbFilePath = dbFilePath;
            this.UnallocatedSpaceDeletedRecords = new ArrayList();

            init();

        }

        private void init()
        {
            sqliteTypes = new Dictionary<int, ArrayList>();
            sqliteTypes.Add(0, new ArrayList() { "NULL", "0" });
            sqliteTypes.Add(1, new ArrayList() { "INTEGER_1", "1" });
            sqliteTypes.Add(2, new ArrayList() { "INTEGER_2", "2" });
            sqliteTypes.Add(3, new ArrayList() { "INTEGER_3", "3" });
            sqliteTypes.Add(4, new ArrayList() { "INTEGER_4", "4" });
            sqliteTypes.Add(5, new ArrayList() { "INTEGER_6", "6" });
            sqliteTypes.Add(6, new ArrayList() { "INTEGER_8", "8" });
            sqliteTypes.Add(7, new ArrayList() { "FLOAT", "8" });
            sqliteTypes.Add(8, new ArrayList() { "INTEGER_0", "0" });
            sqliteTypes.Add(9, new ArrayList() { "INTEGER_0", "0" });
            sqliteTypes.Add(12, new ArrayList() { "BLOB", "*" });
            sqliteTypes.Add(13, new ArrayList() { "STRING", "*" });

            headerBytesOfSQLiteFile = Utils.ReadingFromFile(dbFilePath, 0, dbHeaderSize);
            byte[] currentHeaderString = new byte[16];
            Array.Copy(headerBytesOfSQLiteFile, currentHeaderString, 16);
            string cur = System.Text.Encoding.ASCII.GetString(currentHeaderString);

            if (headerString.Equals(cur))
            {
                byte[] result = Utils.ReadingFromFile(dbFilePath, pageSizeOffsetValue, pageSizeLengthValue);
                Array.Reverse(result);
                pageSize = BitConverter.ToInt16(result, 0);

                dbCopyFilePath = dbFilePath + "_c";
                
                System.IO.File.Copy(dbFilePath, dbCopyFilePath,true);
                
                tableInfo = Utils.getAllTablesInfo(dbCopyFilePath);
            }
            else
            {
                throw new FileNotFoundException("Type of given file is not SQLite!!", dbFilePath);
            }

            
            
        }

        public ArrayList readSMSs()
        {
            records.Clear();
            BTreeTraversal(12);//18//10
            return records;
        }


        private void BTreeTraversal(int currentPageNum)// TODO test for page 0 it has 100 bytes of header
        {
            int currentPageOffset = 0;
            if (currentPageNum != 0)
                currentPageOffset = (currentPageNum - 1) * pageSize;
            currentPage = Utils.ReadingFromFile(dbFilePath, currentPageOffset, pageSize);
            int numOfCells = BitConverter.ToInt16(new byte[] { currentPage[4], currentPage[3] }, 0);
            int cellsOffset = BitConverter.ToInt16(new byte[] { currentPage[6], currentPage[5] }, 0);

            if (currentPage[0] == leafNodeTypeValue)
            {
                getDeletedRecordsFromUnallocatedSpaceAndFreeBlocks(numOfCells, cellsOffset, currentPageNum);

                //getRecordsFromLeafPagesInTableBTree(currentPageNum);

                return;
            }
            else if (currentPage[0] == internalNodeTypeValue)
            {
                int[] childPtr = getChildsPtr(numOfCells, ref cellsOffset);
                getDeletedRecordsFromUnallocatedSpaceAndFreeBlocks(numOfCells, cellsOffset, currentPageNum);

                foreach (int ptr in childPtr)
                {
                    BTreeTraversal(ptr);
                }
            }
        }

        private int[] getChildsPtr(int numOfCells, ref int cellsOffset)
        {
            int[] childPtr = new int[numOfCells + 1];
            int offset = 12;
            int pointer = cellsOffset;
            for (int i = 0; i < childPtr.Length - 1; i++)
            {
                cellsOffset = BitConverter.ToInt16(new byte[] { currentPage[offset + 1], currentPage[offset] }, 0);
                offset = offset + 2;
                childPtr[i] = BitConverter.ToInt32(new byte[] { currentPage[cellsOffset + 3], currentPage[cellsOffset + 2], currentPage[cellsOffset + 1], currentPage[cellsOffset] }, 0);
            }
            childPtr[childPtr.Length - 1] = BitConverter.ToInt32(new byte[] { currentPage[11], currentPage[10], currentPage[9], currentPage[8] }, 0);
            return childPtr;
        }

        private void getDeletedRecordsFromUnallocatedSpaceAndFreeBlocks(int numOfCells, int cellsOffset, int pageNum)
        {
            getAllFreeBlockListData(BitConverter.ToInt16(new byte[] { currentPage[2], currentPage[1] }, 0), pageNum);
            getDataFromUnallocatedSpace(numOfCells * 2 + leafPageHeaderLength, cellsOffset, pageNum);
        }

        private void getAllFreeBlockListData(int currentFreeBlockOffset, int pageNum)
        {
            int currentFreeBlockSize = BitConverter.ToInt16(new byte[] { currentPage[currentFreeBlockOffset + 3], currentPage[currentFreeBlockOffset + 2] }, 0);
            int nextFreeBlockOffset = BitConverter.ToInt16(new byte[] { currentPage[currentFreeBlockOffset + 1], currentPage[currentFreeBlockOffset] }, 0);

            if (currentFreeBlockOffset == 0)
            {
                nextFreeBlockOffset = BitConverter.ToInt16(new byte[] { currentPage[2], currentPage[1] }, 0);
                currentFreeBlockSize = 0;
                if (nextFreeBlockOffset == 0)
                    return;
            }

            if (nextFreeBlockOffset != 0)
            {
                getDataFromFreeBlock(currentFreeBlockOffset, currentFreeBlockSize, pageNum);

                getAllFreeBlockListData(nextFreeBlockOffset, pageNum);
            }
            else if (nextFreeBlockOffset == 0)
            {
                getDataFromFreeBlock(currentFreeBlockOffset, currentFreeBlockSize, pageNum);
                return;
            }
        }

        private void getDataFromUnallocatedSpace(int unallocatedSpaceOffset, int cellsOffset, int pageNum)
        {
            string data = Encoding.UTF8.GetString(currentPage, unallocatedSpaceOffset, cellsOffset - unallocatedSpaceOffset);

            data = data.Replace("\0", "");
            foreach (char c in data)
            {
                if (!((c >= 0x0600 && c <= 0x06FF) || (c >= 0x0021 && c <= 0x007E) || c == 0xFB8A || c == 0x067E || c == 0x0686 || c == 0x06AF))
                {
                    data = data.Replace(c, ' ');

                }

            }
            data = data.Replace("  ", string.Empty);

            if (!String.IsNullOrEmpty(data))
                UnallocatedSpaceDeletedRecords.Add(new string[] { pageNum + "", "UNALLOCATED", data });
        }

        private void getDataFromFreeBlock(int currentFreeBlockOffset, int currentFreeBlockSize, int pageNum)
        {
            string data = Encoding.UTF8.GetString(currentPage, currentFreeBlockOffset + 4, currentFreeBlockSize - 4);
            data = data.Replace("\0", "");
            foreach (char c in data)
            {
                if (!((c >= 0x0600 && c <= 0x06FF) || (c >= 0x0021 && c <= 0x007E) || c == 0xFB8A || c == 0x067E || c == 0x0686 || c == 0x06AF))
                {
                    data = data.Replace(c, ' ');

                }

            }
            data = data.Replace("  ", string.Empty);

            if (!String.IsNullOrEmpty(data))
                UnallocatedSpaceDeletedRecords.Add(new string[] { pageNum + "", "FREE BLOCK", data });
        }


        internal Dictionary<string,ArrayList> UnAllocatedSpacesParser()
        {
            Dictionary<string, ArrayList> res = new Dictionary<string, ArrayList>();
            
            foreach (string[] item in tableInfo)
            {
                BTreeTraversal(Convert.ToInt32(item[1]));
                if(UnallocatedSpaceDeletedRecords.Count>0)
                    res.Add(item[0],(ArrayList)UnallocatedSpaceDeletedRecords.Clone());
                UnallocatedSpaceDeletedRecords.Clear();
                
            }
           
            
            return res;
        }

        internal ArrayList FreeListPagesParser()//TODO Debug this part
        {
            records.Clear();
            int firstTrunkPageNum = BitConverter.ToInt32(new byte[] { headerBytesOfSQLiteFile[35], headerBytesOfSQLiteFile[34], headerBytesOfSQLiteFile[33], headerBytesOfSQLiteFile[32] }, 0);
            if (firstTrunkPageNum != 0)
            {
                getDeletedPagesFromFreeList(firstTrunkPageNum);
            }
            return records;
        }

        private void getDeletedPagesFromFreeList(int trunkPageNum)
        {
            int offset=(trunkPageNum-1)*pageSize;
            currentPage = Utils.ReadingFromFile(dbFilePath, offset, pageSize);
            int nextTrunkPageNum = BitConverter.ToInt32(new byte[] { currentPage[3], currentPage[2], currentPage[1], currentPage[0] }, 0);
            int numOfLeafPagesHear = BitConverter.ToInt32(new byte[] { currentPage[7], currentPage[6], currentPage[5], currentPage[4] }, 0);
            offset = 8;
            int [] freepages=new int[numOfLeafPagesHear];
            for (int i = 0; i < numOfLeafPagesHear; i++)
            {
                freepages[i] = BitConverter.ToInt32(new byte[] { currentPage[offset + 3 + i * 4], currentPage[offset + 2 + i * 4], currentPage[offset + 1 + i * 4], currentPage[offset + i * 4] }, 0);
                offset = offset + 4;
            }

            foreach (int pageNum in freepages)
            {
                if (pageNum != 0)
                    getDeletedRecordsFromDeletedPage(pageNum);
            }

            if (nextTrunkPageNum != 0)
                getDeletedPagesFromFreeList(nextTrunkPageNum);
            else return;

        }

        private void getDeletedRecordsFromDeletedPage(int deletedLeafPageNum)
        {
            BTreeTraversal(deletedLeafPageNum);
        }

        private void getRecordsFromLeafPagesInTableBTree(int currentPageNum)
        {
            if (currentPageNum == 530)
                Debug.Write("");
            int currentPageOffset = 0;
            if (currentPageNum != 0)
                currentPageOffset = (currentPageNum - 1) * pageSize;
            currentPage = Utils.ReadingFromFile(dbFilePath, currentPageOffset, pageSize);
            int numOfCells = BitConverter.ToInt16(new byte[] { currentPage[4], currentPage[3] }, 0);
            //int cellsOffset = BitConverter.ToInt16(new byte[] { currentPage[6], currentPage[5] }, 0);

            int []cellsOffset=new int[numOfCells];
            int offset = 8;
            for (int i = 0; i < cellsOffset.Length; i++)
            {
                cellsOffset[i] = BitConverter.ToInt16(new byte[] { currentPage[offset + 1], currentPage[offset] }, 0);
                offset = offset + 2;
            }

            foreach (int ptr in cellsOffset)
            {
                if (ptr == 801)
                    
                    Debug.Write("");
                readDbRecordFromCell(ptr);
            }

        }
        /// <summary>
        /// exteract db records from given cell offset.
        /// </summary>
        /// <param name="ptr"></param>
        private void readDbRecordFromCell(int ptr)
        {
            if (ptr == 247)
                Debug.Write("");
            byte[] buffer=new byte[9];
            bool isOverflowPage=false;
            int nextOverflowPage=0;
            try
            {
                Array.Copy(currentPage, ptr, buffer, 0, 9);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (ArgumentException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Array.Copy(currentPage, ptr, buffer, 0, currentPage.Length - ptr);
            }
            long recordSizeValue=0;
            int recordSizeArrayLength=Utils.varInt2Int(buffer, ref recordSizeValue);
            ptr = ptr + recordSizeArrayLength;
            try
            {
                Array.Copy(currentPage, ptr, buffer, 0, 9);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (ArgumentException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                Array.Copy(currentPage, ptr, buffer, 0, currentPage.Length-ptr);
            }
            long keyValueField = 0;
            int keyValueFieldLength = Utils.varInt2Int(buffer, ref keyValueField);
            ptr = ptr + keyValueFieldLength;

            long min_embeded_fraction=headerBytesOfSQLiteFile[22];
            long max_local = pageSize - 35;
            long min_local = (pageSize - 12) * min_embeded_fraction / 255 - 23;
            long local_size = min_local + (recordSizeValue - min_local) % (pageSize - 4);
            if (local_size > max_local)
                local_size = min_local;
            long nextOverFlowPageNumFieldOffset = ptr + local_size;
            Dictionary<int, ArrayList> schema = new Dictionary<int, ArrayList>();

            
            if (recordSizeValue <= max_local)// small records
            {
                long recordHeaderSize = 0;
                nextOverFlowPageNumFieldOffset = pageSize;
                try
                {
                    Array.Copy(currentPage, ptr, buffer, 0, 9);
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (ArgumentException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    Array.Copy(currentPage, ptr, buffer, 0, currentPage.Length - ptr);
                }

                int index = Utils.varInt2Int(buffer, ref recordHeaderSize);
                ptr = ptr + index;
                recordHeaderSize = recordHeaderSize - index;
                
                for (int i = 0; recordHeaderSize!=0; i++)
                {
                    if (ptr==260)
                        Debug.Write("");
                    ArrayList item = extractCurrentColLength(ref ptr, buffer,0,ref recordHeaderSize);
                    schema.Add(i, item);
                }
            }
            else//big records  
                //TODO DEBUG this part
            {
                long recordHeaderSize = 0;
                Array.Copy(currentPage, ptr, buffer, 0, 9);
                int index = Utils.varInt2Int(buffer, ref recordHeaderSize);
                ptr = ptr + index;
                recordHeaderSize = recordHeaderSize - index;

                int colNum = 0;
                for (; ptr < nextOverFlowPageNumFieldOffset && recordHeaderSize != 0; colNum++)
                {
                    if (ptr == 260)
                        Debug.Write("");
                    ArrayList item= extractCurrentColLength(ref ptr, buffer, nextOverFlowPageNumFieldOffset,ref recordHeaderSize);
                    schema.Add(colNum, item);
                }
                if (ptr == nextOverFlowPageNumFieldOffset)
                {
                    ptr = ptr - (int)nextOverFlowPageNumFieldOffset + 4;
                    nextOverflowPage = BitConverter.ToInt32(new byte[] { currentPage[nextOverFlowPageNumFieldOffset + 3], currentPage[nextOverFlowPageNumFieldOffset + 2], currentPage[nextOverFlowPageNumFieldOffset + 1], currentPage[nextOverFlowPageNumFieldOffset] }, 0);
                    getDataFromOverflowPages(nextOverflowPage, ref ptr, ref schema, colNum, ref recordHeaderSize);
                }
            }
            if (schema.Count == 1)
            {
                Debug.WriteLine("");
            }
            //TODO choose a suitable dataset for data grid view  

            Dictionary<int,string> currentRecord=new Dictionary<int,string>();
            for (int i = 0; schema.ContainsKey(i); i++)
            {
                ArrayList item = schema[i];
                string type = (string)item[0];
                long colLength = Convert.ToInt64(item[1]);
                buffer = new byte[colLength];
                int bufPtr = 0;
                nextOverflowPage = BitConverter.ToInt32(new byte[] { currentPage[3], currentPage[2], currentPage[1], currentPage[0] }, 0);

                if (!isOverflowPage)
                {
                    if (colLength + ptr <= nextOverFlowPageNumFieldOffset)
                    {
                        Array.Copy(currentPage, ptr, buffer, 0, colLength);
                        ptr = ptr + (int)colLength;
                        bufPtr = buffer.Length;
                    }
                    else
                    {
                        Array.Copy(currentPage, ptr, buffer, 0, nextOverFlowPageNumFieldOffset - ptr);
                        bufPtr = pageSize - ptr;
                        nextOverflowPage = BitConverter.ToInt32(new byte[] { currentPage[nextOverFlowPageNumFieldOffset + 3], currentPage[nextOverFlowPageNumFieldOffset + 2], currentPage[nextOverFlowPageNumFieldOffset + 1], currentPage[nextOverFlowPageNumFieldOffset] }, 0);
                        ptr = pageSize;

                        isOverflowPage = true;
                    }
                }
                if (isOverflowPage)
                {
                    do
                    {

                        if (pageSize - ptr >= buffer.Length - bufPtr)
                        {
                            Array.Copy(currentPage, ptr, buffer, bufPtr, buffer.Length - bufPtr);
                            bufPtr = buffer.Length;
                            ptr = ptr + buffer.Length - bufPtr;
                            break;
                        }
                        else
                        {
                            Array.Copy(currentPage, ptr, buffer, bufPtr, pageSize - ptr);
                            bufPtr = bufPtr + pageSize - ptr;
                            ptr = pageSize;
                        }
                        currentPage = Utils.ReadingFromFile(dbFilePath, (nextOverflowPage - 1) * pageSize, pageSize);
                        nextOverflowPage = BitConverter.ToInt32(new byte[] { currentPage[3], currentPage[2], currentPage[1], currentPage[0] }, 0);
                        ptr = 4;
                    } while (bufPtr != buffer.Length);
                }

                string value = "";
                byte[] buf;
                switch (type)
                {
                    case "STRING":
                        value = Encoding.UTF8.GetString(buffer);
                        break;
                    case "FLOAT":
                        value = Convert.ToString(BitConverter.ToDouble(buffer, 0));
                        break;
                    case"INTEGER_0":
                        value = "-";
                        break;
                    case "INTEGER_1":
                        buf=new byte[2];
                        Array.Copy(buffer, 0, buf, 0, buffer.Length);
                        value = Convert.ToString(BitConverter.ToInt16(buf, 0));
                        break;
                    case "INTEGER_2":
                        value = Convert.ToString(BitConverter.ToInt16(buffer, 0));
                        break;
                    case "INTEGER_3":
                        buf = new byte[4];
                        Array.Copy(buffer, 0, buf, 0, buffer.Length);
                        value = Convert.ToString(BitConverter.ToInt32(buf, 0));
                        break;
                    case "INTEGER_4":
                        value = Convert.ToString(BitConverter.ToInt32(buffer, 0));
                        break;
                    case "INTEGER_6":
                        buf = new byte[8];
                        Array.Copy(buffer, 0, buf, 0, buffer.Length);
                        value = Convert.ToString(BitConverter.ToInt64(buf, 0));
                        break;
                    case "INTEGER_8":
                        value = Convert.ToString(BitConverter.ToInt64(buffer, 0));
                        break;
                    case "NULL":
                        value = null;
                        break;
                    default:
                        value = BitConverter.ToString(buffer);
                        break;
                }
                currentRecord.Add(i,value);
            }
            records.Add(currentRecord);

        }

        private void getDataFromOverflowPages(int nextOverflowPage, ref int ptr, ref Dictionary<int, ArrayList> schema, int colNum, ref long headerSize)//TODO complete this method
        {
            if (headerSize == 0)
            {
                return;
            }
            else
            {
                currentPage = Utils.ReadingFromFile(dbFilePath, (nextOverflowPage - 1) * pageSize, pageSize);
                nextOverflowPage = BitConverter.ToInt32(new byte[] { currentPage[3], currentPage[2], currentPage[1], currentPage[0] }, 0);
                byte[] buffer = new byte[9];
                ArrayList item = extractCurrentColLength(ref ptr, buffer, pageSize, ref headerSize); 
                schema.Add(colNum, item);
                getDataFromOverflowPages(nextOverflowPage,ref ptr, ref schema,colNum++,ref headerSize);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="buffer"></param>
        /// <param name="nextOverflowPageOffset"></param>
        /// <param name="headerSize"></param>
        /// <returns>Array list of 2 string first one is type and the second one is length.</returns>
        private ArrayList extractCurrentColLength(ref int ptr, byte[] buffer, long nextOverflowPageOffset,ref long headerSize)
        {
            if (ptr == 260)
                Debug.Write("");
            long typeNValue = 0;

            if (nextOverflowPageOffset == 0)
            {
                if (ptr + 9 < pageSize)
                    Array.Copy(currentPage, ptr, buffer, 0, 9);
                else
                    Array.Copy(currentPage, ptr, buffer, 0, pageSize - ptr);
            }
            else
            {
                if (ptr + 9 < nextOverflowPageOffset)
                    Array.Copy(currentPage, ptr, buffer, 0, 9);
                else
                {
                    Array.Copy(currentPage, ptr, buffer, 0, nextOverflowPageOffset - ptr);
                    byte []page=Utils.ReadingFromFile(dbFilePath,(nextOverflowPageOffset-1)*pageSize,pageSize);
                    Array.Copy(page, 5, buffer, nextOverflowPageOffset - ptr, 9 - (nextOverflowPageOffset - ptr));
                }
            }
            
            int index = Utils.varInt2Int(buffer, ref typeNValue);
            ptr = ptr + index;
            headerSize = headerSize - index;
            long colLength = 0;
            string type = "";
            ArrayList item=new ArrayList();
            if (typeNValue > 11 && typeNValue % 2 == 0)
            {
                colLength = (typeNValue - 12) / 2;
                type = "BLOB";
                item.Add(type);
                item.Add(colLength);
            }
            else if (typeNValue > 11 && typeNValue % 2 == 1)
            {
                colLength = (typeNValue - 13) / 2;
                type = "STRING";
                item.Add(type);
                item.Add(colLength);
            }
            else
            {
                try
                {
                    item = sqliteTypes[Convert.ToInt16(typeNValue)];
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (OverflowException ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    Debug.WriteLine("");
                }
            }
            return item;
        }

        internal void WALFileParser()
        {
            throw new NotImplementedException();
        }      
    }

    internal class JournalFileParser
    {
        private string journalFilePath;
        private long pageSize;
        private long sectorSize;
        private const int journalHeaderLength = 28;
        private byte[] journalMagic = new byte[] { 0xd9, 0xd5, 0x05, 0xf9, 0x20, 0xa1, 0x63, 0xd7 };
        private const int checksumLength = 4;
        private Dictionary<long, ArrayList> backupPages = new Dictionary<long, ArrayList>();
        private string dbFilePath;
#pragma warning disable CS0169 // The field 'JournalFileParser.recordsCount' is never used
        private long recordsCount;
#pragma warning restore CS0169 // The field 'JournalFileParser.recordsCount' is never used
        private long fileSize;
        private int maxListLength = 0;
        private string path;
        private string rollbackedFile;
        private Dictionary<string, ArrayList> records;

        internal JournalFileParser(string journalFilePath, string dbFilePath, string workSpacePath)
        {

            this.journalFilePath = journalFilePath;
            this.dbFilePath = dbFilePath;
            this.path = workSpacePath;
            rollbackedFile = workSpacePath + @"\rollBackedFile";
            if (!Directory.Exists(workSpacePath))
            {
                Directory.CreateDirectory(workSpacePath);
            }

            init();

            fillBackupPages();

            records = findDeletedRecords();

        }

        internal Dictionary<string, ArrayList> getDeletedRecords()
        {
            return records;
        }
        /// <summary>
        /// Build old db from journal pages and then find the differences of old db with current one.
        /// </summary>
        /// <returns>records whitch is omited or updated from old db classified by table name.</returns>
        private Dictionary<string, ArrayList> findDeletedRecords()
        {
            List<long> keys = backupPages.Keys.ToList<long>();
            for (int i = 0; i < maxListLength; i++)
            {
                System.IO.File.Copy(dbFilePath, rollbackedFile+"_"+i, true);
                Stream outStream = File.Open(rollbackedFile+"_"+i, FileMode.Open);
                for (int j = 0; j < keys.Count; j++)
                {
                    long pageNum = keys[j];
                    ArrayList list = backupPages[pageNum];
                    if (list.Count != 0)
                    {
                        byte[] currentPage = new byte[pageSize];
                        currentPage = Utils.ReadingFromFile(journalFilePath, (long)list[0], (int)pageSize);
                        outStream.Seek((pageNum - 1) * pageSize, SeekOrigin.Begin);
                        outStream.Write(currentPage, 0, currentPage.Length);
                        list.RemoveAt(0);
                        backupPages[pageNum] = list;
                    }
                }
                outStream.Flush();
                outStream.Close();  
            }

            Dictionary<string, Dictionary<string, ArrayList>> result = new Dictionary<string, Dictionary<string, ArrayList>>();
            for(int i=0;i<maxListLength;i++){
                Utils.getDataBaseDifferences(rollbackedFile + "_" + i, dbFilePath,ref result);
            }
            return getRecords(result);
            
        }
        /// <summary>
        /// retrieved records from old dbs with given queries.
        /// </summary>
        /// <param name="queries">contaion queries catagorized by table name at first and then by db path.</param>
        /// <returns>records that classified by table names.</returns>
        private static Dictionary<string, ArrayList> getRecords(Dictionary<string, Dictionary<string, ArrayList>> queries)
        {
            Dictionary<string, ArrayList> result = new Dictionary<string, ArrayList>();

            foreach (string tableName in queries.Keys)
            {
                ArrayList records = new ArrayList();

                string[] filePathes = queries[tableName].Keys.ToArray();
                int index = 0;
                SQLiteConnection connection = Utils.buildDBConnection(filePathes[index]);

                var cmd = new SQLiteCommand("select * from " + tableName, connection);
                var dr = cmd.ExecuteReader();
                ArrayList colNames = new ArrayList();
                for (var i = 0; i < dr.FieldCount; i++)
                {
                    colNames.Add(dr.GetName(i));
                }
                records.Add(colNames);


                do
                {
                    ArrayList mQuery = queries[tableName][filePathes[index]];
                    foreach (string query in mQuery)
                    {
                        SQLiteCommand com = new SQLiteCommand(query, connection);
                        SQLiteDataReader reader = com.ExecuteReader();

                        while (reader.Read())
                        {
                            ArrayList item = new ArrayList();
                            foreach (string col in colNames)
                                item.Add(reader[col]);
                            records.Add(item);
                        }
                    }
                    Utils.closeSqlitConnection(connection);

                    index++;
                    if (index < filePathes.Length)
                        connection = Utils.buildDBConnection(filePathes[index]);
                } while (index < filePathes.Length);
                if (records.Count > 1)
                    result.Add(tableName, records);
            }
            return result;

        }

        /// <summary>
        /// find offset of leaf pages in journal file.
        /// </summary>
        private void fillBackupPages()
        {
            long offset = sectorSize;
            
            
            while (fileSize/sectorSize!=offset/sectorSize)
            {
                byte[] pageNumArray=Utils.ReadingFromFile(journalFilePath, offset, 5);
                int pageType=pageNumArray[4];
                long currentPageNumber = BitConverter.ToInt32(new byte[] { pageNumArray[3], pageNumArray[2], pageNumArray[1], pageNumArray[0] }, 0);
                offset=offset+4;

                if (pageType == 13)
                {
                    if (backupPages.ContainsKey(currentPageNumber))
                    {
                        ArrayList list = backupPages[currentPageNumber];
                        list.Add(offset);
                        backupPages[currentPageNumber] = list;
                        if (list.Count > maxListLength)
                            maxListLength = list.Count;
                    }
                    else
                    {
                        ArrayList list = new ArrayList();
                        list.Add(offset);
                        backupPages.Add(currentPageNumber, list);
                    }
                }
                offset = offset + pageSize + checksumLength;
            }
            
        }

        private void init()
        {   
            fileSize = Utils.fileSize(journalFilePath);
            byte[] header = Utils.ReadingFromFile(journalFilePath, 0, journalHeaderLength);

            sectorSize = BitConverter.ToInt32(new byte[] { header[23], header[22], header[21], header[20] }, 0);
            pageSize = BitConverter.ToInt32(new byte[] { header[27], header[26], header[25], header[24] }, 0);

            if (sectorSize == 0)
                sectorSize = 512;
            if (pageSize == 0)
            {
                byte[] result = Utils.ReadingFromFile(dbFilePath, 16/*pageSizeOffsetValue*/, 2/*pageSizeLengthValue*/);
                Array.Reverse(result);
                pageSize = BitConverter.ToInt16(result, 0);
            }

        }
    }
}
