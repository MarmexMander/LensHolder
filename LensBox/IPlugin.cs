using LensBox.Components;
using LensBox.Core;
using LensBox.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    public enum DependencyType
    {
        Required,
        Synergy
    }

    public interface IPlugin: IDisplayable
    {
        public void Init();
        public Task AsyncUpdate();
        public bool CheckUpdates();
        public IEnumerable<ILens> Lenses { get; }
        //public IEnumerable<Assets.AssetID> Assets { get; }
        public PluginID ID { get; }
        public Dictionary<PluginID, DependencyType> Dependencies { get;}
    }
}
