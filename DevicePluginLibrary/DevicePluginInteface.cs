using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevicePluginInterface
{
    /// <summary>
    ///     this is plugin interface. for each device we should create a class that inherited this interface.
    ///     this interface has 3 methode:
    ///         root()
    ///             this methode should override and  impelement the procedure of accessing root access in each device.
    ///         copyFilesFromDevice()
    ///             this methode is used for copping sqlite DB files from device to computer.
    ///         setAppCheckBoxes()
    ///             this methode is used for producing checkboxes that should add to main tab of device.
    /// </summary>
    public interface DeviceRecoveryPluginInterface
    {
        void root();
        void copyFilesFromDevice(string storingPathInComputer, Dictionary<string, string> appsPathInDevice);
        void setAppCheckBoxes();
    }
}
