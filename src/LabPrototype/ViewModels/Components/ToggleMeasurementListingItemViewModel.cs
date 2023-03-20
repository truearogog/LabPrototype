using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingItemViewModel : MeasurementListingItemViewModel
    {
        private readonly IEnabledMeasurementAttributeService? _enabledMeasurementAttributeService;

        public bool IsChecked { get; set; } = true;
        public ICommand ToggledCommand { get; }

        public ToggleMeasurementListingItemViewModel(
            MeasurementAttribute measurementAttribute, 
            IEnabledMeasurementAttributeService? enabledMeasurementAttributeService = null) : base(measurementAttribute)
        {
            _enabledMeasurementAttributeService = enabledMeasurementAttributeService;

            ToggledCommand = ReactiveCommand.Create(Toggled);
            Toggled();
        }

        private void Toggled()
        {
            _enabledMeasurementAttributeService?.Update(MeasurementAttribute.Id, IsChecked);
        }
    }
}
