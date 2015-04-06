using NMCT.SCORES.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NMCT.SCORES.Models
{
    public class TeamRepository
    {
        public List<Team> GetTeams(int id)
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from t in context.Team
                             where t.Competition.Id == id
                             select t);
                return query.ToList<Team>();
            }
        }

        public Team GetTeam(int selectedTeam)
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from t in context.Team
                             where t.Id == selectedTeam
                             select t);
                return query.Single<Team>();
            }
        }

        public List<Score> GetScoreByTeamId(int id)
        {
            using (ScoreContext context = new ScoreContext())
            {
                Team t = new Team();
                t = GetTeam(id);

                var query = (from s in context.Score.Include(o => o.TeamA)
                                                    .Include(o => o.TeamB)
                             where s.TeamA.Id == t.Id || s.TeamB.Id == t.Id
                             select s);
                return query.ToList<Score>();
            }
        }
    }
}