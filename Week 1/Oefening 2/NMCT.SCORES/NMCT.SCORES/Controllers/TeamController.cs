using NMCT.SCORES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NMCT.SCORES.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            CompetitionRepository cr = new CompetitionRepository();
            ViewBag.Competitions = cr.GetCompetitions();
            ViewBag.Show = false;

            return View();
        }
        public ActionResult Select(int? competition)
        {
            if (!competition.HasValue)
                return RedirectToAction("Index");

            CompetitionRepository cr = new CompetitionRepository();
            ViewBag.Competitions = cr.GetCompetitions();

            TeamRepository tr = new TeamRepository();
            ViewBag.Teams = tr.GetTeams(competition.Value);
            ViewBag.Show = true;

            return View("Index");
        }
        public ActionResult View(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            List<Score> s = new List<Score>();
            TeamRepository tr = new TeamRepository();
            s = tr.GetScoreByTeamId(id.Value);

            return View(s);
        }
    }
}