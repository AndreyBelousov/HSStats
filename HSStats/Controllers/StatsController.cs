using HSStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HSStats.Controllers
{
    public class StatsController : Controller
    {
        private HSStatsDbContext db = new HSStatsDbContext();

        //
        // GET: /Stats/
        public ActionResult Index()
        {
            StatsViewModel statsVM = new StatsViewModel();
            statsVM.TotalMatches = db.Matches.Count();
            statsVM.TotalWins = db.Matches.Where(m => m.Result == Results.Win).Count();
            statsVM.TotalDefeats = db.Matches.Where(m => m.Result == Results.Defeat).Count();
            statsVM.TotalDraws = db.Matches.Where(m => m.Result == Results.Draw).Count();

            statsVM.HeroesMatches = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.HeroesWins = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.HeroesDefeats = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.WinPercentages = new double[Enum.GetNames(typeof(Heroes)).Length];
            
            for ( int i = 0; i < Enum.GetNames(typeof(Heroes)).Length; i++ )
            {
                statsVM.HeroesMatches[i] = db.Matches.Where(m => m.MyHero == (Heroes)i).Count();
                statsVM.HeroesWins[i] = db.Matches.Where(m => m.MyHero == (Heroes)i && m.Result == Results.Win).Count();
                statsVM.HeroesDefeats[i] = db.Matches.Where(m => m.MyHero == (Heroes)i && m.Result == Results.Defeat).Count();
                if ( statsVM.HeroesWins[i] != 0 )
                {
                    statsVM.WinPercentages[i] = Math.Round((double)statsVM.HeroesWins[i] / ( statsVM.HeroesWins[i] + statsVM.HeroesDefeats[i] ) * 100, 2);
                }
            }
            return View(statsVM);
        }
	}
}