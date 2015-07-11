using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteRecovery
{
    interface DeviceRecoveryPlugin
    {
        void root();
        void copyFilesFromDevice(string storingPathInComputer, Dictionary<string,string> appsPathInDevice);
        void setAppCheckBoxes();
    }
}
