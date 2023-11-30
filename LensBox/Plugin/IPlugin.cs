using LensBox.Components;
using LensBox.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin
{
    public enum DependencyType
    {
        Required,
        Synergy
    }

    public interface IPlugin : IDisplayable, IComponentProvider
    {
        public void Init();
        public void Disable();
        public void Enable();
        public void Cleanup();
        public PluginID ID { get; }
        public Dictionary<PluginID, DependencyType> Dependencies { get; }
    }
}
