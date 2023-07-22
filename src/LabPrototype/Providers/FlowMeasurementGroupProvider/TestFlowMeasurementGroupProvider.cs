﻿using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation.MeasurementGroups;
using LabPrototype.Domain.Models.Presentation.Measurements;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace LabPrototype.Providers.FlowMeasurementGroupProvider
{
    public class TestFlowMeasurementGroupProvider : IFlowMeasurementGroupProvider
    {
        private readonly IMeterService _meterService;
        private readonly IMeterTypeService _meterTypeService;

        public event Action<FlowMeasurementGroup>? MeasurementGroupUpdated;
        
        private readonly Random _random;

        private IDictionary<int, Timer> _timers = new ConcurrentDictionary<int, Timer>();
        private IDictionary<int, FlowMeasurementGroup> _measurementGroups = new ConcurrentDictionary<int, FlowMeasurementGroup>();

        public TestFlowMeasurementGroupProvider(IMeterService meterService, IMeterTypeService meterTypeService)
        {
            _meterService = meterService;
            _meterTypeService = meterTypeService;

            _random = new Random();
        }

        public void Start(int meterId)
        {
            var meter = _meterService.GetById(meterId);
            if (meter is not null)
            {
                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                if (measurementTypes.Any())
                {
                    var measurementGroup = new FlowMeasurementGroup { MeterId = meterId, Measurements = new List<FlowMeasurement>() };
                    foreach (var measurementType in measurementTypes)
                    {
                        var measurement = new FlowMeasurement { MeasurementTypeId = measurementType.Id, Value = 0 };
                        measurementGroup.Measurements?.Add(measurement);
                    }
                    _measurementGroups.TryAdd(meterId, measurementGroup);

                    var timer = new Timer(1000) { AutoReset = true };
                    timer.Elapsed += (s, e) =>
                    {
                        if (_measurementGroups.TryGetValue(meterId, out var measurementGroup))
                        {
                            foreach (var measurement in measurementGroup.Measurements ?? Array.Empty<FlowMeasurement>())
                            {
                                measurement.Value += _random.Next(-10, 11);
                            }

                            MeasurementGroupUpdated?.Invoke(measurementGroup);
                        }
                    };
                    timer.Start();
                    _timers.Add(meterId, timer);
                }
            }
        }

        public bool IsRunning(int meterId)
        {
            return _timers.TryGetValue(meterId, out var timer) && timer.Enabled;
        }


        public void Stop(int meterId)
        {
            if (_timers.TryGetValue(meterId, out var timer))
            {
                timer.Stop();
                timer.Dispose();
                _timers.Remove(meterId);
                _measurementGroups.Remove(meterId);
            }
        }
    }
}