using NMCT.SCORES.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NMCT.SCORES.ViewModels
{
    public class AddScoreVM
    {
        public List<Team> Teams { get; set; }
        public int SelectedTeamA { get; set; }
        [Required]
        public int ScoreA { get; set; }
        public int SelectedTeamB { get; set; }
        [Required]
        public int ScoreB { get; set; }
        public int CompetitionId { get; set; }    
    }
}