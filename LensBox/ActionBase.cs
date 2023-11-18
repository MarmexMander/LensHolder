using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal abstract class ActionBase<T> : IDisplayable
    {
        public string Name { get; private set;}
        public string Description { get; private set; }
        public byte[] Icon { get; private set; }
        public abstract void Execute(T obj);
    }
}
