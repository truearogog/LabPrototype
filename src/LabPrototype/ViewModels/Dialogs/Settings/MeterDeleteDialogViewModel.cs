using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Models;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs.Settings
{
    public class MeterDeleteDialogViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<Meter>>
    {
        private readonly IMeterService _meterService;
        private readonly IMeterStore _meterStore;

        private Meter _model = new();
        public Meter Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        public ICommand? DeleteCommand { get; }
        public ICommand? CancelCommand { get; }

        public MeterDeleteDialogViewModel()
        {
            _meterService = GetRequiredService<IMeterService>();
            _meterStore = GetRequiredService<IMeterStore>();

            DeleteCommand = ReactiveCommand.Create(Delete);
            CancelCommand = CloseCommand;
        }

        public override void Activate(ModelNavigationParameter<Meter> parameter)
        {
            Model = parameter.Model!;
        }

        private void Delete()
        {
            _meterStore.Delete(_meterService, Model.Id);
            Close();
        }
    }
}
