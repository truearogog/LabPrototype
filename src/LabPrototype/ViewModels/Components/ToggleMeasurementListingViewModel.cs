using LabPrototype.Domain.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingViewModel : ViewModelBase
    {
        public event Action<Guid, bool> OnChecked;

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        public ToggleMeasurementListingViewModel()
        {

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
            if (meter != null)
            {
                Items.Clear();
                if (meter != null)
                {
                    Items.Add(new ToggleMeasurementListingItemViewModel(
                        this,
                        new MeasurementAttribute(
                            "Time",
                            string.Empty,
                            x => x.DateTime.ToString(),
                            "DateTime",
                            ColorScheme.Midnight
                        )
                    ));
                    foreach (var measurementAttribute in meter.MeasurementAttributes)
                    {
                        Items.Add(new ToggleMeasurementListingItemViewModel(this, measurementAttribute));
                    }
                }
            }
        }

        public void UpdateMeasurement(Measurement measurement)
        {
            foreach (var item in Items)
            {
                item.Update(measurement);
            }
        }

        public void UpdateMeasurementAttribute(Guid id, bool isChecked)
        {
            OnChecked?.Invoke(id, isChecked);
        }
    }
}
