using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSStats.Models;

namespace HSStats.Controllers
{
    public class ArenaController : Controller
    {
        private HSStatsDbContext db = new HSStatsDbContext();
        //
        // GET: /Arena/
        public ActionResult Index()
        {
            var arenas = from a in db.Arenas select a;
            return View(arenas);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Arena arena)
        {
            if(ModelState.IsValid)
            {
                arena.StartDate = DateTime.Now;
                db.Arenas.Add(arena);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arena);
        }

        public ActionResult AddMatch(int id, Heroes myHero)
        {
            Match match = new Match() {  ArenaID = id, Mode = Modes.Arena, MyHero = myHero };        
            return View(match);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMatch(Match match)
        {
            if(ModelState.IsValid)
            {
                var arena = db.Arenas.Where(m => m.ArenaID == match.ArenaID).Select(m => m).Single();
                if ( match.Result == Results.Win )
                {                    
                    arena.Wins = arena.Wins+1;
                }
                if ( match.Result == Results.Defeat )
                {
                    arena.Defeats = arena.Defeats+1;
                }
                match.MatchTime = DateTime.Now;
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }
	}
}