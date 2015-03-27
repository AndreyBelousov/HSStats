﻿using System;
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
    public class MatchController : Controller
    {
        private HSStatsDbContext db = new HSStatsDbContext();

        // GET: /Match/
        public ActionResult Index(Modes? mode, Heroes? myHero, Heroes? opponentHero, Turns? turn, Results? result)
        {
            var matches = from m in db.Matches select m;

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

            return View(matches);
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
        public ActionResult Create([Bind(Include="MatchID,Mode,MyHero,OpponentHero,Turn,Result,MatchTime")] Match match)
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
        public ActionResult Edit(int? id)
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

        // POST: /Match/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MatchID,Mode,MyHero,OpponentHero,Turn,Result,MatchTime")] Match match)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: /Match/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: /Match/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
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