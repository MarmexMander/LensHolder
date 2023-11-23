using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin
{
    public struct PluginID
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        //public string UUID { get; private set; }

        public PluginID(string name, string author)
        {
            Name = name;
            Author = author;
        }

        public static PluginID FromString(string idString)
        {
            string[] parts = idString.Split('@');

            if (parts.Length == 2)
            {
                return new PluginID(parts[0], parts[1]);
            }
            else
            {
                throw new ArgumentException("Invalid PluginID string format");
            }
        }

        public override string ToString()
        {
            return $"{Name}@{Author}";
        }
    }

}
