using LensBox.Components;
using LensBox.Updaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin
{
    internal abstract class UpdateblePlugin : IPlugin
    {
        private IPluginUpdater _updater;
        public Version GetVersion() =>
            Assembly.GetExecutingAssembly().GetName().Version
            ?? throw new InvalidOperationException($"Version information is not provided in the assembly of plugin with ID: {ID}.");
        public abstract void Init();
        public abstract void Disable();
        public abstract void Cleanup();

        public abstract void Enable();

        public abstract IEnumerable<object> GetComponentsOfType(Type t);

        public abstract bool HasComponentsOfType(Type t);

        public IPluginUpdater Updater { get => _updater; }
        public abstract IEnumerable<IAction> Actions { get; }
        public abstract PluginID ID { get; }
        public abstract Dictionary<PluginID, DependencyType> Dependencies { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract byte[] Icon { get; }
    }
}
