using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Models
{
    public class Games
    {
        public int WeekNumber { get; set; }
        public int HomeTeamCode { get; set; }
        public int GuestTeamCode { get; set; }
        public int HomeGoals { get; set; }
        public int GuestGoals { get; set; }
    }
}
