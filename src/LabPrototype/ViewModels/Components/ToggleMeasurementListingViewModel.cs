using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        public event Action<int, bool>? OnChecked;

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        private readonly IMeterTypeService _meterTypeService;
        private readonly IColorSchemeService _colorSchemeService;

        public ToggleMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _colorSchemeService = GetRequiredService<IColorSchemeService>();
        }

        public override void Dispose()
        {
            foreach (var item in Items)
            {
                item.Dispose();
            }
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId) ?? Enumerable.Empty<MeasurementType>();

                Items.Clear();

                var dateTimeColorScheme = _colorSchemeService.GetById(9);
                Items.Add(new ToggleMeasurementListingItemViewModel(
                    this, 
                    new MeasurementType() { Name = "Date/time", ColorScheme = dateTimeColorScheme }, 
                    measurementGroup => measurementGroup.Created.ToString()));
                foreach (var measurementType in measurementTypes)
                {
                    Items.Add(new ToggleMeasurementListingItemViewModel(this, measurementType));
                }
            }
        }

        public void UpdateMeasurementGroup(MeasurementGroup measurement)
        {
            foreach (var item in Items)
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
