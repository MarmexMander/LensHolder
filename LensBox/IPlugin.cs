using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal interface IPlugin
    {
        public void Init();
        public Task AsyncUpdate();
        public bool CheckUpdates();
        public IEnumerable<ILens> Lenses { get; }
        public PluginID PluginID { get; }
    }
}
