﻿using LabPrototype.Domain.Models;
using LabPrototype.Services.Implementations;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System;
using System.Collections.Generic;
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

        public MeterDetailListingViewModel MeterDetailListingViewModel { get; }
        public FlowMeasurementListingViewModel FlowMeasurementListingViewModel { get; }

        public ToggleMeasurementListingViewModel ToggleMeasurementListingViewModel { get; }
        
        private Dictionary<Type, ViewModelBase> _measurementPresentationViewModels = new Dictionary<Type, ViewModelBase>();

        public MeasurementChartViewModel MeasurementChartViewModel
            => _measurementPresentationViewModels[typeof(MeasurementChartViewModel)] as MeasurementChartViewModel;
        public MeasurementTableViewModel MeasurementTableViewModel { get; }

        public ICommand SelectMeasurementChartCommand { get; }
        public ICommand SelectMeasurementTableCommand { get; }
        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public SelectedMeterViewModel(
            IDialogService dialogService, 
            IMeterService meterService, 
            ISelectedMeterService selectedMeterService, 
            IFlowMeasurementProvider flowMeasurementProvider,
            IChartMeasurementProvider chartMeasurementProvider,
            IEnabledMeasurementAttributeService enabledMeasurementAttributeService,
            IMeasurementService measurementService)
        {
            _dialogService = dialogService;
            _meterService = meterService;
            _selectedMeterService = selectedMeterService;
            _selectedMeterService.SelectedMeterUpdated += _SelectedMeterUpdated;

            MeterDetailListingViewModel = new MeterDetailListingViewModel(selectedMeterService);
            FlowMeasurementListingViewModel = new FlowMeasurementListingViewModel(selectedMeterService, flowMeasurementProvider);

            ToggleMeasurementListingViewModel = new ToggleMeasurementListingViewModel(
                selectedMeterService,
                chartMeasurementProvider,
                enabledMeasurementAttributeService);

            MeasurementChartViewModel = new MeasurementChartViewModel(
                selectedMeterService,
                enabledMeasurementAttributeService,
                measurementService,
                chartMeasurementProvider);

            MeasurementTableViewModel = new MeasurementTableViewModel(
                selectedMeterService,
                measurementService);

            SelectMeasurementChartCommand = ReactiveCommand.Create(SelectMeasurementChart);
            SelectMeasurementTableCommand = ReactiveCommand.Create(SelectMeasurementTable);

            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(OpenUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(OpenDeleteMeterDialogAsync);

            if (!flowMeasurementProvider.IsRunning)
            {
                flowMeasurementProvider.Start();
            }
        }

        public override void Dispose()
        {
            _selectedMeterService.SelectedMeterUpdated -= _SelectedMeterUpdated;
            base.Dispose();
        }

        private void SelectMeasurementChart()
        {
            CurrentMeasurementViewModel = MeasurementChartViewModel;
        }

        private void SelectMeasurementTable()
        {
            CurrentMeasurementViewModel = MeasurementTableViewModel;
        }

        private async Task OpenUpdateMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(UpdateMeterDialogViewModel), parameter);
        }

        private async Task OpenDeleteMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(DeleteMeterDialogViewModel), parameter);
        }

        private void _SelectedMeterUpdated(Meter meter)
        {
            this.RaisePropertyChanged(nameof(SelectedMeter));
        }
    }
}
