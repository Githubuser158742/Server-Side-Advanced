using NMCT.SCORES.Models;
using NMCT.SCORES.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NMCT.SCORES.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Index()
        {
            CompetitionRepository repo = new CompetitionRepository();
            List<Competition> comps = new List<Competition>();
            comps = repo.GetCompetitions();
            return View(comps);
        }

        public ActionResult Details(int id)
        {
            CompetitionRepository repo = new CompetitionRepository();
            Competition comp = new Competition();
            comp = repo.GetCompetitionById(id);
            return View(comp);
        }
        [HttpGet]
        public ActionResult AddScore(int competitionId)
        {
            TeamRepository repot = new TeamRepository();
            AddScoreVM vm = new AddScoreVM();
            vm.Teams = repot.GetTeams(competitionId);
            vm.CompetitionId = competitionId;
            return View(vm);
        }
        [HttpPost]
        public ActionResult AddScore(AddScoreVM vm)
        {
            TeamRepository repot = new TeamRepository();
            Score score = new Score();
            score.ScoreA = vm.ScoreA;
            score.ScoreB = vm.ScoreB;
            score.TeamA = repot.GetTeam(vm.SelectedTeamA);
            score.TeamB = repot.GetTeam(vm.SelectedTeamB);
            score.CompetitionId = vm.CompetitionId;

            CompetitionRepository repoc = new CompetitionRepository();
            repoc.AddScore(score);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Confirm(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Details");

            Score s = new Score();
            CompetitionRepository sr = new CompetitionRepository();
            s = sr.GetScore(id.Value);

            return View(s);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            CompetitionRepository cr = new CompetitionRepository();
            cr.DeleteScore(id.Value);

            return RedirectToAction("Index");
        }

    }
}