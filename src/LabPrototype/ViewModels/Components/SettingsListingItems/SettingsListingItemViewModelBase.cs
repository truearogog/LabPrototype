using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
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
            set
            {
                this.RaiseAndSetIfChanged(ref _model, value);
                CreateCommands();
            }
        }

        private Func<T, ICommand>? _updateCommandFactory;
        private Func<T, ICommand>? _deleteCommandFactory;

        public ICommand? OpenUpdateModelCommand { get; private set; }
        public ICommand? OpenDeleteModelCommand { get; private set; }

        public void Activate(Func<T, ICommand> updateCommandFactory, Func<T, ICommand> deleteCommandFactory)
        {
            _updateCommandFactory = updateCommandFactory;
            _deleteCommandFactory = deleteCommandFactory;
            CreateCommands();
        }

        private void CreateCommands()
        {
            if (_model is not null)
            {
                OpenUpdateModelCommand = _updateCommandFactory?.Invoke(_model);
                this.RaisePropertyChanged(nameof(OpenUpdateModelCommand));
                OpenDeleteModelCommand = _deleteCommandFactory?.Invoke(_model);
                this.RaisePropertyChanged(nameof(OpenDeleteModelCommand));
            }
        }
    }
}
