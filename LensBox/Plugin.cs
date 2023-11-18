using LensBox.Assets;

namespace LensBox.Core
{
    public enum DependencyType
    {
        Required,
        Synergy
    }
    public class Plugin : IPlugin 
    {
        Version Version { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public byte[] Icon { get; private set; }
        public Dictionary<string, DependencyType> Dependencies { get; private set; }

        IEnumerable<ILens> IPlugin.Lenses => throw new NotImplementedException();

        IEnumerable<AssetID> IPlugin.Assets => throw new NotImplementedException();

        PluginID IPlugin.PluginID => throw new NotImplementedException();

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