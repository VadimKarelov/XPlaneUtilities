using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUtilsWPF.Modules.LandingRate
{
    internal static class LandingRateHandler
    {
        public static List<LandingRateRecord> GetLog(string file)
        {
            List<LandingRateRecord> res = new List<LandingRateRecord>();

            string[] lines = file.Split('\n');

            foreach (string line in lines)
            {
                string t = line.Replace("\r", "").Replace("\n", "");

                if (!string.IsNullOrEmpty(t))
                    res.Add(new LandingRateRecord(t));
            }

            return res;
        }
    }
}
