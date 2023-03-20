using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System;
using System.Timers;

namespace LabPrototype.Services.Implementations
{
    public class TestFlowMeasurementProvider : IFlowMeasurementProvider, IDisposable
    {
        public event Action<Measurement> MeasurementUpdated;

        private readonly Timer _timer;
        public bool IsRunning => _timer.Enabled;

        private Measurement _measurement;
        private readonly Random _random;

        public TestFlowMeasurementProvider()
        {
            // todo: add elapsed time to config
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += GetNewMeasurement;

            _measurement = new Measurement(0, 0, 0, 0);
            _random = new Random();
        }

        private void GetNewMeasurement(object? sender, ElapsedEventArgs e)
        {
            Measurement measurement = new Measurement(
                _measurement.Q1 + _random.Next(-10, 11),
                _measurement.Q2 + _random.Next(-10, 11),
                _measurement.P + _random.Next(-10, 11),
                _measurement.T + _random.Next(-10, 11)
            );
            _measurement = measurement;
            MeasurementUpdated?.Invoke(_measurement);
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
