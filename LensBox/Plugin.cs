namespace LensBox.Core
{
    public enum DependencyType
    {
        Required,
        Synergy
    }
    public class Plugin
    {
        Version Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, DependencyType> Dependencies { get;}
    }
}