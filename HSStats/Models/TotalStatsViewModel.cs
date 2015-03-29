using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSStats.Models
{
    public class ShortStatsViewModel
    {
        public int[] MatchesByMode { get; set; }
        public int[] WinsByMode { get; set; }
        public int[] DefeatsByMode { get; set; }
        public double[] WinPercentageByMode { get; set; }
        public Modes Modes { get; set; }
    }
}