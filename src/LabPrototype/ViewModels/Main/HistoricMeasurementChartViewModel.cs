using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;

namespace LabPrototype.ViewModels.Main
{
    public class HistoricMeasurementChartViewModel : ViewModelBase
    {
        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }
        public MeasurementChartViewModel MeasurementChartViewModel { get; }

        public HistoricMeasurementChartViewModel(
            ISelectedMeterService selectedMeterService, 
            IChartMeasurementProvider measurementProvider, 
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel(
                selectedMeterService, 
                measurementProvider, 
                enabledMeasurementAttributeService);
            MeasurementChartViewModel = new MeasurementChartViewModel(
                selectedMeterService, 
                enabledMeasurementAttributeService, 
                measurementService, 
                measurementProvider);
        }
    }
}
