using ReactiveUI;

namespace LabPrototype.ViewModels
{
    public class ValueViewModelBase<T> : ViewModelBase
    {
        private T? _value = default;
        public T? Value
        {
            get => _value;
            set => this.RaiseAndSetIfChanged(ref _value, value);
        }
    }
}
