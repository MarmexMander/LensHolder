using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using LensBox.Interface;
using LensBox.Plugin;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.ViewModels
{
    public class PluginPanelViewModel : ReactiveObject
    {
        public ObservableCollection<IPlugin> Plugins { get; set; }
        public PluginPanelViewModel(IEnumerable<IPlugin> plugins) 
        {
            Plugins = new(plugins);
        }
    }
}
