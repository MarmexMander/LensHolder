﻿using LensBox.Interface;
using Mapsui;
using Mapsui.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Components
{
    public interface ILandmark : IDisplayable
    {
        public IFeature MapFeature { get; }
    }
}
