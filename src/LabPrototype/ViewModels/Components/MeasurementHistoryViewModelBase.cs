using LabPrototype.Domain.Models;
using ReactiveUI;
using System;

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

        public event Action<Meter> UpdateViewCalled;

        protected void UpdateView(Meter meter)
        {
            UpdateViewCalled?.Invoke(meter);
        }
    }
}
