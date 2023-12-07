using LensBox.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.Plugins.Managing.ManagingStrategies
{
    internal class Unrestricted : IPluginManagingStrategy
    {
        PluginLoadContext assemblyContext;
        public void Disable(PluginID plugin)
        {
            assemblyContext.Unload();
        }

        public IPlugin Enable(string path)
        {
            App app = App.Current as App;
            string _pluginsDirectory = app.PluginStorage.PluginsDirectory;
            assemblyContext = new PluginLoadContext(_pluginsDirectory);
            var types = assemblyContext.LoadFromAssemblyPath(path).GetTypes();
            var pluginType = types.FirstOrDefault(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract);

            if (pluginType != null)
            {
                var plugin = (IPlugin)Activator.CreateInstance(pluginType);
                if (plugin != null)
                    return plugin;
            }
            throw new Exception("Invalid plugin");
        }
    }
}
