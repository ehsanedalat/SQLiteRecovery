using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevicePluginInterface;

namespace AndroidPlugin
{
    public class AndroidPlugin : DeviceRecoveryPluginInterface
    {
        #region overide interface methodes
        
        public void root()
        {
            throw new NotImplementedException();
        }

        public void copyFilesFromDevice(string storingPathInComputer, Dictionary<string, string> appsPathInDevice)
        {
            throw new NotImplementedException();
        }

        public void setAppCheckBoxes()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
