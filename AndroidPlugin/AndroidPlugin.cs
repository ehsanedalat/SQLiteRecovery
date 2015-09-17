using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevicePluginInterface;
using System.Windows.Forms;
using RegawMOD.Android;
using System.Diagnostics;
using System.Threading;

namespace AndroidPlugin
{
    public class AndroidPlugin : DeviceRecoveryPluginInterface
    {
        private AndroidController android;
        private Device device;
        private Thread thread;

        public AndroidPlugin()
        {
            android = AndroidController.Instance;
            DeviceConnect += ConnectingDevice;
            if (!android.HasConnectedDevices)
            {
                thread = new Thread(waitForDevice);
                thread.Start();
            }
            else
            {
                device = android.GetConnectedDevice();
            }
        }
        private void waitForDevice()
        {
            android.WaitForDevice();
            OnDeviceConnected(EventArgs.Empty);

            thread.Abort();
        }
        protected virtual void OnDeviceConnected(EventArgs e)
        {
            EventHandler handler = DeviceConnect;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public EventHandler DeviceConnect;

        public void ConnectingDevice(object sender, EventArgs e)
        {
            device = android.GetConnectedDevice();
        }

        public void copyAppsDataBaseFromDevice(Dictionary<string, string> apps, string destination)
        {
            foreach (string key in apps.Keys)
            {
                device.PullFile(apps[key], destination);
                device.PullFile(apps[key] + "-journal", destination);
                /*AdbCommand adbCmd = Adb.FormAdbShellCommand(device, true, "chmod", new object[] { 777, apps[key] });
                Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));
                adbCmd = Adb.FormAdbShellCommand(device, true, "chmod", new object[] { 777, apps[key] + "-journal" });
                Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));
                adbCmd = Adb.FormAdbCommand(device, "pull", apps[key], destination);
                Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));
                adbCmd = Adb.FormAdbCommand(device, "pull", apps[key]+"-journal", destination);
                Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));*/
            }
        }

        public bool isDeviceRoot()
        { 
            return device.HasRoot;
        }

        public bool rootDevice()
        {
            throw new NotImplementedException();
        }

        public bool unRootDevice()
        {
            throw new NotImplementedException();
        }
    }
}
