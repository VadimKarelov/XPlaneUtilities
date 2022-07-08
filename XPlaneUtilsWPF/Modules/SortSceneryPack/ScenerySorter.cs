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
            
            FindAirportsByCode(lines, sceneries);

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

        private static void FindAirportsByCode(List<string> lines, List<Scenery> sceneries)
        {
            List<string> rules = new List<string>();

            for (char c1 = 'A'; c1 <= 'Z'; c1++)
                for (char c2 = 'A'; c2 <= 'Z'; c2++)
                    for (char c3 = 'A'; c3 <= 'Z'; c3++)
                        for (char c4 = 'A'; c4 <= 'Z'; c4++)
                            rules.Add($"{c1}{c2}{c3}{c4}");

            UseRule(lines, sceneries, rules, SceneryType.Airport, SceneryPriority.Normal);
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
                //int ind = lines.IndexOf(_template + rule);
                int ind = FindLineWithTemplate(lines, rule);

                while (ind != -1)
                {
                    sceneries.Add(new Scenery(lines[ind], type, priority));
                    lines.RemoveAt(ind);
                    //ind = lines.IndexOf(_template + rule);
                    ind = FindLineWithTemplate(lines, rule);
                }
            }
        }

        private static int FindLineWithTemplate(List<string> lines, string rule)
        {
            int ind = lines.IndexOf(_template + rule);

            if (ind == -1)
            {
                // нужен для определения не только полностью совпадающих строк, но и содержащих шаблон из ruleLines
                foreach (string line in lines)
                {
                    if (line.IndexOf(rule) != -1)
                    {
                        ind = lines.IndexOf(line);
                    }
                }
            }

            return ind;
        }

        /// <summary>
        /// Формирует окончательный файл
        /// </summary>
        private static string MakeFile(List<Scenery> sceneries)
        {
            sceneries = sceneries.OrderBy(x => x.Line).ToList();
            sceneries = sceneries.OrderBy(x => x.Priority).ToList();
            sceneries = sceneries.OrderBy(x => x.Type).ToList();

            string resFile = _fileHeader;

            foreach (Scenery scen in sceneries)
            {
                resFile += scen.Line + "\n";
            }

            return resFile;
        }
    }
}
