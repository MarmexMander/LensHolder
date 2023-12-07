using Avalonia.Platform.Storage;
using LensBox.Plugin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace LensHolder_Map.Plugins.Storing
{
    internal class AndroidPluginStorage : IPluginStorage
    {
        private readonly string _pluginsDirectory;
        private Dictionary<PluginID, string> _installedPlugins = new Dictionary<PluginID, string>();
        private readonly string _pluginCacheFileName = "installed_plugins.cache";

        public string PluginsDirectory { get => _pluginsDirectory; }

        public AndroidPluginStorage()
        {
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _pluginsDirectory = Path.Combine(appDataDir, "Plugins");
            Directory.CreateDirectory(_pluginsDirectory);
            LoadInstalledPluginsFromCache();
            ValidateCache();
        }

        //TODO: Implement
        private bool ValidateCache(){
            return true;
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

        public PluginID InstallPlugin(IStorageFile file)
        {
            var task = InstallPluginAsync(file);
            task.Wait();
            return task.Result;
        }
        //TODO: Refactor
        public async Task<PluginID> InstallPluginAsync(IStorageFile file)
        {
            int buffSize = 1024 * 1024;
            // Generate a unique file name for the copied DLL
            var fileName = Guid.NewGuid().ToString("N") + ".dll";
            var destinationPath = Path.Combine(_pluginsDirectory, fileName);

            // Copy the DLL to the plugins folder
            using (FileStream fileStream = File.Create(destinationPath))
            {
                Stream fs = await file.OpenReadAsync();
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[buffSize];

                while ((bytesRead = fs.Read(bytes, 0, buffSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
                fs.Close();
            }
            // Load the plugin from the copied DLL
            var context = new PluginLoadContext(_pluginsDirectory);
            var types = context.LoadFromAssemblyPath(destinationPath).GetTypes();
            var pluginType = types.FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);

            PluginID pluginID;
            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                pluginID = plugin.ID;
                _installedPlugins[pluginID] = destinationPath;
            }
            context.Unload();
            SaveInstalledPluginsToCache();
            return pluginID;
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
                    var lines = File.ReadAllText(cacheFilePath);
                    _installedPlugins = JsonConvert.DeserializeObject<Dictionary<PluginID, string>>(lines) ?? new();
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
                var lines = JsonConvert.SerializeObject(_installedPlugins);
                File.WriteAllText(cacheFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving plugin cache: {ex.Message}");
            }
        }



    }
}
