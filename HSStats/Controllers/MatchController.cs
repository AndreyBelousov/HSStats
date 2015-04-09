using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSStats.Models;
using System.Data.Entity.Infrastructure;

namespace HSStats.Controllers
{
    public class MatchController : Controller
    {
        private HSStatsDbContext db = new HSStatsDbContext();

        // GET: /Match/
        public ActionResult Index(Modes? mode, Heroes? myHero, Heroes? opponentHero, Turns? turn, Results? result, int? arenaID)
        {
            var matches = from m in db.Matches select m;

            if(arenaID != null)
            {
                matches = matches.Where(m => m.ArenaID == arenaID);
            }

            if(mode != null)
            {
                matches = matches.Where(m => m.Mode == mode);
            }

            if(myHero != null)
            {
                matches = matches.Where(m => m.MyHero == myHero);
            }
            
            if(opponentHero != null)
            {
                matches = matches.Where(o => o.OpponentHero == opponentHero);
            }

            if(turn != null)
            {
                matches = matches.Where(y => y.Turn == turn);
            }

            if(result != null)
            {
                matches = matches.Where(r => r.Result == result);
            }

            return View(matches.OrderByDescending(m => m.MatchTime));
        }

        // GET: /Match/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: /Match/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Match/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MatchID,Mode,MyHero,OpponentHero,Turn,Result,MatchTime,ArenaID")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.MatchTime = DateTime.Now;
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: /Match/Edit/5
        public ActionResult Edit(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(match);
        }

        // POST: /Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MatchID,Mode,MyHero,OpponentHero,Turn,Result,MatchTime,ArenaID")] Match match, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(match.ArenaID != null)
                {
                    Match original = db.Matches.Find(match.MatchID);
                    if ( match.Result != original.Result )
                    {
                        Arena arena = db.Arenas.Find(match.ArenaID);
                        if ( match.Result == Results.Win && arena.Wins < 12)
                        {
                            arena.Wins = arena.Wins + 1;
                            arena.Defeats = arena.Defeats - 1;
                        }
                        else if ( match.Result == Results.Win && arena.Wins > 11 )
                        {
                            ViewBag.Message = "Number of wins on the single arena can't be more than 12.";
                            return View(match);
                        }

                        if ( match.Result == Results.Defeat && arena.Defeats < 3)
                        {
                            arena.Defeats = arena.Defeats + 1;
                            arena.Wins = arena.Wins - 1;
                        }
                        else if ( match.Result == Results.Defeat && arena.Defeats > 2 )
                        {
                            ViewBag.Message = "Number of defeats on the single arena can't be more than 3.";
                            return View(match);
                        }
                    }
                    ((IObjectContextAdapter)db).ObjectContext.Detach(original);
                }                
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                if ( returnUrl != null )
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: /Match/Delete/5
        public ActionResult Delete(int? id, string returnUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(match);
        }

        // POST: /Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Match match = db.Matches.Find(id);
            if(match.ArenaID != null)
            {
                Arena arena = db.Arenas.Find(match.ArenaID);
                if(match.Result == Results.Win)
                {
                    arena.Wins = arena.Wins - 1;
                }
                else if(match.Result == Results.Defeat)
                {
                    arena.Defeats = arena.Defeats - 1;
                }
            }
            db.Matches.Remove(match);
            db.SaveChanges();
            
            if ( returnUrl != null )
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [ChildActionOnly]
        public ActionResult Search(Match match)
        {
            return PartialView("_Search", match);
        }
    }
}
