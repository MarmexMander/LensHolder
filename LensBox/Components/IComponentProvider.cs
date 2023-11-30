using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Components
{
    public interface IComponentProvider
    {
        IDictionary<Type, IEnumerable<object>> Components { get; }
        public bool HasComponentsOfType(Type t);
    }
}
