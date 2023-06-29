namespace LabPrototype.Services.FlowMeasurementGroupProvider
{
    public interface IFlowMeasurementGroupProvider : IMeasurementGroupProvider
    {
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}
