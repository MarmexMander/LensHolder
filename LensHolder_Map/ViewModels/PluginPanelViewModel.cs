using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using LensBox.Interface;
using LensBox.Plugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.ViewModels
{
    internal class PluginPanelViewModel
    {
        public ObservableCollection<IPlugin> Plugins { get; set; }
        public PluginPanelViewModel() { }
    }
}
