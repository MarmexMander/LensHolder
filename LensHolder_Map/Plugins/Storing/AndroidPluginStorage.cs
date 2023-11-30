using LensBox.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace LensHolder_Map.Plugins.Storing
{
    internal class AndroidPluginStorage : IPluginStorage
    {
        private readonly string _pluginsDirectory;
        private Dictionary<PluginID, string> _installedPlugins = new Dictionary<PluginID, string>();
        private readonly string _pluginCacheFileName = "installed_plugins.cache";

        public AndroidPluginStorage()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            _pluginsDirectory = Path.Combine(appDataDir, "Plugins");
            Directory.CreateDirectory(_pluginsDirectory);
        }

        public IEnumerable<PluginID> GetInstalledPlugins()
        {
            return _installedPlugins.Keys;
        }

        public string GetPluginPath(PluginID plugin)
        {
            if (_installedPlugins.TryGetValue(plugin, out var path))
            {
                return path;
            }

            return null;
        }

        public void InstallPlugin(string filePath)
        {
            // Generate a unique file name for the copied DLL
            var fileName = Guid.NewGuid().ToString("N") + ".dll";
            var destinationPath = Path.Combine(_pluginsDirectory, fileName);

            // Copy the DLL to the plugins folder
            File.Copy(filePath, destinationPath, overwrite: true);

            // Load the plugin from the copied DLL
            var context = new PluginLoadContext(_pluginsDirectory);
            var pluginType = context.LoadFromAssemblyPath(destinationPath).GetTypes()
                                .FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);

            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                _installedPlugins[plugin.ID] = destinationPath;
            }
            context.Unload();
            SaveInstalledPluginsToCache();
        }

        public void UninstallPlugin(PluginID plugin)
        {
            if (_installedPlugins.TryGetValue(plugin, out var path))
            {
                File.Delete(path);
                _installedPlugins.Remove(plugin);
            }
        }

        private void LoadInstalledPluginsFromCache()
        {
            string cacheFilePath = Path.Combine(_pluginsDirectory, _pluginCacheFileName);

            try
            {
                if (File.Exists(cacheFilePath))
                {
                    var lines = File.ReadAllLines(cacheFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            var pluginName = parts[0];
                            var pluginPath = parts[1];
                            var pluginID = PluginID.FromString(line);
                            _installedPlugins[pluginID] = Path.Combine(_pluginsDirectory, pluginPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log or display an error message
                Console.WriteLine($"Error loading plugin cache: {ex.Message}");
            }
        }

        private void SaveInstalledPluginsToCache()
        {
            string cacheFilePath = Path.Combine(_pluginsDirectory, _pluginCacheFileName);

            try
            {
                var lines = _installedPlugins.Select(kv => $"{kv.Key}|{kv.Value}");
                File.WriteAllLines(cacheFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving plugin cache: {ex.Message}");
            }
        }

        private class PluginLoadContext : AssemblyLoadContext
        {
            public PluginLoadContext(string pluginPath) : base(isCollectible: true)
            {
                Resolving += (context, assemblyName) =>
                {
                    var assemblyPath = Path.Combine(pluginPath, assemblyName.Name + ".dll");
                    if (File.Exists(assemblyPath))
                    {
                        return LoadFromAssemblyPath(assemblyPath);
                    }
                    return null;
                };
            }
        }

    }
}
