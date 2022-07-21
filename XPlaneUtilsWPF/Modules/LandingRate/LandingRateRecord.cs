using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUtilsWPF.Modules.LandingRate
{
    public class LandingRateRecord : IComparable
    {
        public DateTime DateTime { get; set; }
        public string Aircraft { get; set; }
        public float DescendingSpeed { get; set; }
        public float Overload { get; set; }

        public LandingRateRecord(DateTime dateTime, string aircraft, float descendingSpeed, float overload)        {
            DateTime = dateTime;
            Aircraft = aircraft;
            DescendingSpeed = descendingSpeed;
            Overload = overload;
        }

        public LandingRateRecord(string line)
        {
            string[] parameters = line.Split(',');

            DateTime = DateTime.Parse(parameters[0]);
            Aircraft = parameters[1];
            DescendingSpeed = float.Parse(parameters[2].Replace(".",","));
            Overload = float.Parse(parameters[3].Replace(".", ","));
        }

        public int CompareTo(object? obj)
        {
            int res = 0;

            if (obj != null && obj is LandingRateRecord lrr)
            {
                res = this.DateTime.CompareTo(lrr.DateTime);
            }

            return res;
        }
    }
}
