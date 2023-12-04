using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Components
{
    public interface IComponentProvider
    {
        IEnumerable<object> GetComponentsOfType(Type t);
        public bool HasComponentsOfType(Type t);
    }
}
