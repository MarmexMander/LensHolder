using Mapsui;
using Mapsui.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal interface ILandmark
    {
        public IFeature MapFeature { get; }
    }
}
