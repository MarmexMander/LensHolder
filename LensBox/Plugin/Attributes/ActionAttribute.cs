using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin.Attributes
{
    internal class ActionAttribute : DisplayableAttributeBase
    {
        public ActionAttribute(string name, string? description = null, byte[]? icon = null) : base(name, description, icon)
        {

        }
    }
}
