using LensBox.Plugin;
using LensHolder_Map.Plugins.Storing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace LensHolder_Map.Plugins.Managing
{
    internal class PluginsManager : LensBox.Components.IComponentProvider
    {
        private static readonly Dictionary<PluginStrategy, Type> strategies =
        new Dictionary<PluginStrategy, Type>(){
            { PluginStrategy.Unrestricted, typeof(Unrestricted) }
        };

        public static readonly ReadOnlyDictionary<PluginStrategy, Type> PluginStrategies = new(strategies);

        #region Singleton
        // #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //         private static PluginsManager _instance;
        // #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        //         public static PluginsManager Instance
        //         {
        //             get
        //             {

        //                 if (_instance == null) _instance = new();
        //                 return _instance;
        //             }
        //             private set => _instance = value;
        //         }
        #endregion

        IPluginStorage _pluginStorage;
        Dictionary<PluginID, PluginConfig> _installedPlugins;
        Dictionary<PluginID, IPlugin> _activePlugins;

        public PluginsManager(IPluginStorage storage)
        {
            _pluginStorage = storage;
            _installedPlugins = LoadPluginConfig();
            _activePlugins = new();
            ActivatePluginsFromConfig();
        }

        private void ActivatePluginsFromConfig()
        {
            _installedPlugins
                .Where(p=>p.Value.IsActive && !_activePlugins.Keys.Contains(p.Key))
                .Select(p => p.Key)
                .ToList()
                .ForEach(EnablePlugin);
        }

        /* TODO: 
*  - Found the best way to store installed plugin list.
*  - I need to store enabled plugins instances, managing strategy(<- and better name for it) and     
*/

        public bool LoadPlugin(string path, IPluginManagingStrategy strategy)
        {
            PluginID plugin = _pluginStorage.InstallPlugin(path);
            _installedPlugins[plugin] = new() { IsActive = false, ManagingStrategy = strategy, Strategy = PluginStrategy.Unrestricted}; // TODO: PluginStrategy changing/picking during instalation
            return true;
        }

        public void DisablePlugin(PluginID plugin)
        {
            var pluginConfig = _installedPlugins[plugin];
            pluginConfig.IsActive = false;
            _activePlugins[plugin].Disable();
            _activePlugins.Remove(plugin);
            _installedPlugins[plugin] = pluginConfig;
        }

        public void EnablePlugin(PluginID plugin)
        {
            var pluginConfig = _installedPlugins[plugin];
            var strategie = pluginConfig.ManagingStrategy;
            string pluginPath = _pluginStorage.GetPluginPath(plugin);
            _activePlugins[plugin] = strategie.Enable(pluginPath);
            _activePlugins[plugin].Enable();
            pluginConfig.IsActive = true;
            _installedPlugins[plugin] = pluginConfig;
        }

        public void DeletePlugin(PluginID plugin)
        {
            if (_activePlugins.ContainsKey(plugin))
            {
                DisablePlugin(plugin);
            }
            _installedPlugins.Remove(plugin);
            _pluginStorage.UninstallPlugin(plugin);
        }

        Dictionary<Type, List<object>> _gatheredComponents = new();

        void GatherComponents(Type t)
        {

            if (_gatheredComponents.Keys.Contains(t))
            {
                _gatheredComponents.Remove(t);
            }

            _gatheredComponents[t] = new();

            foreach (var plugin in _activePlugins)
                _gatheredComponents[t].AddRange(plugin.Value.GetComponentsOfType(t));
        }

        public bool HasComponentsOfType(Type t)
        {
            return _gatheredComponents.Keys.Contains(t);
        }

        public IEnumerable<object> GetComponentsOfType(Type t)
        {
            if (!HasComponentsOfType(t))
                GatherComponents(t);

            return _gatheredComponents[t];
        }
        private bool SavePluginConfig(string path)
        {
            var json = JsonConvert.SerializeObject(_installedPlugins, new PluginConfigJsonConverter(strategies));
            File.WriteAllText(path, json);
            return true;
        }

        private Dictionary<PluginID, PluginConfig> LoadPluginConfig()
        {
            var json = File.ReadAllText("plugins.json");
            var deserialized = JsonConvert.DeserializeObject<Dictionary<PluginID, PluginConfig>>(
              json,
              new PluginConfigJsonConverter(strategies));
            return deserialized ?? new();
        }

        [Serializable]
        private struct PluginConfig
        {
            public PluginStrategy Strategy;
            public bool IsActive;
            [NonSerialized]
            public IPluginManagingStrategy ManagingStrategy;
        }

        private class PluginConfigJsonConverter : JsonConverter<PluginConfig>
        {
            private Dictionary<PluginStrategy, Type> strategies;

            public PluginConfigJsonConverter(Dictionary<PluginStrategy, Type> strategies)
            {
                this.strategies = strategies;
            }

            public override void WriteJson(JsonWriter writer, PluginConfig value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value.Strategy);
            }

            public override PluginConfig ReadJson(JsonReader reader, Type objectType, PluginConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var config = serializer.Deserialize<PluginConfig>(reader);
                var type = strategies[config.Strategy];
                config.ManagingStrategy = (IPluginManagingStrategy)Activator.CreateInstance(type);
                return config;
            }
        }
    }
}
