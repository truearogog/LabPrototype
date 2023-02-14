using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Interfaces
{
    public interface IMeterService
    {
        IEnumerable<Meter> Meters { get; }
        Task Load();
        Task Create(Meter meter);
        Task Update(Meter meter);
        Task Delete(Guid id);

        void SubscribeMetersLoaded(Action handler);
        void UnsubscribeMetersLoaded(Action handler);
        void SubscribeMeterCreated(Action<Meter> handler);
        void UnsubscribeMeterCreated(Action<Meter> handler);
        void SubscribeMeterUpdated(Action<Meter> handler);
        void UnsubscribeMetersLoaded(Action<Meter> handler);
        void SubscribeMeterDeleted(Action<Guid> handler);
        void UnsubscribeMeterDeleted(Action<Guid> handler);
    }
}
