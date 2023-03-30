using ReactiveUI;

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
    }
}
