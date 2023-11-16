using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Assets
{
    enum SecurityLevel {
        Global,
        Local,
        Shared,
        Private
    }
    internal struct AssetID
    {
        public PluginID OwnerPlugin { get; private set;}
        public string Name { get; private set;}
        public SecurityLevel SecurityLevel { get; private set;}
    }
}
