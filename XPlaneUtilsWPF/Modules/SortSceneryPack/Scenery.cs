namespace XPlaneUtilsWPF.Modules.SortSceneryPack
{
    internal class Scenery
    {
        public string Line { get; set; }
        public SceneryType Type { get; set; }
        public SceneryPriority Priority { get; set; }

        public Scenery(string line, SceneryType type, SceneryPriority priority)
        {
            Line = line;
            Type = type;
            Priority = priority;
        }
    }

    internal enum SceneryType
    {
        Airport,
        OSM,
        Library,
        PhotoSubstrate,
        Mesh
    }

    internal enum SceneryPriority
    {
        Normal,
        Low
    }
}
