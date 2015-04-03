using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HSStats.Models
{
    public class Arena
    {
        public int ArenaID { get; set; }

        [Required]
        [Display(Name="AS")]
        public Heroes? Hero { get; set; }

        [Range(0,12)]
        public int Wins { get; set; }

        [Range(0,3)]
        public int Defeats { get; set; }

        [Display(Name="Gold earned")]
        public int Gold { get; set; }

        [Display(Name="Dust earned")]
        public int Dust { get; set; }

        [Display(Name = "Arena was started at")]
        public DateTime StartDate { get; set; }

        public string Notes { get; set; }
    }
}