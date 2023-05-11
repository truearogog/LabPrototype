using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views.Dialogs;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MeterWindowViewModel : ParametrizedWindowViewModelBase<MeterNavigationParameter>
    {
        private readonly IWindowService _windowService;
        private readonly IMeterService _meterService;

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
            _meterService = GetRequiredService<IMeterService>();

            _meterService.MeterUpdated += _MeterUpdated;
            _meterService.MeterDeleted += _MeterDeleted;

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

        public override void Activate(MeterNavigationParameter parameter)
        {
            Meter = parameter.Meter;
            MeterDetailListingViewModel.UpdateMeter(Meter);
            FlowMeasurementListingViewModel.UpdateMeter(Meter);
            MeasurementHistoryChartViewModel.UpdateMeter(Meter);
            MeasurementHistoryTableViewModel.UpdateMeter(Meter);
        }

        public override void Dispose()
        {
            _meterService.MeterUpdated -= _MeterUpdated;
            _meterService.MeterDeleted -= _MeterDeleted;

            MeterDetailListingViewModel.Dispose();
            FlowMeasurementListingViewModel.Dispose();
            ToggleMeasurementListingViewModel.Dispose();
            MeasurementHistoryChartViewModel.Dispose();
            MeasurementHistoryTableViewModel.Dispose();
        }

        private void _MeterUpdated(Meter meter)
        {
            if (Meter != null)
            {
                if (Meter.Id.Equals(meter.Id))
                {
                    Meter = meter;
                    MeterDetailListingViewModel.UpdateMeter(Meter);
                    FlowMeasurementListingViewModel.UpdateMeter(Meter);
                    MeasurementHistoryChartViewModel.UpdateMeter(Meter);
                    MeasurementHistoryTableViewModel.UpdateMeter(Meter);
                }
            }
        }

        private void _MeterDeleted(Guid id)
        {
            if (Meter != null)
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
            MeterNavigationParameter parameter = new MeterNavigationParameter(Meter);
            await _windowService.ShowDialogAsync<UpdateMeterDialog, UpdateMeterDialogViewModel, MeterNavigationParameter>(this, parameter);
        }

        private async Task OpenDeleteMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(Meter);
            await _windowService.ShowDialogAsync<DeleteMeterDialog, DeleteMeterDialogViewModel, MeterNavigationParameter>(this, parameter);
        }
    }
}
