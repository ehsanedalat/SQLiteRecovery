using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using DevicePluginInterface;
using System.Collections;

namespace SQLiteRecovery
{
    class PluginServices
    {
        
        internal static object loadPlugin(string path)
        {
            // Use the file name to load the assembly into the current
            // application domain.
            Assembly a = Assembly.Load(path);
            // Get the type to use.
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            // Create an instance.
            return Activator.CreateInstance(pluginType);
        }

        internal static void copyAppsDataBaseFromDevice(object plugin, Dictionary<string, string> apps, string distination)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("copyAppsDataBaseFromDevice");
            copyMethod.Invoke(plugin, new object[] { apps, distination });
        }

        internal static bool isDeviceRoot(object plugin)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("isDeviceRoot");
            return (bool)copyMethod.Invoke(plugin, null);
        }

        internal static bool rootDevice(object plugin)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("rootDevice");
            return (bool)copyMethod.Invoke(plugin, null);
        }

        internal static bool unRootDevice(object plugin)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("unRootDevice");
            return (bool)copyMethod.Invoke(plugin, null);
        }
    }
}
