using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using DevicePluginInterface;

namespace SQLiteRecovery
{
    class PluginServices
    {
        /// <summary>
        ///     this class is used for loading plugins that is stored in ".dll" file format in special directory.
        /// </summary>
        /// <param name="path">
        ///     path is a Address of saved plugins directory.(type: string)
        /// </param>
        /// <returns>
        ///     list of all plugins that exists in path directory.(type: ICollection)
        /// </returns>

        public static ICollection<DeviceRecoveryPluginInterface> LoadPlugins(string path)
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(DeviceRecoveryPluginInterface);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

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
                    }
                }

                ICollection<DeviceRecoveryPluginInterface> plugins = new List<DeviceRecoveryPluginInterface>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    DeviceRecoveryPluginInterface plugin = (DeviceRecoveryPluginInterface)Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }
    }
}
