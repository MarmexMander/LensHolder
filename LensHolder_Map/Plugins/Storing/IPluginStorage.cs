using Avalonia.Platform.Storage;
using LensBox.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.Plugins.Storing
{
    internal interface IPluginStorage
    {
        public IEnumerable<PluginID> GetInstalledPlugins();
        public string GetPluginPath(PluginID plugin);
        public PluginID InstallPlugin(IStorageFile path);
        public void UninstallPlugin(PluginID plugin);

        public string PluginsDirectory { get; }
    }
}
