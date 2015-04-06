using NMCT.SCORES.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NMCT.SCORES.Models
{
    public class CompetitionRepository
    {
        public List<Competition> GetCompetitions()
        {
            using(ScoreContext context = new ScoreContext())
            {
                var query = (from c in context.Competition.Include(c=>c.Country) select c);
                return query.ToList<Competition>();
            }
        }

        public Competition GetCompetitionById(int id)
        {
            using(ScoreContext context = new ScoreContext())
            {
                var query = (from c in context.Competition.Include(c => c.Country)
                                 .Include(s => s.Scores.Select(t => t.TeamA))
                                 .Include(s => s.Scores.Select(t => t.TeamB))
                             where c.Id == id
                             select c);
                return query.Single<Competition>();
            }
        }
        public void AddScore(Score s)
        {
            using (ScoreContext context = new ScoreContext())
            {
                Score newScore = new Score();
                newScore.CompetitionId = s.CompetitionId;
                newScore.ScoreA = s.ScoreA;
                newScore.ScoreB = s.ScoreB;
                newScore.TeamA = s.TeamA;
                newScore.TeamB = s.TeamB;

                //context.Score.Add(newScore);
                context.Entry<Team>(newScore.TeamA).State = EntityState.Unchanged;
                context.Entry<Team>(newScore.TeamB).State = EntityState.Unchanged;
                context.Entry(newScore).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public Score GetScore(int id)
        {
            using (ScoreContext context = new ScoreContext())
            {
                var query = (from s in context.Score.Include(o => o.TeamA)
                                                    .Include(o => o.TeamB)
                             where s.Id == id
                             select s);
                return query.Single<Score>();
            }
        }

        public void DeleteScore(int id)
        {
            using (ScoreContext context = new ScoreContext())
            {
                Score score = new Score();
                score = GetScore(id);

                try
                {
                    //context.Score.Remove(score);
                    context.Entry(score).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}