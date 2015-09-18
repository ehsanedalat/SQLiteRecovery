using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroidPlugin;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Plugin plugin = new Plugin();
            Dictionary<string, string> apps = new Dictionary<string, string>();
            apps.Add("SMS", "/data/data/com.android.providers.telephony/databases/mmssms.db");
            apps.Add("Contacts", "/data/data/com.android.providers.contacts/databases/contacts2.db");
            plugin.copyAppDataBaseFromDevice("SMS", "/data/data/com.android.providers.telephony/databases/mmssms.db", @"F:\SQLite DBs\Copy\");
            Console.WriteLine(plugin.isDeviceRoot());
            Console.ReadLine();
        }
    }
}
