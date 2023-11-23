using LensBox.Assets;
using LensBox.Components;

namespace LensBox.Plugin
{

    public abstract class Plugin : IPlugin
    {
        public PluginID ID => throw new NotImplementedException();
        Version Version { get; set; }
        public string Name { get => ID.Name; }
        public string Description { get; private set; }
        public byte[] Icon { get; private set; }
        public Dictionary<string, DependencyType> Dependencies { get; private set; }

        IEnumerable<ILens> IPlugin.Lenses => throw new NotImplementedException();

        Dictionary<PluginID, DependencyType> IPlugin.Dependencies => throw new NotImplementedException();

        public void Init()
        {
            throw new NotImplementedException();
        }

        public Task AsyncUpdate()
        {
            throw new NotImplementedException();
        }

        public bool CheckUpdates()
        {
            throw new NotImplementedException();
        }
    }
}