using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin.Attributes
{

    [AttributeUsage(AttributeTargets.Method)]
    internal class DisplayableAttributeBase : Attribute
    {
        public string Name { get; }
        public string? Description { get; }
        public byte[]? Icon { get; }

        public DisplayableAttributeBase(string name, string? description = null, byte[]? icon = null)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}
