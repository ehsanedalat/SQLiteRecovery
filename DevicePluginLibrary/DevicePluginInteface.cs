using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
<<<<<<< HEAD

namespace SQLiteRecovery
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
    interface DeviceRecoveryPluginInterface
    {
        void root();
        void copyFilesFromDevice(string storingPathInComputer, Dictionary<string, string> appsPathInDevice);
        void setAppCheckBoxes();
=======
using System.Windows.Forms;

namespace DevicePluginInterface
{
    /// <summary>
    ///<listheader>
    ///<term>this is plugin interface. for each device we should create a class that inherited this interface.</term>
    ///<description> this interface has 3 methode:</description>
    ///</listheader>
    ///<list type="number">
    ///<item>
    /// <term>root()</term>
    ///<description>this methode should override and  impelement the procedure of accessing root access in each device.</description>
    ///</item>
    ///<item>
    /// <term>copyFilesFromDevice()</term>
    ///<description>this methode is used for copping sqlite DB files from device to computer.</description>
    ///</item>
    ///<item>
    /// <term>setOsTabControls()</term>
    ///<description>this methode is used for producing tab page that should add to main tab of application.</description>
    ///</item>
    ///</list>
    /// </summary>
    public interface DeviceRecoveryPluginInterface
    {
        string pluginName { get; set; }
        void root();
       
>>>>>>> pluginInterfaceLibrary
    }
}
