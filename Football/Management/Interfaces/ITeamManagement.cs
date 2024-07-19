using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management.Interfaces
{
    public interface ITeamManagement:IManagement<Models.Team>
    {
        public Team GetTeam(int TeamCode);
        public Team GetTeamByName(string TeamName);

    }
}
