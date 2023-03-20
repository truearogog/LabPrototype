using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LabPrototype.Services.Implementations
{
    public class MeasurementService : IMeasurementService
    {
        public event Action<Guid> MeterMeasurementsLoaded;

        public Dictionary<Guid, List<Measurement>> LoadedMeasurements { get; set; } = new Dictionary<Guid, List<Measurement>>();

        private readonly IGetMeasurementsQuery _getMeasurementsQuery;

        public MeasurementService(IGetMeasurementsQuery getMeasurementsQuery)
        {
            _getMeasurementsQuery = getMeasurementsQuery;
        }

        public async Task LoadMeter(Guid meterId)
        {
            if (!LoadedMeasurements.ContainsKey(meterId))
            {
                var measurements = await _getMeasurementsQuery.Execute(meterId);

                LoadedMeasurements.Add(meterId, measurements.ToList());

                MeterMeasurementsLoaded?.Invoke(meterId);
            }
        }
    }
}
