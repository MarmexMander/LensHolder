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
        public void Disable(PluginID plugin)
        {
            throw new NotImplementedException();
        }

        public Plugin Enable(string path)
        {
            throw new NotImplementedException();
        }
    }
}
