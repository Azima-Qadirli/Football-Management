using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Models
{
    public class Team
    {
        public int TeamCode { get; set; }
        public string? TeamName { get; set; }
        public int Wins { get; set; }
        public int Equality { get; set; }
        public int Losses { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int Points
        {
            get {  return Wins*3+Equality; }
        }
        public int GoalDifference
        {
            get { return GoalsScored - GoalsConceded; }
        }
    }
}
