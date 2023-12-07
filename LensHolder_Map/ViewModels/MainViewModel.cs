using LensBox.Plugin;
using LensBox.Components;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        public List<IPlugin> Plugins { get; set; }
        public List<ILens> Lenses { get; set; }
        public IBaseMap BaseMap { get; set; }

        public MapViewModel MapVM { get; set; }
        public LensesPanelViewModel LensesPanelVM { get; set; }
        public PluginPanelViewModel PluginsPanelVM { get; set; }
        public MainViewModel() 
        {
            App app = App.Current as App;
            Plugins = app.PluginsManager.Plugins.ToList();
            Lenses = app.PluginsManager.GetComponentsOfType<ILens>().ToList();
            var baseMaps = app.PluginsManager.GetComponentsOfType<IBaseMap>();
            if(baseMaps.Count() > 0)
                BaseMap = baseMaps.First(); //TODO: change to retriving basemap from settings

            MapVM = new(Lenses, BaseMap);
            LensesPanelVM = new(Lenses);
            PluginsPanelVM = new(Plugins);
        }
    }
}

