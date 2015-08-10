using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        private const int internalPageCellsHeaderLength = 12;
        private Dictionary<int, Dictionary<string, string>> deletedRecords;

        public SQLiteParser(string dbFilePath,string tableName)
        {
            pageSize = getPageSize(dbFilePath);
            rootPageNum = Utils.getRootPageNumber(tableName, dbFilePath);
            this.dbFilePath = dbFilePath;
            deletedRecords=new Dictionary<int,Dictionary<string,string>>();

            allNodes(rootPageNum);

            /*TODO
             * retrive unallocated and free blocks data
             
             */
        }

        private void allNodes(int rootPageNum)
        {
            int currentPageOffset = (rootPageNum - 1) * pageSize;
            currentPage = Utils.ReadingFromFile(dbFilePath, currentPageOffset, pageSize);
            int numOfCells = BitConverter.ToInt16(new byte[] { currentPage[4], currentPage[3] }, 0);
            int cellsOffset = BitConverter.ToInt16(new byte[] { currentPage[6], currentPage[5] }, 0);
            
            if (currentPage[0] == leafNodeTypeValue)
            {
                getDeletedRecords(numOfCells,cellsOffset);
                return;
            }
            else if (currentPage[0] == internalNodeTypeValue)
            {
                int[] childPtr = getChildsPtr(numOfCells, ref cellsOffset);

                foreach (int ptr in childPtr)
                {
                    allNodes(ptr);
                }
            }
        }

        private int[] getChildsPtr(int numOfCells, ref int cellsOffset)
        {
            int[] childPtr = new int[numOfCells + 1];
            for (int i = 0; i < childPtr.Length - 1; i++)
            {
                childPtr[i] = BitConverter.ToInt32(new byte[] { currentPage[cellsOffset + 3], currentPage[cellsOffset + 2], currentPage[cellsOffset + 1], currentPage[cellsOffset] }, 0);
                cellsOffset = cellsOffset + internalPageCellsHeaderLength;
            }
            childPtr[childPtr.Length - 1] = BitConverter.ToInt32(new byte[] { currentPage[11], currentPage[10], currentPage[9], currentPage[8] }, 0);
            return childPtr;
        }

        private void getDeletedRecords(int numOfCells, int cellsOffset)
        {
            getAllFreeBlockListData(BitConverter.ToInt16(new byte[] { currentPage[2], currentPage[1] }, 0));
            getDataFromUnallocatedSpace(numOfCells*2+leafPageHeaderLength,cellsOffset);
        }

        private void getDataFromUnallocatedSpace(int unallocatedSpaceOffset, int cellsOffset)
        {
            throw new NotImplementedException();
        }

        private void getAllFreeBlockListData(int currentFreeBlockOffset)
        {
            if (currentFreeBlockOffset != 0)
            {
                int nextFreeBlockOffset = BitConverter.ToInt16(new byte[] { currentPage[currentFreeBlockOffset + 1], currentPage[currentFreeBlockOffset] }, 0);
                int currentFreeBlockSize = BitConverter.ToInt16(new byte[] { currentPage[currentFreeBlockOffset + 3], currentPage[currentFreeBlockOffset + 2] }, 0);
                getDataFromFreeBlock(currentFreeBlockOffset,currentFreeBlockSize);
                getAllFreeBlockListData(nextFreeBlockOffset);
            }
            else if (currentFreeBlockOffset == 0)
            {
                int currentFreeBlockSize = BitConverter.ToInt16(new byte[] { currentPage[currentFreeBlockOffset + 3], currentPage[currentFreeBlockOffset + 2] }, 0);
                getDataFromFreeBlock(currentFreeBlockOffset, currentFreeBlockSize);
                return;
            }
        }

        private void getDataFromFreeBlock(int currentFreeBlockOffset, int currentFreeBlockSize)
        {
            throw new NotImplementedException();
        }

        private int getPageSize(string fileName)
        {
            byte[] result = Utils.ReadingFromFile(fileName, pageSizeOffsetValue, pageSizeLengthValue);
            Array.Reverse(result);
            return BitConverter.ToInt16(result, 0);
        }
    }
}
