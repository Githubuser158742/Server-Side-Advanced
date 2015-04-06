using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NMCT.SCORES.Models {
    public class Competition {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Score> Scores { get; set; }
        public Country Country { get; set; }

    }
}