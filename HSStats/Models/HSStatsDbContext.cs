using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HSStats.Models
{
    public class HSStatsDbContext: DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<Arena> Arenas { get; set; }
    }
}