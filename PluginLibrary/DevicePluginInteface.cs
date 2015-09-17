using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DevicePluginInterface
{
    public interface DeviceRecoveryPluginInterface
    {
        void copyAppsDataBaseFromDevice(Dictionary<string, string> apps, string distination);

        bool isDeviceRoot();

        bool rootDevice();

        bool unRootDevice();
    }
}
