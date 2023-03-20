using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System;

namespace LabPrototype.Services.Implementations
{
    public class ChartMeasurementProvider : IChartMeasurementProvider
    {
        public event Action<Measurement> MeasurementUpdated;

        private Measurement _measurement;
        public Measurement Measurement
        {
            get => _measurement;
            set
            {
                _measurement = value;
                MeasurementUpdated?.Invoke(_measurement);
            }
        }

        public void Dispose()
        {

        }
    }
}
