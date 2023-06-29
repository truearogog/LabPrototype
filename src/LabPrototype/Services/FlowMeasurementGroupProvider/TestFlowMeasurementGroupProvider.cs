using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace LabPrototype.Services.FlowMeasurementGroupProvider
{
    public class TestFlowMeasurementGroupProvider : IFlowMeasurementGroupProvider
    {
        private readonly IMeterService _meterService;
        private readonly IMeterTypeService _meterTypeService;

        public event Action<MeasurementGroup>? MeasurementGroupUpdated;

        private readonly Timer _timer;
        public bool IsRunning => _timer.Enabled;

        private readonly Random _random;

        private ICollection<MeasurementGroup> _measurementGroups = new List<MeasurementGroup>();

        public TestFlowMeasurementGroupProvider(IMeterService meterService, IMeterTypeService meterTypeService)
        {
            _meterService = meterService;
            _meterTypeService = meterTypeService;

            var meters = _meterService.GetAll().ToList();
            foreach (var meter in meters)
            {
                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                var measurements = measurementTypes.Select(x => new Measurement() { Value = 0, MeasurementTypeId = x.Id, }).ToList();

                _measurementGroups.Add(new MeasurementGroup() { MeterId = meter.Id, Measurements = measurements, });
            }

            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += UpdateMeasurementGroups;

            _random = new Random();
        }

        private void UpdateMeasurementGroups(object? sender, ElapsedEventArgs e)
        {
            foreach (var measurementGroup in _measurementGroups)
            {
                foreach (var measurement in measurementGroup.Measurements ?? Enumerable.Empty<Measurement>())
                {
                    measurement.Value += _random.Next(-10, 11);
                }

                MeasurementGroupUpdated?.Invoke(measurementGroup);
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
