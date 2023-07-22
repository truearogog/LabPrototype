using LabPrototype.AppManagers.Services;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Domain.Models.Presentation.MeasurementGroups;
using LabPrototype.Domain.Models.Presentation.Measurements;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        public event Action<int, bool>? OnChecked;

        public ObservableCollection<ToggleMeasurementListingItemViewModel> ToggleMeasurementListingItems { get; set; } = new();

        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeterTypeMeasurementTypeService _meterTypeMeasurementTypeService;

        public ToggleMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _meterTypeMeasurementTypeService = GetRequiredService<IMeterTypeMeasurementTypeService>();
        }

        public override void Dispose()
        {
            foreach (var item in ToggleMeasurementListingItems)
            {
                item.Dispose();
            }
            base.Dispose();
        }

        public void Update(Meter? meter, Func<Measurement, double>? valueSelector = null)
        {
            if (meter is not null)
            {
                var meterTypeMeasurementTypes = _meterTypeMeasurementTypeService.GetAll(x => x.MeterTypeId.Equals(meter.Id));

                var measurementTypes = 
                    _meterTypeService
                    .GetMeasurementTypes(meter.MeterTypeId)
                    .OrderBy(x => meterTypeMeasurementTypes.FirstOrDefault(y => y.MeasurementTypeId.Equals(x.Id))?.SortOrder ?? int.MaxValue)
                    ?? Enumerable.Empty<MeasurementType>();

                ToggleMeasurementListingItems.Clear();

                var dateTimeColorScheme = new ColorScheme { PrimaryColor = "#2c3e50", SecondaryColor = "#34495e" };
                ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(
                    new MeasurementType() { Name = "Date/time", ColorScheme = dateTimeColorScheme }, 
                    this,
                    measurementGroup => measurementGroup.DateTime.ToString()));

                foreach (var measurementType in measurementTypes)
                {
                    ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(measurementType, this, measurementGroup =>
                    {
                        var measurement = measurementGroup.Measurements?.FirstOrDefault(x => x.MeasurementTypeId.Equals(measurementType.Id));
                        return measurement is not null ? valueSelector?.Invoke(measurement).ToString() : null;
                    }));
                }
            }
        }

        public void UpdateMeasurementGroup(MeasurementGroup measurement)
        {
            foreach (var item in ToggleMeasurementListingItems)
            {
                item.Update(measurement);
            }
        }

        public void UpdateMeasurementAttribute(int id, bool isChecked)
        {
            OnChecked?.Invoke(id, isChecked);
        }
    }
}
