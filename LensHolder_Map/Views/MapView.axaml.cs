using Avalonia.Controls;
using Mapsui;
using Mapsui.Extensions;
using Avalonia.Input;
using System;
using LensHolder_Map.ViewModels;
using System.ComponentModel;

namespace LensHolder_Map.Views;

public partial class MapView : UserControl
{
    MapViewModel _viewModel;
    public MapView()
    {
        
        InitializeComponent();
        _viewModel = DataContext as MapViewModel;
        if (_viewModel == null) return;
        _viewModel.PropertyChanged += ReloadLenses;
        map.AddHandler(Gestures.PinchEvent, (s, e) =>
        {
            
            map.Map?.Navigator.MouseWheelZoom(Convert.ToInt32(e.Scale), new MPoint(e.ScaleOrigin.X, e.ScaleOrigin.Y));
            e.Handled = true;
        });
        //map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
        //map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
    }

    private void ReloadLenses(object? sender, PropertyChangedEventArgs e)
    {
        map.Map?.Layers.Clear();
        _viewModel.Lens.ForEach(lens =>
        {
            map.Map?.Layers.Add(lens.GetLayer());
        });
    }
}
