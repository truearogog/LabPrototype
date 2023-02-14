using LabPrototype.Domain.Commands;
using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Implementations
{
    public class MeterService : IMeterService
    {
        private readonly IGetAllMetersQuery _getAllMetersQuery;
        private readonly ICreateMeterCommand _createMeterCommand;
        private readonly IUpdateMeterCommand _updateMeterCommand;
        private readonly IDeleteMeterCommand _deleteMeterCommand;

        private readonly List<Meter> _meters;
        public IEnumerable<Meter> Meters => _meters;

        private event Action _metersLoaded;
        private event Action<Meter> _meterCreated;
        private event Action<Meter> _meterUpdated;
        private event Action<Guid> _meterDeleted;

        public MeterService(
            IGetAllMetersQuery getAllMetersQuery, 
            ICreateMeterCommand createMeterCommand, 
            IUpdateMeterCommand updateMeterCommand, 
            IDeleteMeterCommand deleteMeterCommand)
        {
            _getAllMetersQuery = getAllMetersQuery;
            _createMeterCommand = createMeterCommand;
            _updateMeterCommand = updateMeterCommand;
            _deleteMeterCommand = deleteMeterCommand;

            _meters = new List<Meter>();
        }

        public async Task Load()
        {
            _meters.Clear();
            _meters.AddRange(new List<Meter>()
            {
                new Meter(Guid.NewGuid(), "A123", "Meter1", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter2", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter3", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter4", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter5", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter6", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter7", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter8", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter9", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter10", "Address"),
                new Meter(Guid.NewGuid(), "A123", "Meter11", "Address"),
            });

            _metersLoaded?.Invoke();
        }
        
        /*
        public async Task Load()
        {
            IEnumerable<Meter> meters = await _getAllMetersQuery.Execute();

            _meters.Clear();
            _meters.AddRange(meters);

            MetersLoaded?.Invoke();
        }
        */

        public async Task Create(Meter meter)
        {
            await _createMeterCommand.Execute(meter);
            _meters.Add(meter);
            _meterCreated?.Invoke(meter);
        }

        public async Task Update(Meter meter)
        {
            await _updateMeterCommand.Execute(meter);
            int currentIndex = _meters.FindIndex(x => x.Id.Equals(meter.Id));
            if (currentIndex != -1)
            {
                _meters[currentIndex] = meter;
            }
            else
            {
                _meters.Add(meter);
            }
            _meterUpdated?.Invoke(meter);
        }

        public async Task Delete(Guid id)
        {
            await _deleteMeterCommand.Execute(id);
            _meters.RemoveAll(x => x.Id.Equals(id));
            _meterDeleted?.Invoke(id);
        }

        public void SubscribeMetersLoaded(Action handler) => _metersLoaded += handler;
        public void UnsubscribeMetersLoaded(Action handler) => _metersLoaded -= handler;
        public void SubscribeMeterCreated(Action<Meter> handler) => _meterCreated += handler;
        public void UnsubscribeMeterCreated(Action<Meter> handler) => _meterCreated -= handler;
        public void SubscribeMeterUpdated(Action<Meter> handler) => _meterUpdated += handler;
        public void UnsubscribeMetersLoaded(Action<Meter> handler) => _meterUpdated -= handler;
        public void SubscribeMeterDeleted(Action<Guid> handler) => _meterDeleted += handler;
        public void UnsubscribeMeterDeleted(Action<Guid> handler) => _meterDeleted -= handler;
    }
}
