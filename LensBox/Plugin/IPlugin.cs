using LensBox.Components;
using LensBox.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Loader;
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
        string IDisplayable.Name { get => ID.Name; } //Is it good way?
        public void Init();
        public void Disable();
        public void Enable();
        public void Cleanup();
        public PluginID ID { get; }
        public ReadOnlyDictionary<PluginID, DependencyType> Dependencies { get; }
    }

    //TODO: Move
    public class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext(string pluginPath) : base(isCollectible: true)
        {
            Resolving += (context, assemblyName) =>
            {
                var assemblyPath = Path.Combine(pluginPath, assemblyName.Name + ".dll");
                if (File.Exists(assemblyPath))
                {
                    return LoadFromAssemblyPath(assemblyPath);
                }
                return null;
            };
        }
    }
}
