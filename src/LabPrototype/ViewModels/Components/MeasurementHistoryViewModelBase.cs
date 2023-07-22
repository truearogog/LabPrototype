using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Collections.Generic;

namespace LabPrototype.ViewModels.Components
{
    public abstract class MeasurementHistoryViewModelBase : ViewModelBase
    {
        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }

        public event Action<IEnumerable<MeasurementType>>? UpdateViewCalled;

        protected void UpdateView(IEnumerable<MeasurementType> measurementTypes)
        {
            UpdateViewCalled?.Invoke(measurementTypes);
        }
    }
}
