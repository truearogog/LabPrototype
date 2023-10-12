using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Models;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MeterWindowViewModel : ParametrizedWindowViewModelBase<ModelNavigationParameter<Meter>>
    {
        private readonly IArchiveService _measurementGroupArchiveService;
        private readonly IArchiveStore _measurementGroupArchiveStore;
        private readonly IMeterStore _meterStore;

        private Meter? _meter = null;
        public Meter? Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        private int _selectedArchiveViewModelIndex;
        public int SelectedArchiveViewModelIndex
        {
            get => _selectedArchiveViewModelIndex;
            set
            {
                var _val = _selectedArchiveViewModelIndex;
                if (_val != this.RaiseAndSetIfChanged(ref _selectedArchiveViewModelIndex, value))
                {
                    UpdateMeasurementHistory();
                    UpdateIntegrationArchive();
                }
            }
        }

        public ObservableCollection<ValueViewModelBase<Archive>> ArchiveViewModels { get; set; } = new();

        private int _selectedMeasurementDisplayModeIndex;
        public int SelectedMeasurementDisplayModeIndex
        {
            get => _selectedMeasurementDisplayModeIndex;
            set
            {
                var _val = _selectedMeasurementDisplayModeIndex;
                if (_val != this.RaiseAndSetIfChanged(ref _selectedMeasurementDisplayModeIndex, value))
                {
                    UpdateMeasurementHistory();
                    UpdateIntegrationDisplayMode();
                }
            }
        }

        public ObservableCollection<MeasurementDisplayMode> MeasurementDisplayModes { get; set; } = new();

        public FlowMeasurementListingViewModel FlowMeasurementListingViewModel { get; }
        public IntegralMeasurementListingViewModel IntegralMeasurementListingViewModel { get; }

        public MeasurementHistoryChartViewModel MeasurementHistoryChartViewModel { get; }
        public MeasurementHistoryTableViewModel MeasurementHistoryTableViewModel { get; }

        public ICommand SelectChartCommand { get; }
        public ICommand SelectTableCommand { get; }

        public MeterWindowViewModel()
        {
            _measurementGroupArchiveService = GetRequiredService<IArchiveService>();

            _measurementGroupArchiveStore = GetRequiredService<IArchiveStore>();
            _measurementGroupArchiveStore.ModelCreated += _ArchiveCreated;
            _measurementGroupArchiveStore.ModelUpdated += _ArchiveUpdated;
            _measurementGroupArchiveStore.ModelDeleted += _ArchiveDeleted;

            _meterStore = GetRequiredService<IMeterStore>();
            _meterStore.ModelUpdated += _MeterUpdated;
            _meterStore.ModelDeleted += _MeterDeleted;

            FlowMeasurementListingViewModel = new FlowMeasurementListingViewModel();
            IntegralMeasurementListingViewModel = new IntegralMeasurementListingViewModel();

            MeasurementHistoryChartViewModel = new MeasurementHistoryChartViewModel(IntegralMeasurementListingViewModel) { IsVisible = true };
            MeasurementHistoryTableViewModel = new MeasurementHistoryTableViewModel() { IsVisible = false };

            SelectChartCommand = ReactiveCommand.Create(SelectMeasurementChart);
            SelectTableCommand = ReactiveCommand.Create(SelectMeasurementTable);

            MeasurementDisplayModes.Add(new MeasurementDisplayMode("Average", x => x.AverageValues));
            // MeasurementDisplayModes.Add(new MeasurementDisplayMode("Summary", x => x.SummaryValues));
        }

        public override void Activate(ModelNavigationParameter<Meter> parameter)
        {
            Meter = parameter.Model;

            if (Meter is not null)
            {
                var archives = _measurementGroupArchiveService.GetAll(x => x.MeterId.Equals(Meter.Id));
                CreateArchives(archives);
                this.RaisePropertyChanged(nameof(SelectedArchiveViewModelIndex));
                UpdateMeasurementHistory();
                UpdateIntegrationArchive();
                UpdateIntegrationDisplayMode();
                FlowMeasurementListingViewModel.UpdateMeter(Meter);
                IntegralMeasurementListingViewModel.UpdateMeter(Meter);
            }
        }

        public override void Dispose()
        {
            _meterStore.ModelUpdated -= _MeterUpdated;
            _meterStore.ModelDeleted -= _MeterDeleted;

            _measurementGroupArchiveStore.ModelCreated -= _ArchiveCreated;
            _measurementGroupArchiveStore.ModelUpdated -= _ArchiveUpdated;
            _measurementGroupArchiveStore.ModelDeleted -= _ArchiveDeleted;

            FlowMeasurementListingViewModel.Dispose();
            IntegralMeasurementListingViewModel.Dispose();
            MeasurementHistoryChartViewModel.Dispose();
            MeasurementHistoryTableViewModel.Dispose();
        }

        private void _MeterUpdated(Meter? meter)
        {
            if (meter is not null && Meter is not null && Meter.Id.Equals(meter.Id))
            {
                Activate(new ModelNavigationParameter<Meter> { Model = meter });
            }
        }

        private void _MeterDeleted(int id)
        {
            if (Meter is not null && Meter.Id.Equals(id))
            {
                Close();
            }
        }

        private void _ArchiveCreated(Archive archive)
        {
            if (Meter is not null && archive.MeterId.Equals(Meter.Id))
            {
                CreateArchive(archive);
            }
        }

        private void _ArchiveUpdated(Archive? archive)
        {
            if (Meter is not null && archive is not null && archive.MeterId.Equals(Meter.Id))
            {
                var archiveViewModel = ArchiveViewModels.FirstOrDefault(x => archive.Id.Equals(x.Value?.Id ?? 0));
                if (archiveViewModel is not null)
                {
                    archiveViewModel.Value = archive;
                }
            }
        }

        private void _ArchiveDeleted(int archiveId)
        {
            var archiveViewModel = ArchiveViewModels.FirstOrDefault(x => archiveId.Equals(x.Value?.Id ?? 0));
            if (archiveViewModel is not null)
            {
                ArchiveViewModels.Remove(archiveViewModel);
            }
        }

        private void CreateArchives(IEnumerable<Archive> archives)
        {
            ArchiveViewModels.Clear();
            foreach (var archive in archives)
            {
                CreateArchive(archive);
            }
        }

        private void CreateArchive(Archive archive)
        {
            ArchiveViewModels.Add(new ValueViewModelBase<Archive> { Value = archive });
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

        private void UpdateMeasurementHistory()
        {
            var selectedMeasurementDisplayMode = MeasurementDisplayModes.ElementAtOrDefault(SelectedMeasurementDisplayModeIndex);
            var selectedArchive = ArchiveViewModels.ElementAtOrDefault(SelectedArchiveViewModelIndex);
            MeasurementHistoryChartViewModel.Update(Meter, selectedArchive?.Value, selectedMeasurementDisplayMode);
            MeasurementHistoryTableViewModel.Update(Meter, selectedArchive?.Value, selectedMeasurementDisplayMode);
        }

        private void UpdateIntegrationArchive()
        {
            var selectedArchive = ArchiveViewModels.ElementAtOrDefault(SelectedArchiveViewModelIndex);
            IntegralMeasurementListingViewModel.UpdateIntegrationArchive(selectedArchive!.Value!.Id);
        }

        private void UpdateIntegrationDisplayMode()
        {
            var selectedMeasurementDisplayMode = MeasurementDisplayModes.ElementAtOrDefault(SelectedMeasurementDisplayModeIndex);
            IntegralMeasurementListingViewModel.UpdateIntegrationDisplayMode(selectedMeasurementDisplayMode);
        }
    }
}
