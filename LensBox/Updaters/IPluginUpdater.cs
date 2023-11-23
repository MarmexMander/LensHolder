using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Updaters
{
    public interface IPluginUpdater
    {
        public Task AsyncUpdate();
        public bool CheckUpdates();
    }
}
