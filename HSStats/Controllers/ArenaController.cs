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
                Arena arena = db.Arenas.Find(match.ArenaID);
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

        public ActionResult AddRewards(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arena arena = db.Arenas.Find(id);
            if ( arena == null )
            {
                return HttpNotFound();
            }
            return View(arena);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRewards(Arena arena)
        {
            if(ModelState.IsValid)
            {
                db.Entry(arena).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arena);
        }

        public ActionResult Delete(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arena arena = db.Arenas.Find(id);            
            if ( arena == null )
            {
                return HttpNotFound();
            }
            return View(arena);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arena arena = db.Arenas.Find(id);            
            db.Arenas.Remove(arena);
            foreach (var match in db.Matches.Where(m=>m.ArenaID == id).Select(m => m))
            {
                db.Matches.Remove(match);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arena arena = db.Arenas.Find(id);            
            if ( arena == null )
            {
                return HttpNotFound();
            }
            return View(arena);
        }

        [ChildActionOnly]
        public ActionResult ListOfMatches(int? id)
        {
            if ( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var matches = db.Matches.Where(m => m.ArenaID == id).Select(m => m);
            if(matches == null)
            {
                return HttpNotFound();
            }
            return PartialView("_ListOfMatches", matches);
        }

	}
}