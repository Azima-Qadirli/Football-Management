using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football.Management.Interfaces
{
    public interface IManagement<T>where T : class
    {
        void Add(T entity);
        List<T>GetAll();
    }
}
