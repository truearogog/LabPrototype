using LabPrototype.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Services.Implementations
{
    public class MeterService : IMeterService
    {
        public event Action MetersLoaded;
        public event Action<Meter> MeterCreated;
        public event Action<Meter> MeterUpdated;
        public event Action<Guid> MeterDeleted;

        private readonly IGetAllMetersQuery _getAllMetersQuery;
        private readonly ICreateMeterCommand _createMeterCommand;
        private readonly IUpdateMeterCommand _updateMeterCommand;
        private readonly IDeleteMeterCommand _deleteMeterCommand;

        private readonly List<Meter> _meters;
        public IEnumerable<Meter> Meters => _meters;

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
            IEnumerable<Meter> meters = await _getAllMetersQuery.Execute();

            _meters.Clear();
            _meters.AddRange(meters);

            MetersLoaded?.Invoke();
        }

        public async Task Create(Meter meter)
        {
            await _createMeterCommand.Execute(meter);
            _meters.Add(meter);
            MeterCreated?.Invoke(meter);
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
            MeterUpdated?.Invoke(meter);
        }

        public async Task Delete(Guid id)
        {
            await _deleteMeterCommand.Execute(id);
            _meters.RemoveAll(x => x.Id.Equals(id));
            MeterDeleted?.Invoke(id);
        }
    }
}
