using LensBox.Components;
using LensBox.Core;
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

    public interface IPlugin : IDisplayable
    {
        public void Init();
        public void Disable();
        public void Cleanup();
        public IEnumerable<ILens> Lenses { get; }
        public IEnumerable<IAction> Actions { get; }
        //public IEnumerable<Assets.AssetID> Assets { get; }
        public PluginID ID { get; }
        public Dictionary<PluginID, DependencyType> Dependencies { get; }
    }
}
