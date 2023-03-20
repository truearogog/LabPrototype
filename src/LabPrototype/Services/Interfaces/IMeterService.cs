using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeterService
    {
        event Action MetersLoaded;
        event Action<Meter> MeterCreated;
        event Action<Meter> MeterUpdated;
        event Action<Guid> MeterDeleted;

        IEnumerable<Meter> Meters { get; }
        
        Task Load();
        Task Create(Meter meter);
        Task Update(Meter meter);
        Task Delete(Guid id);
    }
}
