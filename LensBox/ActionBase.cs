using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox
{
    internal abstract class ActionBase<T>
    {
        public readonly string Name;
        public readonly Assets.AssetID Icon;
        public abstract void Execute(T obj);

    }
}
