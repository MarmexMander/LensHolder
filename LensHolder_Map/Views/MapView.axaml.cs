using Avalonia.Controls;
using Mapsui;
using Mapsui.Extensions;
using Avalonia.Input;
using System;

namespace LensHolder_Map.Views;

public partial class MapView : UserControl
{
    public MapView()
    {
        
        InitializeComponent();
        
        
        map.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        map.AddHandler(Gestures.PinchEvent, (s, e) =>
        {
            
            map.Map?.Navigator.MouseWheelZoom(Convert.ToInt32(e.Scale), new MPoint(e.ScaleOrigin.X, e.ScaleOrigin.Y));
            e.Handled = true;
        });
                
        //
        
        //map.Widgets.Add(new ScaleBarWidget(map) { TextAlignment = Alignment.Center, HorizontalAlignment = Mapsui.Widgets.HorizontalAlignment.Center, VerticalAlignment = Mapsui.Widgets.VerticalAlignment.Top });
        //map.Widgets.Add(new ZoomInOutWidget { MarginX = 20, MarginY = 40 });
        
    }
}
