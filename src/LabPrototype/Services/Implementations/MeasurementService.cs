using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabPrototype.Services.Implementations
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IGetMeasurementsQuery _getMeasurementsQuery;

        private Dictionary<Guid, List<Measurement>> _loadedMeasurements = new Dictionary<Guid, List<Measurement>>();

        public IDictionary<Guid, List<Measurement>> LoadedMeasurements => _loadedMeasurements;

        private event Action<Guid> _meterMeasurementsLoaded;

        public MeasurementService(
            IGetMeasurementsQuery getMeasurementsQuery)
        {
            _getMeasurementsQuery = getMeasurementsQuery;
        }

        public async Task LoadMeter(Guid meterId)
        {
            if (!_loadedMeasurements.ContainsKey(meterId))
            {
                var measurements = await _getMeasurementsQuery.Execute(meterId);

                _loadedMeasurements.Add(meterId, measurements.ToList());

                _meterMeasurementsLoaded?.Invoke(meterId);
            }
        }

        public void SubscribeMeterMeasurementsLoaded(Action<Guid> handler) => _meterMeasurementsLoaded += handler;

        public void UnsubscribeMeterMeasurementsLoaded(Action<Guid> handler) => _meterMeasurementsLoaded -= handler;
    }
}
