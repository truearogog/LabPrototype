﻿using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class SelectedMeterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IMeterService _meterService;
        private readonly ISelectedMeterService _selectedMeterService;
        public Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public MeterDetailsViewModel MeterDetailsViewModel { get; }
        public HistoricMeasurementsChartViewModel HistoricMeasurementsChartViewModel { get; }
        public FlowMeasurementsViewModel FlowMeasurementsViewModel { get; }

        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public SelectedMeterViewModel(IDialogService dialogService, IMeterService meterService, ISelectedMeterService selectedMeterService, IFlowMeasurementProvider flowMeasurementProvider)
        {
            _dialogService = dialogService;
            _meterService = meterService;
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SubscribeSelectedMeterUpdated(SelectedMeterService_SelectedMeterUpdated);

            MeterDetailsViewModel = new MeterDetailsViewModel(selectedMeterService);
            HistoricMeasurementsChartViewModel = new HistoricMeasurementsChartViewModel();
            FlowMeasurementsViewModel = new FlowMeasurementsViewModel(selectedMeterService, flowMeasurementProvider);

            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(ShowUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(ShowDeleteMeterDialogAsync);

            if (!flowMeasurementProvider.IsRunning)
            {
                flowMeasurementProvider.Start();
            }
        }

        private async Task ShowUpdateMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(UpdateMeterDialogViewModel), parameter);
        }

        private async Task ShowDeleteMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(DeleteMeterDialogViewModel), parameter);
        }

        private void SelectedMeterService_SelectedMeterUpdated()
        {
            this.RaisePropertyChanged(nameof(SelectedMeter));
        }
    }
}
