using LabPrototype.AppManagers.Services;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        public event Action<int, bool>? OnChecked;

        public ObservableCollection<ToggleMeasurementListingItemViewModel> ToggleMeasurementListingItems { get; set; } = new();

        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupSchemaService _measurementGroupSchemaService;

        public ToggleMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupSchemaService = GetRequiredService<IMeasurementGroupSchemaService>();
        }

        public override void Dispose()
        {
            foreach (var item in ToggleMeasurementListingItems)
            {
                item.Dispose();
            }
            base.Dispose();
        }
        
        public void Update(Meter meter, Func<MeasurementGroup, IEnumerable<double>>? groupSelector = null)
        {
            if (meter is not null)
            {
                ToggleMeasurementListingItems.Clear();

                var dateTimeColorScheme = new ColorScheme { PrimaryColor = "#2c3e50", SecondaryColor = "#34495e" };
                ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(
                    new MeasurementType() { Name = "Date/time", ColorScheme = dateTimeColorScheme },
                    this,
                    measurementGroup => measurementGroup.DateTime.ToString()));

                // create dictionary that contains schema id -> measurement type -> array index
                var schemas = _measurementGroupSchemaService.GetAll(x => x.MeterTypeId.Equals(meter.MeterTypeId));
                var schemaTypeIndexes = new Dictionary<int, Dictionary<int, int>>();
                foreach (var schema in schemas)
                {
                    var typeIndexes = new Dictionary<int, int>();
                    var measurements = _measurementGroupSchemaService.GetMeasurementTypes(schema.Id);
                    var i = 0;
                    foreach (var measurement in measurements)
                    {
                        typeIndexes.Add(measurement.Id, i++);
                    }
                    schemaTypeIndexes.Add(schema.Id, typeIndexes);
                }

                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                foreach (var measurementType in measurementTypes)
                {
                    ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(measurementType, this, measurementGroup =>
                    {
                        var index = schemaTypeIndexes[measurementGroup.MeasurementGroupSchemaId][measurementType.Id];
                        var group = groupSelector?.Invoke(measurementGroup);
                        return group?.ElementAt(index).ToString() ?? null;
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
