using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUtilsWPF.Modules.SortSceneryPack
{
    internal static class ScenerySorter
    {
        public static string SortFile(string file)
        {
            List<string> lines = SeparateLines(file);

            List<Scenery> sceneries = new List<Scenery>();

            FindDefaultAndKnownSceneries(lines, sceneries);


        }

        private static List<string> SeparateLines(string file)
        {
            file = file.Remove(file.IndexOf(_fileHeader), _fileHeader.Length);

            string[] symbols = { "\r", "\n" };

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
            string template = "SCENERY_PACK Custom Scenery/";
            string[] defaultAirports = { "Global Airports/"};
            string[] knownLibries = _defaultLibraries;
            string[] knownMesh = { "KSEA Demo Area/", "LOWI Demo Area/" };

            foreach (string dAirport in defaultAirports)
            {
                int ind = lines.IndexOf(template + dAirport);

                while (ind != -1)
                {
                    sceneries.Add(new Scenery(lines[ind], SceneryType.Airport, SceneryPriority.Low));
                    lines.RemoveAt(ind);
                    ind = lines.IndexOf(template + dAirport);
                }
            }

            foreach (string lib in knownLibries)
            {
                int ind = lines.IndexOf(template + lib);

                while (ind != -1)
                {
                    sceneries.Add(new Scenery(lines[ind], SceneryType.Library, SceneryPriority.Normal));
                    lines.RemoveAt(ind);
                    ind = lines.IndexOf(template + lib);
                }
            }

            foreach (string mesh in knownMesh)
            {
                int ind = lines.IndexOf(template + mesh);

                while (ind != -1)
                {
                    sceneries.Add(new Scenery(lines[ind], SceneryType.Mesh, SceneryPriority.Low));
                    lines.RemoveAt(ind);
                    ind = lines.IndexOf(template + mesh);
                }
            }
        }

        private static string _fileHeader = @"I
1000 Version
SCENERY

";

        private static string[] _defaultLibraries = {"000_Madagascar_Lib/",
"3D_people_library/",
"ALES_DEV_LIB/",
"AR_Library/",
"BS2001 Object Library/",
"CCVA022_Object_Library/",
"CDB-Library/",
"cemetery/",
"CFXP - Static Aircraft Library/",
"Europe_Library/",
"Europe_Library_HD/",
"Europe_RoadTraffic/",
"european_vehicles_library_uwespeed/",
"ff_library/",
"ff_library_extended_LOD/",
"FJS_Scenery_Library_v1.7/",
"flags_of_the_world/",
"flags_of_USA_states/",
"FlyAgi_Vegetation/",
"Flyby_Planes/",
"forest/",
"german_traffic_library/",
"gt_library/",
"ISDGLibrary/",
"JB_Library/",
"mada_bush_airfields/",
"Madagascar_Forests/",
"MisterX_Library/",
"NAPS_library/",
"OpenSceneryX/",
"Orbx_OrbxlibsXP/",
"People_LIB/",
"pm_library/",
"PPlibrary/",
"PuF_Libs/",
"R2_Library/",
"RA_Library/",
"RD_Library/",
"RE_Library/",
"RescueX_Lib/",
"RescueX_Terrain/",
"ruscenery/",
"SAM_Library/",
"Sea_Life/",
"Serviced Aircraft Europe A320/",
"Serviced Aircraft Europe A340/",
"Serviced Aircraft Europe B737/",
"Serviced Aircraft Europe B747/",
"Serviced Aircraft North America Part 1/",
"Serviced Aircraft North America Part 2/",
"Serviced Aircraft North America Part 3/",
"Serviced Aircraft World Part 1/",
"Shoreline_Objects/",
"Static_GA_Aircraft_Australia/",
"Static_GA_Aircraft_NZ/",
"Switchable Serviced Aircraft  A380/",
"Switchable Serviced Aircraft Europe A320/",
"Switchable Serviced Aircraft Europe B737/",
"Switchable Serviced Aircraft Europe B747/",
"Switchable Serviced Aircraft Europe Small/",
"Switchable Serviced Aircraft North America Part 1/",
"Switchable Serviced Aircraft North America Part 2/",
"Switchable Serviced Aircraft North America Part 3/",
"Switchable Serviced Aircraft World Part 1/",
"TerraFloraXP/",
"THE-FAIB Aircraft Library/",
"THE-FRUIT-STAND Aircraft Library v3.0/",
"The_Handy_Objects_Library/",
"TM Library - Billboards HD vol 1 XP11/",
"Vehicle Library Extension/",
"Waves_Library/",
"world-models/",
"Wrecked_Vehicles/",
"XAirportScenery/",
"XS_Library/"};

        private static string[] _meshTemplates = {"z_",
            "zz_",
            "zzz_",
            "Z_",
            "ZZ_",
            "ZZZ_"};

        private static string[] _photoTemplates = {"Ortho",
        "ortho",
        "photo",
        "Photo",
        "PHOTO"};

        private static string[] _osmTemplates = {"Landmarks",
        "osm"};
    }
}
