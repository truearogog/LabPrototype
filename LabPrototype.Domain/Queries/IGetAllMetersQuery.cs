using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Domain.Queries
{
    public interface IGetAllMetersQuery
    {
        Task<IEnumerable<Meter>> Execute();
    }
}
