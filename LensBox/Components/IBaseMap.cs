using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LensBox.Interface;

namespace LensBox.Components
{
    public interface IBaseMap : IDisplayable
    {
        Mapsui.Tiling.Layers.TileLayer GetTileLayer();
    }
}
