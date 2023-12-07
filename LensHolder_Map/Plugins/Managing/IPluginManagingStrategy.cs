using LensBox.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.Plugins.Managing
{
    enum PluginStrategy
    {
        Unrestricted,
    }

    internal interface IPluginManagingStrategy
    {
        public IPlugin Enable(string path);
        public void Disable(PluginID plugin);
    }
}
