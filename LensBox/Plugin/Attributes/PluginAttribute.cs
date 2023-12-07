using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        public string Name;
        public string Author;
        public PluginID ID { get => new(Name, Author); }

        public PluginAttribute(string name, string author)
        {
            Name = name;
            Author = author;
        }
    }
}
