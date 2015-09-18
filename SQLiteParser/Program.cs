using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SQLiteParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Int64 value = 0;
            //int index = Utils.vaiInt2Int(new byte[] { 0x2B, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }, ref value);
            SQLiteLibrary sqlite = new SQLiteLibrary(@"F:\SQLite DBs\MMSSMS\Seyed\", @"F:\SQLite DBs\MMSSMS\Seyed\mmssms.db", "mmssms.db");
            //SQLiteInterface sqlite = new SQLiteInterface(@"F:\SQLite DBs\Browser\MyChrome\","History","History_c");
            //sqlite.readSMS();
            //sqlite.freeListRetrival();
            sqlite.unAllocatedSpases();
            sqlite.journalRecovery(@"F:\SQLite DBs\MMSSMS\Seyed\mmssms.db-journal");
            sqlite.getAllTableRecords("sms", "9125169683");

            Console.ReadLine();
        }
    }
}
