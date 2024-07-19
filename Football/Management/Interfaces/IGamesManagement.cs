using Football.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management.Interfaces
{
    public interface IGamesManagement:IManagement<Models.Games>
    {
        public List<Games> GetGamesByWeek(int weekNumber);

    }
}
