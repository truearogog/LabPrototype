using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using LabPrototype.Providers.IntegrationCacheProvider;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace LabPrototype.ViewModels.Components
{
    public class IntegralMeasurementListingViewModel : ViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementCacheProvider _measurementCacheProvider;

        public ObservableCollection<IntegralMeasurementListingItemViewModel> IntegralMeasurementListingItems { get; set; } = new();

        public DateTimePickerViewModel SelectionStartDateTimePickerViewModel { get; }
        public DateTimePickerViewModel SelectionEndDateTimePickerViewModel { get; }

        private int _meterId;
        private int _archiveId;
        private MeasurementDisplayMode _displayMode;

        public IntegralMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementCacheProvider = GetRequiredService<IMeasurementCacheProvider>();

            SelectionStartDateTimePickerViewModel = new DateTimePickerViewModel();
            SelectionEndDateTimePickerViewModel = new DateTimePickerViewModel();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                _meterId = meter.Id;
                CreateMeasurements(meter);
            }
        }

        public void UpdateIntegrationArchive(int archiveId)
        {
            _archiveId = archiveId;
        }

        public void UpdateIntegrationDisplayMode(MeasurementDisplayMode displayMode)
        {
            _displayMode = displayMode;
        }

        private void CreateMeasurements(Meter meter)
        {
            IntegralMeasurementListingItems.Clear();
            var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
            foreach (var measurementType in measurementTypes)
            {
                IntegralMeasurementListingItems.Add(new IntegralMeasurementListingItemViewModel(measurementType));
            }
        }

        public void UpdateSelectionEdges(double? selectionMin, double? selectionMax)
        {
            SelectionStartDateTimePickerViewModel.SelectedDateTime = selectionMin.HasValue ? DateTime.FromOADate(selectionMin.Value) : null;
            SelectionEndDateTimePickerViewModel.SelectedDateTime = selectionMax.HasValue ? DateTime.FromOADate(selectionMax.Value) : null;
        }

        public async Task UpdateSelectionIntegration()
        {
            if (SelectionStartDateTimePickerViewModel.HasSelected && SelectionEndDateTimePickerViewModel.HasSelected)
            {
                var integratedMeasurementGroup = await _measurementCacheProvider.Integrate(_meterId, _archiveId, _displayMode,
                    SelectionStartDateTimePickerViewModel.SelectedDateTime!.Value.ToOADate(),
                    SelectionEndDateTimePickerViewModel.SelectedDateTime!.Value.ToOADate());

                foreach (var integralMeasurementListingItem in IntegralMeasurementListingItems)
                {
                    integralMeasurementListingItem.Update(integratedMeasurementGroup);
                }
            }
        }
    }
}
