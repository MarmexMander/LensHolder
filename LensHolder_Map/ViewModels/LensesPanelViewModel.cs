﻿using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using LensBox.Components;
using LensBox.Interface;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensHolder_Map.ViewModels
{
    public class LensesPanelViewModel : ReactiveObject
    {
        public ObservableCollection<ILens> Lenses { get; set; }
        public LensesPanelViewModel( IEnumerable<ILens> lens) 
        {
            Lenses = new ObservableCollection<ILens>(lens);
        }
    }
}
