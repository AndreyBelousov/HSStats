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
            ViewBag.TotalWins = db.Matches.Where(m => m.Result == Results.Win).Count();
            ViewBag.TotalDefeats = db.Matches.Where(m => m.Result == Results.Defeat).Count();
            ViewBag.TotalDraws = db.Matches.Where(m => m.Result == Results.Draw).Count();
            return View();
        }
	}
}