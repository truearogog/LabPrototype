using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingItemViewModel : MeasurementListingItemViewModel
    {
        private ToggleMeasurementListingViewModel? _toggleMeasurementListingViewModel;

        public bool IsChecked { get; set; } = true;
        public ICommand ToggledCommand { get; }

        public ToggleMeasurementListingItemViewModel(
            ToggleMeasurementListingViewModel toggleMeasurementListingViewModel, 
            MeasurementType measurementType, 
            Func<MeasurementGroup, string>? valueGetter = null)
            : base(measurementType, valueGetter)
        {
            _toggleMeasurementListingViewModel = toggleMeasurementListingViewModel;

            ToggledCommand = ReactiveCommand.Create(Toggled);
            Toggled();
        }

        public override void Dispose()
        {
            _toggleMeasurementListingViewModel = null;
            base.Dispose();
        }

        private void Toggled()
        {
            _toggleMeasurementListingViewModel?.UpdateMeasurementAttribute(MeasurementType.Id, IsChecked);
        }
    }
}
