using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management.Interfaces
{
    public interface IFootballPlayerManagement:IManagement<Models.FootballPlayers>
    {
        public List<FootballPlayers> GetPlayersByTeam(int teamCode);
        public FootballPlayers GetPlayers(int formNumber);
    }
}
