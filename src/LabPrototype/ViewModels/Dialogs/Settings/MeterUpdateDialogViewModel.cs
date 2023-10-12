using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Components.Settings;
using LabPrototype.ViewModels.Models;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs.Settings
{
    public class MeterUpdateDialogViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<Meter>>
    {
        public MeterFormUpdateViewModel MeterFormUpdateViewModel { get; set; } = new();

        public ICommand? UpdateCommand { get; private set; }
        public ICommand? CancelCommand { get; private set; }

        public MeterUpdateDialogViewModel() : base()
        {
            UpdateCommand = ReactiveCommand.Create(Update);
            CancelCommand = CloseCommand;
        }

        public override void Activate(ModelNavigationParameter<Meter> parameter)
        {
            MeterFormUpdateViewModel.Model = parameter.Model!;
        }

        private void Update()
        {
            if (MeterFormUpdateViewModel.Update())
            {
                Close();
            }
        }
    }
}
