using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUtilsWPF.Modules.SortSceneryPack
{
    internal static class ScenerySorter
    {
        public static string[] UnsortedLines { get { return _unsorted; } }
        private static string[] _unsorted;

        public static string SortFile(string file)
        {
            List<string> lines = SeparateLines(file);

            List<Scenery> sceneries = new List<Scenery>();

            FindDefaultAndKnownSceneries(lines, sceneries);

            UseTemplates(lines, sceneries);  

            SetDefault(lines, sceneries);

            // check program working
            if (lines.Count > 0)
            {
                throw new Exception("lines not empty");
            }

            return MakeFile(sceneries);
        }

        private static string _template = "SCENERY_PACK Custom Scenery/";

        private static string _fileHeader = @"I
1000 Version
SCENERY

";

        private static List<string> SeparateLines(string file)
        {
            file = file.Remove(0, file.IndexOf("SCENERY_PACK"));

            string[] symbols = { "\r", "\n",};

            List<string> res = file.Split(symbols[0]).ToList();

            for (int i = 1; i < symbols.Length; i++)
            {
                List<string> lines = new List<string>();
                foreach (string s in res)
                {
                    lines.AddRange(s.Split(symbols[i]));
                }
                res = lines;
            }

            while (res.IndexOf("") != -1)
            {
                res.RemoveAt(res.IndexOf(""));
            }

            return res;
        }

        private static void FindDefaultAndKnownSceneries(List<string> lines, List<Scenery> sceneries)
        {
            //string template = "SCENERY_PACK Custom Scenery/";
            string[] defaultAirports = { "Global Airports/" };
            string[] knownLibries = InformationHolder.DefaultLibraries;
            string[] knownMesh = { "KSEA Demo Area/", "LOWI Demo Area/" };

            UseRule(lines, sceneries, defaultAirports.ToList(), SceneryType.Airport, SceneryPriority.Low);

            UseRule(lines, sceneries, knownLibries.ToList(), SceneryType.Library, SceneryPriority.Normal);

            UseRule(lines, sceneries, knownMesh.ToList(), SceneryType.Mesh, SceneryPriority.Low);
        }

        private static void UseTemplates(List<string> lines, List<Scenery> sceneries)
        {
            UseRule(lines, sceneries, InformationHolder.MeshTemplates.ToList(), SceneryType.Mesh, SceneryPriority.Normal);
            UseRule(lines, sceneries, InformationHolder.PhotoTemplates.ToList(), SceneryType.PhotoSubstrate, SceneryPriority.Normal);
            UseRule(lines, sceneries, InformationHolder.OSMTemplates.ToList(), SceneryType.OSM, SceneryPriority.Normal);
        }

        private static void SetDefault(List<string> lines, List<Scenery> sceneries)
        {
            // clone
            _unsorted = new List<string>(lines).ToArray();
            string[] empty = { "" };
            UseRule(lines, sceneries, empty.ToList(), SceneryType.Library, SceneryPriority.Low);
        }

        /// <summary>
        /// Ищет строки в lines еквивалентных ruleLines
        /// </summary>
        private static void UseRule(List<string> lines, List<Scenery> sceneries, List<string> ruleLines, SceneryType type, SceneryPriority priority)
        {
            foreach (string rule in ruleLines)
            {
                int ind = lines.IndexOf(_template + rule);

                while (ind != -1)
                {
                    sceneries.Add(new Scenery(lines[ind], type, priority));
                    lines.RemoveAt(ind);
                    ind = lines.IndexOf(_template + rule);
                }
            }
        } 

        private static string MakeFile(List<Scenery> sceneries)
        {
            sceneries = sceneries.OrderBy(x => x.Line).OrderBy(x => x.Priority).OrderBy(x => x.Type).ToList();

            string resFile = _fileHeader;

            foreach (Scenery scen in sceneries)
            {
                resFile += scen.Line;
            }

            return resFile;
        }
    }
}
