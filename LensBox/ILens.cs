using Mapsui.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal interface ILens
    {
        public ILayer GetLayer();
    }
}
