namespace LabPrototype.Services.Interfaces
{
    public interface IFlowMeasurementProvider : IMeasurementProvider
    {
        public bool IsRunning { get; }
        void Start();
        void Stop();
    }
}
