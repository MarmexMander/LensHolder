using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LensBox.Plugin
{
    [TypeConverter(typeof(PluginIDTypeConverter))]
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

    public class PluginIDTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var casted = value as string;
            var result = casted != null
                ? PluginID.FromString(casted)
                : base.ConvertFrom(context, culture, value);
            return result;
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var casted = value as PluginID?;
            return destinationType == typeof(string) && casted != null
                ? String.Join("", casted.ToString())
                : base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
