using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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
    ///</list>
    /// </summary>
    public interface DeviceRecoveryPluginInterface
    {
        string pluginName { get; set; }
        void root();
       

    }
}
