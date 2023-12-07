using LensBox.Assets;
using LensBox.Components;
using LensBox.Plugin.Attributes;
using System.Collections.ObjectModel;
using System.Reflection;

namespace LensBox.Plugin
{

    public abstract class PluginBase : MarshalByRefObject, IPlugin
    {
        protected Dictionary<Type, List<object>> components = new();

        public PluginID ID { get; private set; }

        public abstract ReadOnlyDictionary<PluginID, DependencyType> Dependencies { get; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public byte[] Icon { get; private set; }

        public abstract void Cleanup();

        public abstract void Disable();

        public abstract void Enable();

        protected PluginBase()
        {
            //TODO: Gather components by its attributes, where possible
            //      and get plugin ID and displayable properties from
            //      attribute
            if (ID.Name == null || ID.Name.Length == 0)
            {
                var pluginMeta = GetType().GetCustomAttributes(typeof(PluginAttribute), true)[0] as PluginAttribute;
                if (pluginMeta != null)
                    ID = pluginMeta.ID;
                else
                    throw new InvalidOperationException("Plugin have no ID");
            }
            if (Description == null || Name == null || Icon == null)
            {
                var displayableMeta = GetType().GetCustomAttributes(typeof(DisplayableAttribute), true)[0] as DisplayableAttribute;
                if (displayableMeta != null)
                {
                    Description = displayableMeta.Description;
                    Name = displayableMeta.Description;
                    Icon = displayableMeta.Icon;
                }
            }
            GatherComponents();
        }

        void GatherComponents()
        {
            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            var componentGeneartorsInfo = methods
                .Where(m => 
                       m.GetCustomAttributes(typeof(ComponentAttribute))
                       .Count() > 0
                    )
                .ToList();

            foreach (var methodInfo in componentGeneartorsInfo)
            {
                var type = methodInfo.ReturnType;
                if (!components.ContainsKey(type) || components[type] == null)
                    components[type] = new();
                object component = methodInfo.Invoke(this, null);
                if (component != null)
                    components[type].Add(component);
                else
                    throw new InvalidOperationException($"Method {methodInfo.Name} of class {methodInfo.DeclaringType.Name}, marked as Component returned null");
            }
        }

        public bool HasComponentsOfType(Type t)
        {
            return components.Keys.Contains(t);
        }

        public IEnumerable<object> GetComponentsOfType(Type t)
        {
            return components.GetValueOrDefault(t, null);
        }
        public IEnumerable<T> GetComponentsOfType<T>()
        {
            return GetComponentsOfType(typeof(T)).Cast<T>(); //WARN:Looks slow
        }

        public abstract void Init();
    }
}