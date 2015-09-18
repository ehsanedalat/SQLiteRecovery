using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevicePluginInterface;
using System.Windows.Forms;
using RegawMOD.Android;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace AndroidPlugin
{
    public class Plugin : DeviceRecoveryPluginInterface
    {
        private AndroidController android;
        private Device device;
        private Thread thread;

        public Plugin()
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

        public void copyAppDataBaseFromDevice(string key, string path, string destination)
        {
            if (!Directory.Exists(destination + key))
            {
                Directory.CreateDirectory(destination + key);
            }
            AdbCommand adbCmd = Adb.FormAdbShellCommand(device, true, "chmod", new object[] { 777, path });
            Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));
            adbCmd = Adb.FormAdbShellCommand(device, true, "chmod", new object[] { 777, path + "-journal" });
            Debug.WriteLine(Adb.ExecuteAdbCommand(adbCmd));

            device.PullFile(path, destination + key);
            device.PullFile(path + "-journal", destination + key);

        }

        public bool isDeviceRoot()
        { 
            return device.HasRoot;
        }


        public bool isDeviceConnected()
        {
            return android.HasConnectedDevices;
        }
        
        public bool rootDevice()
        {
            throw new NotImplementedException();
        }

        public bool unRootDevice()
        {
            throw new NotImplementedException();
        }

        public void refreshDeviceList()
        {
            android.UpdateDeviceList();
        }
    }
}
