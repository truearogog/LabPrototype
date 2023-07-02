using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.SettingsListingItems
{
    public class SettingsListingItemViewModelBase<T> : ViewModelBase
        where T : PresentationModelBase
    {
        private T? _model = default;
        public T? Model
        {
            get => _model; 
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        public ICommand? OpenUpdateModelCommand { get; private set; }
        public ICommand? OpenDeleteModelCommand { get; private set; }

        public void Activate(T model, ICommand updateCommand, ICommand deleteCommand)
        {
            Model = model;

            OpenUpdateModelCommand = updateCommand;
            this.RaisePropertyChanged(nameof(OpenUpdateModelCommand));
            OpenDeleteModelCommand = deleteCommand;
            this.RaisePropertyChanged(nameof(OpenDeleteModelCommand));
        }
    }
}
