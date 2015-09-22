using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DevicePluginInterface
{
    public interface DeviceRecoveryPluginInterface
    {
        void copyAppDataBaseFromDevice(string key,string path, string distination);

        bool isDeviceRoot();

        bool rootDevice();

        bool unRootDevice();

        bool isDeviceConnected();

        void refreshDeviceList();

        bool installApp(string path);
    }
}
