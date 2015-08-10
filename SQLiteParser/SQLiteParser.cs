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
        private int leafPageHeaderLength = 8;
        private int internalPageHeaderLength = 12;
        private byte[] currentPage;

        public SQLiteParser(string dbFilePath,string tableName)
        {
            pageSize = Utils.getPageSize(dbFilePath);
            rootPageNum = Utils.getRootPageNumber(tableName, dbFilePath);
            
            /*TODO build B-Tree reader
             * get leaf pages
             * pars leaf pages
             * retrive unallocated and free blocks data
             */
        }
    }
}
