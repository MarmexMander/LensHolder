using Mapsui.Tiling.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Components
{
    //TODO: Make somehow possible too not add IDisplayable
    //      values in constructor, but piuck it from plugin
    //      generator-method for components from DisplayableAttribute
    public class GenericBaseMap : IBaseMap
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public byte[] Icon { get; private set; }

        Func<TileLayer> _getTileLayer;

        public GenericBaseMap(string name, string description, byte[] icon, Func<TileLayer> getTileLayer)
        {
            Name = name;
            Description = description;
            Icon = icon;
            _getTileLayer = getTileLayer;
        }

        public TileLayer GetTileLayer()
        {
            return _getTileLayer();
        }
    }
}
