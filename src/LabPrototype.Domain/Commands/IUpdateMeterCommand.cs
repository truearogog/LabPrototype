using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Domain.Commands
{
    public interface IUpdateMeterCommand
    {
        Task Execute(Meter meter);
    }
}
