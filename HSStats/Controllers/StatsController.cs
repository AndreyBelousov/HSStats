﻿using HSStats.Models;
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
        public ActionResult Index(Modes? mode, Heroes? myHero, Turns? turn)
        {
            StatsViewModel statsVM = new StatsViewModel();
            
            statsVM.HeroesMatches = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.HeroesWins = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.HeroesDefeats = new int[Enum.GetNames(typeof(Heroes)).Length];
            statsVM.WinPercentages = new double[Enum.GetNames(typeof(Heroes)).Length];
            List<Match> matches = db.Matches.ToList();

            if ( mode != null )
            {
                matches = matches.Where(m => m.Mode == mode).Select(m => m).ToList();
            }

            if (turn != null)
            {
                matches = matches.Where(m => m.Turn == turn).Select(m => m).ToList();
            }

            if (myHero != null)
            {
                matches = matches.Where(m => m.MyHero == myHero).Select(m => m).ToList();

                for ( int i = 0; i < Enum.GetNames(typeof(Heroes)).Length; i++ )
                {
                    statsVM.HeroesMatches[i] = matches.Where(m => m.OpponentHero == (Heroes)i).Count();
                    statsVM.HeroesWins[i] = matches.Where(m => m.OpponentHero == (Heroes)i && m.Result == Results.Win).Count();
                    statsVM.HeroesDefeats[i] = matches.Where(m => m.OpponentHero == (Heroes)i && m.Result == Results.Defeat).Count();
                    if ( statsVM.HeroesWins[i] != 0 )
                    {
                        statsVM.WinPercentages[i] = Math.Round((double)statsVM.HeroesWins[i] / ( statsVM.HeroesWins[i] + statsVM.HeroesDefeats[i] ) * 100, 2);
                    }
                }
            }
            else
            {
                for ( int i = 0; i < Enum.GetNames(typeof(Heroes)).Length; i++ )
                {
                    statsVM.HeroesMatches[i] = matches.Where(m => m.MyHero == (Heroes)i).Count();
                    statsVM.HeroesWins[i] = matches.Where(m => m.MyHero == (Heroes)i && m.Result == Results.Win).Count();
                    statsVM.HeroesDefeats[i] = matches.Where(m => m.MyHero == (Heroes)i && m.Result == Results.Defeat).Count();
                    if ( statsVM.HeroesWins[i] != 0 )
                    {
                        statsVM.WinPercentages[i] = Math.Round((double)statsVM.HeroesWins[i] / ( statsVM.HeroesWins[i] + statsVM.HeroesDefeats[i] ) * 100, 2);
                    }
                }
            }            

            return View(statsVM);
        }

        [ChildActionOnly]
        public ActionResult Filter(Match match)
        {
            return PartialView("_Filter", match);
        }

        [ChildActionOnly]
        public ActionResult ShortStats()
        {
            ShortStatsViewModel shortStatsVM = new ShortStatsViewModel();

            shortStatsVM.MatchesByMode = new int[Enum.GetNames(typeof(Modes)).Length+1];
            shortStatsVM.WinsByMode = new int[Enum.GetNames(typeof(Modes)).Length+1];
            shortStatsVM.DefeatsByMode = new int[Enum.GetNames(typeof(Modes)).Length+1];
            shortStatsVM.WinPercentageByMode = new double[Enum.GetNames(typeof(Modes)).Length+1];

            shortStatsVM.MatchesByMode[0] = db.Matches.Count();
            shortStatsVM.WinsByMode[0] = db.Matches.Where(m => m.Result == Results.Win).Count();
            shortStatsVM.DefeatsByMode[0] = db.Matches.Where(m => m.Result == Results.Defeat).Count();
            shortStatsVM.WinPercentageByMode[0] = Math.Round((double)shortStatsVM.WinsByMode[0] / ( shortStatsVM.WinsByMode[0] + shortStatsVM.DefeatsByMode[0] ) * 100, 2);

            for ( int i = 1; i < Enum.GetNames(typeof(Modes)).Length+1; i++ )
            {
                shortStatsVM.MatchesByMode[i] = db.Matches.Where(m => m.Mode == (Modes)i-1).Count();
                shortStatsVM.WinsByMode[i] = db.Matches.Where(m => m.Mode == (Modes)i-1 && m.Result == Results.Win).Count();
                shortStatsVM.DefeatsByMode[i] = db.Matches.Where(m => m.Mode == (Modes)i-1 && m.Result == Results.Defeat).Count();
                if(shortStatsVM.WinsByMode[i] != 0)
                {
                    shortStatsVM.WinPercentageByMode[i] = Math.Round((double)shortStatsVM.WinsByMode[i] / ( shortStatsVM.WinsByMode[i] + shortStatsVM.DefeatsByMode[i] ) * 100, 2);
                }
            }
            
            return PartialView("_ShortStats", shortStatsVM);
        }
	}
}