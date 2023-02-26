using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class HistoricMeasurementChartViewModel : ViewModelBase
    {
        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }
        public MeasurementChartViewModel MeasurementChartViewModel { get; }

        public ICommand MinusLimitCommand { get; set; }
        public ICommand PlusLimitCommand { get; set; }

        public HistoricMeasurementChartViewModel(
            ISelectedMeterService selectedMeterService, 
            IMeasurementProvider measurementProvider, 
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService)
        {
            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel(selectedMeterService, measurementProvider, enabledMeasurementAttributeService);
            MeasurementChartViewModel = new MeasurementChartViewModel(selectedMeterService, enabledMeasurementAttributeService);

            MinusLimitCommand = ReactiveCommand.Create(MeasurementChartViewModel.MinusLimit);
            PlusLimitCommand = ReactiveCommand.Create(MeasurementChartViewModel.PlusLimit);
        }
    }
}
