using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using System.Collections.ObjectModel;

namespace LabPrototype.ViewModels.Components
{
    public class FlowMeasurementListingViewModel : ViewModelBase
    {
        private readonly IMeterTypeService _meterTypeService;
        private readonly IColorSchemeService _colorSchemeService;

        public ObservableCollection<MeasurementListingItemViewModel> Items { get; set; } = new();

        public FlowMeasurementListingViewModel()
        {
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _colorSchemeService = GetRequiredService<IColorSchemeService>();
        }

        private void CreateMeasurements(Meter meter)
        {
            Items.Clear();
            var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.Id);

            foreach (var measurementType in measurementTypes)
            {
                Items.Add(new MeasurementListingItemViewModel(measurementAttribute));
            }
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                CreateMeasurements(meter);
            }
        }
    }
}
