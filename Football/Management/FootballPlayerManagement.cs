using Football.Management.Interfaces;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management
{
    public class FootballPlayerManagement:IManagement<FootballPlayers>
    {
        public List<FootballPlayers> players = new List<FootballPlayers>();
        public void Add(FootballPlayers player)
        {
            if (players.Any(p => p.FormNumber == player.FormNumber))
            {
                throw new Exception("This player already exists");
            }
            players.Add(player);
        }
        public FootballPlayers GetPlayers(int formNumber)
        {
            return players.FirstOrDefault(p => p.FormNumber == formNumber);
        }
        public List<FootballPlayers>GetPlayersByTeam(int teamCode)
        {
            return players.Where(p=>p.FormNumber/100==teamCode).ToList();   
        }
        public List<FootballPlayers> GetAll()
        {
            return players;
        }
        public void Update(FootballPlayers updatedplayer)
        {
            var player = GetPlayers(updatedplayer.FormNumber);
            if(player != null)
            {
                player.FullName = updatedplayer.FullName;
                player.GoalsScored = updatedplayer.GoalsScored;
            }
        }
        
    }
}