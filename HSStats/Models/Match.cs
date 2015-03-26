using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HSStats.Models
{
    public class Match
    {
        public int MatchID { get; set; }

        [Required]        
        public Modes? Mode { get; set; }

        [Required]
        [Display(Name = "As")]
        public Heroes? MyHero { get; set; }

        [Required]
        [Display(Name = "Vs")]
        public Heroes? OpponentHero { get; set; }

        [Required]
        [Display(Name="Your turn")]
        public Turns? Turn { get; set; }

        [Required]
        public Results? Result { get; set; }

        [Display(Name = "Time of the match")]        
        public DateTime MatchTime { get; set; }
    }

    public class HSStatsDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
    }

    public enum Heroes : byte { Druid, Hunter, Mage, Paladin, Priest, Rogue, Shaman, Warlock, Warrior };

    public enum Results : byte { Win, Defeat, Draw, Unknown };

    public enum Modes: byte { Ranked, Arena, Unranked }

    public enum Turns { Unknown, [Display(Name = "Go first")] GoFirst, [Display(Name = "Go second")] GoSecond }
}