using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        public event Action<int, bool>? OnChecked;

        public ObservableCollection<ToggleMeasurementListingItemViewModel> ToggleMeasurementListingItems { get; set; } = new();

        private readonly IMeterService _meterTypeService;

        public ToggleMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterService>();
        }

        public override void Dispose()
        {
            foreach (var item in ToggleMeasurementListingItems)
            {
                item.Dispose();
            }
            base.Dispose();
        }
        
        public void Update(Meter meter, MeasurementDisplayMode displayMode)
        {
            if (meter is not null)
            {
                ToggleMeasurementListingItems.Clear();

                ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(
                    new MeasurementType() { Name = "Date/time", PrimaryColor = "#2c3e50", SecondaryColor = "#34495e" },
                    this,
                    measurementGroup => measurementGroup.DateTime.ToString()));

                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.Id);
                var measurementTypeIndex = 0;
                foreach (var measurementType in measurementTypes)
                {
                    ToggleMeasurementListingItems.Add(new ToggleMeasurementListingItemViewModel(measurementType, this, measurementGroup =>
                    {
                        var group = displayMode.ValueSelector?.Invoke(measurementGroup);
                        return group?.ElementAt(measurementTypeIndex).ToString() ?? string.Empty;
                    }));
                    ++measurementTypeIndex;
                }
            }
        }

        public void UpdateMeasurementGroup(MeasurementGroupEntity measurement)
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
