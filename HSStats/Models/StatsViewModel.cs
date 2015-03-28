using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSStats.Models
{
    public class StatsViewModel
    {
        public int TotalMatches { get; set; }
        public int TotalWins { get; set; }
        public int TotalDefeats { get; set; }
        public int TotalDraws { get; set; }

        [Display(Name = "Matches")]
        public int[] HeroesMatches { get; set; }

        [Display(Name="Hero")]
        public Heroes Heroes { get; set; }

        [Display(Name="Wins")]
        public int[] HeroesWins { get; set; }

        [Display(Name="Defeats")]
        public int[] HeroesDefeats { get; set; }

        [Display(Name="Win %")]
        public double[] WinPercentages { get; set; }
    }
}