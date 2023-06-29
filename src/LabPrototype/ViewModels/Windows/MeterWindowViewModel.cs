using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.WindowService;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs.MeterSettings;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views.Dialogs.MeterSettings;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MeterWindowViewModel : ParametrizedWindowViewModelBase<ModelNavigationParameter<Meter>>
    {
        private readonly IWindowService _windowService;
        private readonly IMeterStore _meterStore;

        private Meter? _meter = null;
        public Meter? Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        public MeterDetailListingViewModel MeterDetailListingViewModel { get; }
        public FlowMeasurementListingViewModel FlowMeasurementListingViewModel { get; }

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }
        public MeasurementHistoryChartViewModel MeasurementHistoryChartViewModel { get; }
        public MeasurementHistoryTableViewModel MeasurementHistoryTableViewModel { get; }

        public ICommand SelectChartCommand { get; }
        public ICommand SelectTableCommand { get; }
        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public MeterWindowViewModel()
        {
            _windowService = GetRequiredService<IWindowService>();

            _meterStore = GetRequiredService<IMeterStore>();
            _meterStore.ModelUpdated += _MeterUpdated;
            _meterStore.ModelDeleted += _MeterDeleted;

            MeterDetailListingViewModel = new MeterDetailListingViewModel();
            FlowMeasurementListingViewModel = new FlowMeasurementListingViewModel();

            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel();
            MeasurementHistoryChartViewModel = new MeasurementHistoryChartViewModel(ToggleMeasurementListingViewModel) { IsVisible = true };
            MeasurementHistoryTableViewModel = new MeasurementHistoryTableViewModel() { IsVisible = false };

            SelectChartCommand = ReactiveCommand.Create(SelectMeasurementChart);
            SelectTableCommand = ReactiveCommand.Create(SelectMeasurementTable);
            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(OpenUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(OpenDeleteMeterDialogAsync);
        }

        public override void Activate(ModelNavigationParameter<Meter> parameter)
        {
            Meter = parameter.Model;
            MeterDetailListingViewModel.UpdateMeter(Meter);
            FlowMeasurementListingViewModel.UpdateMeter(Meter);
            ToggleMeasurementListingViewModel.UpdateMeter(Meter);
            MeasurementHistoryChartViewModel.UpdateMeter(Meter);
            MeasurementHistoryTableViewModel.UpdateMeter(Meter);
        }

        public override void Dispose()
        {
            _meterStore.ModelUpdated -= _MeterUpdated;
            _meterStore.ModelDeleted -= _MeterDeleted;

            MeterDetailListingViewModel.Dispose();
            FlowMeasurementListingViewModel.Dispose();
            ToggleMeasurementListingViewModel.Dispose();
            MeasurementHistoryChartViewModel.Dispose();
            MeasurementHistoryTableViewModel.Dispose();
        }

        private void _MeterUpdated(Meter? meter)
        {
            if (meter is not null && Meter is not null)
            {
                if (Meter.Id.Equals(meter.Id))
                {
                    Activate(new ModelNavigationParameter<Meter>(meter));
                }
            }
        }

        private void _MeterDeleted(int id)
        {
            if (Meter is not null)
            {
                if (Meter.Id.Equals(id))
                {
                    Close();
                }
            }
        }

        private void SelectMeasurementChart()
        {
            MeasurementHistoryChartViewModel.IsVisible = true;
            MeasurementHistoryTableViewModel.IsVisible = false;
        }

        private void SelectMeasurementTable()
        {
            MeasurementHistoryChartViewModel.IsVisible = false;
            MeasurementHistoryTableViewModel.IsVisible = true;
        }

        private async Task OpenUpdateMeterDialogAsync()
        {
            var parameter = new ModelNavigationParameter<Meter>(Meter);
            await _windowService.ShowDialogAsync<UpdateMeterDialog, UpdateMeterDialogViewModel, ModelNavigationParameter<Meter>>(this, parameter);
        }

        private async Task OpenDeleteMeterDialogAsync()
        {
            var parameter = new ModelNavigationParameter<Meter>(Meter);
            await _windowService.ShowDialogAsync<DeleteMeterDialog, DeleteMeterDialogViewModel, ModelNavigationParameter<Meter>>(this, parameter);
        }
    }
}
