using Football.Management.Interfaces;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management
{
    public class GameManagement:IManagement<Games>
    {
        private List<Games>games = new List<Games>();

        public void Add(Games game)
        {
            games.Add(game);
        }
        public List<Games> GetGamesByWeek(int weekNumber)
        {
            return games.FindAll(g=> g.WeekNumber == weekNumber);
        }
        public List<Games> GetAll()
        {
            return games;
        }

        
    }
}
