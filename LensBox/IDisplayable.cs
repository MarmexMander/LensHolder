using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal interface IDisplayable
    {
        string Name { get; }
        string Description { get; }
        byte[] Icon { get; }
    }
}
