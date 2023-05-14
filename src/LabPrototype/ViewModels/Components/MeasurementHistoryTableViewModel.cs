using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementService _measurementService;

        public ObservableCollection<Measurement> Measurements { get; set; } = new();

        public MeasurementHistoryTableViewModel()
        {
            _measurementService = GetRequiredService<IMeasurementService>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter != null)
            {
                Task.Run(() => _measurementService.LoadMeter(meter.Id)).Wait();
                var measurements = _measurementService.LoadedMeasurements[meter.Id];
                Measurements = new(measurements);
                UpdateView(meter);
                this.RaisePropertyChanged(nameof(Measurements));
            }
        }
    }
}
