using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Linq;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class ToggleMeasurementListingItemViewModel : ViewModelBase
    {
        private MeasurementType _measurementType;
        public MeasurementType MeasurementType
        {
            get => _measurementType;
            set => this.RaiseAndSetIfChanged(ref _measurementType, value);
        }

        private string? _value = null;
        public string? Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }

        public bool HasValue => Value is not null;

        private ToggleMeasurementListingViewModel? _toggleMeasurementListingViewModel;
        private Func<MeasurementGroupEntity, string?>? _valueSelector = null;

        public bool IsChecked { get; set; } = true;
        public ICommand ToggledCommand { get; }

        public ToggleMeasurementListingItemViewModel(
            MeasurementType measurementType,
            ToggleMeasurementListingViewModel toggleMeasurementListingViewModel, 
            Func<MeasurementGroupEntity, string?>? valueSelector = null)
        {
            _measurementType = measurementType;
            _toggleMeasurementListingViewModel = toggleMeasurementListingViewModel;
            _valueSelector = valueSelector;

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

        public void Update(MeasurementGroupEntity measurementGroup)
        {
            Value = _valueSelector?.Invoke(measurementGroup);
            this.RaisePropertyChanged(nameof(HasValue));
        }
    }
}
