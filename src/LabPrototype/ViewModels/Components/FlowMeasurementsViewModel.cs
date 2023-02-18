using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementsViewModel : ViewModelBase
    {
        public string Title => "Flow";

        private ISelectedMeterService _selectedMeterService;
        private IFlowMeasurementProvider _measurementProvider;

        public ObservableCollection<MeasurementViewModel> MeasurementViewModels { get; set; } = new();

        public FlowMeasurementsViewModel(ISelectedMeterService selectedMeterService, IFlowMeasurementProvider measurementProvider)
        {
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            _measurementProvider = measurementProvider;
            _measurementProvider.SubscribeMeasurementUpdated(MeasurementProvider_MeasurementUpdated);

            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void CreateMeasurements(Meter meter)
        {
            MeasurementViewModels.Clear();
            if (meter != null)
            {
                MeasurementViewModels.Add(new MeasurementViewModel("Q1", "m³/h", x => x.Q1.ToString()));
                MeasurementViewModels.Add(new MeasurementViewModel("Q2", "m³/h", x => x.Q2.ToString()));
                MeasurementViewModels.Add(new MeasurementViewModel("ΔQ", "m³/h", x => x.DeltaQ.ToString()));
                MeasurementViewModels.Add(new MeasurementViewModel("P", "MW", x => x.P.ToString()));
                MeasurementViewModels.Add(new MeasurementViewModel("T", "°C", x => x.T.ToString()));
            }
        }

        private void UpdateMeasurements(Measurement measurement)
        {
            foreach (var measurementViewModel in MeasurementViewModels)
            {
                measurementViewModel.Update(measurement);
            }
        }

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            CreateMeasurements(_selectedMeterService.SelectedMeter);
        }

        private void MeasurementProvider_MeasurementUpdated(Measurement measurement)
        {
            UpdateMeasurements(measurement);
        }
    }
}
