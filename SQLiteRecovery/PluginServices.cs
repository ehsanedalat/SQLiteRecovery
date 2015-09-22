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
            AssemblyName assamblyName = AssemblyName.GetAssemblyName(path);
            Assembly assembly = Assembly.Load(assamblyName);
            // Get the type to use.
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            Type[] types = assembly.GetTypes();
            ArrayList pluginTypes = new ArrayList();
            foreach (Type type in types)
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue;
                }
                else
                {
                    if (type.GetInterface(pluginType.FullName) != null)
                    {
                        pluginTypes.Add(type);
                    }
                }
            } 
            // Create an instance.
            return Activator.CreateInstance((Type)pluginTypes[0]);
        }

        internal static void copyAppDataBaseFromDevice(object plugin, string key,string path, string distination)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("copyAppDataBaseFromDevice");
            copyMethod.Invoke(plugin, new object[] { key, path, distination });

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
        internal static bool isDeviceConnected(object plugin)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("isDeviceConnected");
            return (bool)copyMethod.Invoke(plugin, null);
        }

        internal static void refreshDeviceList(object plugin)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("refreshDeviceList");
            copyMethod.Invoke(plugin, null);
        }

        internal static bool installApp(object plugin, string path)
        {
            Type pluginType = typeof(DeviceRecoveryPluginInterface);
            MethodInfo copyMethod = pluginType.GetMethod("installApp");
            return (bool)copyMethod.Invoke(plugin, new object[] { path });
        }
    }
}
