using LensBox.Plugin;
using LensHolder_Map.Plugins.Storing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net.WebSockets;
using System.Reflection;
using Avalonia.Platform.Storage;

namespace LensHolder_Map.Plugins.Managing
{
    internal class PluginsManager : LensBox.Components.IComponentProvider
    {
        const string PLUGIN_CONFIG_NAME = "plugins_config.json";

        public IEnumerable<IPlugin> Plugins { get => _activePlugins.Values; }

        //REFACTOR: Move it somwhere. For example near the PluginStrategy
        private static readonly Dictionary<PluginStrategy, Type> strategies =
        new Dictionary<PluginStrategy, Type>(){
            { PluginStrategy.Unrestricted, typeof(ManagingStrategies.Unrestricted) }
        };

        public static readonly ReadOnlyDictionary<PluginStrategy, Type> PluginStrategies = new(strategies);
        private string _pluginConfigPath;

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
            var _appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _pluginConfigPath = Path.Combine(_appDataDir, PLUGIN_CONFIG_NAME);
            _pluginStorage = storage;
            _installedPlugins = LoadPluginConfig();
            _activePlugins = new();
        }

        public void Init()
        {
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

        public bool LoadPlugin(IStorageFile file, PluginStrategy strategy)
        {
            PluginID plugin = _pluginStorage.InstallPlugin(file);
            _installedPlugins[plugin] = new() { IsActive = false, ManagingStrategy = Activator.CreateInstance(strategies[strategy]) as IPluginManagingStrategy, Strategy = strategy}; // TODO: PluginStrategy changing/picking during instalation; REFACTOR: Activato null check
            SavePluginConfig();
            return true;
        }

        public void DisablePlugin(PluginID plugin)
        {
            var pluginConfig = _installedPlugins[plugin];
            pluginConfig.IsActive = false;
            _activePlugins[plugin].Disable();
            _activePlugins.Remove(plugin);
            _installedPlugins[plugin] = pluginConfig;
            SavePluginConfig();
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
            SavePluginConfig();
        }

        public void DeletePlugin(PluginID plugin)
        {
            if (_activePlugins.ContainsKey(plugin))
            {
                DisablePlugin(plugin);
            }
            _installedPlugins.Remove(plugin);
            _pluginStorage.UninstallPlugin(plugin);
            SavePluginConfig();
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
            {
                var components = plugin.Value.GetComponentsOfType(t);
                if (components != null)
                    _gatheredComponents[t].AddRange(components);
            }
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
        public IEnumerable<T> GetComponentsOfType<T>()
        {
            return GetComponentsOfType(typeof(T)).Cast<T>(); //WARN:Looks slow
        }

        private bool SavePluginConfig()
        {
            var json = JsonConvert.SerializeObject(_installedPlugins, new PluginConfigJsonConverter(strategies));
            
            File.WriteAllText(_pluginConfigPath, json);
            return true;
        }

        private Dictionary<PluginID, PluginConfig> LoadPluginConfig()
        {
            if (!Path.Exists(_pluginConfigPath)) return new();

            var json = File.ReadAllText(_pluginConfigPath);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<PluginID, PluginConfig>>(
              json,
              new PluginConfigJsonConverter(strategies)
              );
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


        [Serializable]
        private struct PluginConfigDto
        {
            public PluginStrategy Strategy;
            public bool IsActive;
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
                var dto = new PluginConfigDto { Strategy = value.Strategy, IsActive = value.IsActive };
                serializer.Serialize(writer, dto);
            }

            public override PluginConfig ReadJson(JsonReader reader, Type objectType, PluginConfig existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var dto = serializer.Deserialize<PluginConfigDto>(reader);
                var type = strategies[dto.Strategy];
                var managingStrategy = (IPluginManagingStrategy)Activator.CreateInstance(type);

                return new PluginConfig
                {
                    Strategy = dto.Strategy,
                    IsActive = dto.IsActive,
                    ManagingStrategy = managingStrategy
                };
            }
        }
    }
}
