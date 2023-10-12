using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System.Windows.Input;
using System;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterListingItemViewModel : ViewModelBase
    {
        private Meter? _model = default;
        public Meter? Model
        {
            get => _model;
            set
            {
                this.RaiseAndSetIfChanged(ref _model, value);
                CreateCommands();
            }
        }

        private Func<Meter, ICommand>? _updateCommandFactory;
        private Func<Meter, ICommand>? _deleteCommandFactory;

        public ICommand? OpenUpdateModelCommand { get; private set; }
        public ICommand? OpenDeleteModelCommand { get; private set; }

        public void Activate(Func<Meter, ICommand> updateCommandFactory, Func<Meter, ICommand> deleteCommandFactory)
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
