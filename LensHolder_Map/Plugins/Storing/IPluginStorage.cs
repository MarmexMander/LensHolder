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
        public void InstallPlugin(string path);
        public void UninstallPlugin(PluginID plugin);
    }
}
