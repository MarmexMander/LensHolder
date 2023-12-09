using Avalonia.Controls;
using Mapsui;
using Mapsui.Extensions;
using Avalonia.Input;
using System;
using LensHolder_Map.ViewModels;
using System.ComponentModel;
using LensBox.Components;
using Avalonia.Interactivity;

namespace LensHolder_Map.Views;

public partial class MapView : UserControl
{
    MapViewModel _viewModel;
    public MapView()
    {
        Loaded += Init;
        InitializeComponent();
        
        //map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
        //map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
    }
    private void Init(object? sender, RoutedEventArgs e)
    {
        _viewModel = DataContext as MapViewModel;
        if (_viewModel == null) return;
        _viewModel.PropertyChanged += ReloadLenses;
        //map.AddHandler(Gestures.PinchEvent, (s, e) =>
        //{
        //    map.Map?.Navigator.MouseWheelZoom(Convert.ToInt32(e.Scale), new MPoint(e.ScaleOrigin.X, e.ScaleOrigin.Y));
        //    e.Handled = true;
        //});
        ReloadLenses();
    }

    private void ReloadLenses(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != "Lenses") return;
        ReloadLenses();
    }
    private void ReloadLenses()
    {
        map.Map?.Layers.Clear();
        if (_viewModel.BaseMap == null) return;
        map.Map?.Layers.Add(_viewModel.BaseMap.GetTileLayer());
        _viewModel.Lens?.ForEach(lens =>
        {
            map.Map?.Layers.Add(lens.GetLayer());
        });
        map.ForceUpdate();
    }
}
