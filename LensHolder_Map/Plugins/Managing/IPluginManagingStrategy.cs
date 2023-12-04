using LensBox.Plugin;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        public Plugin Enable(string path);
        public void Disable(PluginID plugin);
    }
}
