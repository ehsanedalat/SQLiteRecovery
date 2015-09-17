using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DevicePluginInterface
{
    public interface DeviceRecoveryPluginInterface
    {
        public void copyAppsDataBaseFromDevice(Dictionary<string, string> apps, string distination);

        public bool isDeviceRoot();

        public bool rootDevice();

        public bool unRootDevice();
    }
}
