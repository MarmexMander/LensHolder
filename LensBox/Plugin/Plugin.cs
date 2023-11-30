using LensBox.Assets;
using LensBox.Components;

namespace LensBox.Plugin
{

    public abstract class Plugin : MarshalByRefObject, IPlugin
    {
        public PluginID ID => throw new NotImplementedException();

        public Dictionary<PluginID, DependencyType> Dependencies => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public byte[] Icon => throw new NotImplementedException();

        public IDictionary<Type, IEnumerable<object>> Components => throw new NotImplementedException();

        public void Cleanup()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public bool HasComponentsOfType(Type t)
        {
            throw new NotImplementedException();
        }

        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}