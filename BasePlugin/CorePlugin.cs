using LensBox.Components;
using LensBox.Plugin;
using LensBox.Plugin.Attributes;
using System.Collections.ObjectModel;

namespace CorePlugin
{
    [Displayable(
        "Core Plugin",
        "Core demo plugin"
        )]
    [Plugin("Core", "LensHolder")]
    public class CorePlugin : PluginBase
    {
        public override ReadOnlyDictionary<PluginID, DependencyType> Dependencies { get; } 
            = new(new Dictionary<PluginID, DependencyType>());

        public override void Cleanup()
        {
            return;
        }

        public override void Disable()
        {
            return;
        }

        public override void Enable()
        {
            return;
        }

        public override void Init()
        {
            return;
        }

        [Component]
        private IBaseMap OpenStreetBase()
        {
            return new GenericBaseMap(
                "OpenStreet map",
                "Generic open street map tile provider",
                Resources.Logo_OSM,
                () => {
                    return Mapsui.Tiling.OpenStreetMap.CreateTileLayer(); 
                }
                );
        }
    }
}