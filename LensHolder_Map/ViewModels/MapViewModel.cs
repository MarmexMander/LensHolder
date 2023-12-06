
using LensBox.Components;
using ReactiveUI;
using System.Collections.Generic;

namespace LensHolder_Map.ViewModels;

public class MapViewModel : ReactiveObject
{
    public List<ILens> Lens { get; set; }

    public MapViewModel(List<ILens> lens)
    {
        Lens = lens;
    }
}
