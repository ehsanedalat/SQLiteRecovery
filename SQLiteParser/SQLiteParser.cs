using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace SQLiteParser
{
    class SQLiteParser
    {
        private const int internalNodeTypeValue = 5;
        private const int leafNodeTypeValue = 13;
        private int pageSize;
        private int rootPageNum;
        private const int leafPageHeaderLength = 8;
        private const int internalPageHeaderLength = 12;
        private const int pageSizeOffsetValue = 16;
        private const int pageSizeLengthValue = 2;
        private byte[] currentPage;
        private string dbFilePath;
        private const int internalPageCellsPionerLength = 4;
        public ArrayList deletedRecords { get; private set; }

        /*public SQLiteParser(string dbFilePathForIO, string dbFilePathForQuery, string tableName)
        {
            pageSize = getPageSize(dbFilePathForIO);
            rootPageNum = Utils.getRootPageNumber(tableName, dbFilePathForQuery);
            this.dbFilePath = dbFilePathForIO;
            deletedRecords = new ArrayList();

            BTreeTraversal(rootPageNum);

        }*/

        private void BTreeTraversal(int currentPageNum)
        {
            int currentPageOffset = 0;
            if(currentPageNum!=0)
                currentPageOffset = (currentPageNum - 1) * pageSize;
            currentPage = Utils.ReadingFromFile(dbFilePath, currentPageOffset, pageSize);
            int numOfCells = BitConverter.ToInt16(new byte[] { currentPage[4], currentPage[3] }, 0);
            int cellsOffset = BitConverter.ToInt16(new byte[] { currentPage[6], currentPage[5] }, 0);

            if (currentPage[0] == leafNodeTypeValue)
            {
                getDeletedRecords(numOfCells, cellsOffset, currentPageNum);
                return;
            }
            else if (currentPage[0] == internalNodeTypeValue)
            {
                int[] childPtr = getChildsPtr(numOfCells, ref cellsOffset);
                getDeletedRecords(numOfCells, cellsOffset, currentPageNum);

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

        private void getDeletedRecords(int numOfCells, int cellsOffset, int pageNum)
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
            if(!String.IsNullOrEmpty(data))
                deletedRecords.Add(new string[] { pageNum + "", "UNALLOCATED", data });
        }

        private void getDataFromFreeBlock(int currentFreeBlockOffset, int currentFreeBlockSize, int pageNum)
        {
            string data = Encoding.UTF8.GetString(currentPage, currentFreeBlockOffset + 4, currentFreeBlockSize - 4);
            data = data.Replace("\0", "");
            if (!String.IsNullOrEmpty(data))
                deletedRecords.Add(new string[] { pageNum + "", "FREEBLOCK", data });
        }

        private int getPageSize(string fileName)
        {
            byte[] result = Utils.ReadingFromFile(fileName, pageSizeOffsetValue, pageSizeLengthValue);
            Array.Reverse(result);
            return BitConverter.ToInt16(result, 0);
        }


        
        public SQLiteParser(string dbFilePath, string dbCopyFilePath, bool resultInFile, string resultPath)
        {
            this.dbFilePath = dbFilePath;
            pageSize = getPageSize(dbFilePath);
            ArrayList tableInfo = Utils.getAllTableInfo(dbCopyFilePath);
            this.deletedRecords = new ArrayList();
            foreach (string[] item in tableInfo)
            {
                BTreeTraversal(Convert.ToInt32(item[1]));
            }

            if (resultInFile)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(resultPath, FileMode.Create)))
                {
                    foreach (string[] item in this.deletedRecords)
                        writer.Write("page #: " + item[0] + " | type: " + item[1] + " | DATA: " + item[2] + "\r\n");
                }
            }
        }
    }

    

    
}
