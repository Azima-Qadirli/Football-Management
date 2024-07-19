using Football.Management.Interfaces;
using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management
{
    public class TeamManagement:IManagement<Team>
    {
        private List<Team>teams=new List<Team>();

        public void Add(Team team)
        {
            if (teams.Any(t => t.TeamCode == team.TeamCode))
            {
                throw new Exception("Dear user, this team with code already exist.");
            }
            teams.Add(team);
        }
        public Team GetTeam(int TeamCode)
        {
            return teams.FirstOrDefault(t=>t.TeamCode==TeamCode);
        }
        public Team GetTeamByName(string TeamName)
        {
            return teams.FirstOrDefault(t => t.TeamName == TeamName);
        }
        public List<Team> GetAll()
        { 
            return teams;
        }
    }
}
