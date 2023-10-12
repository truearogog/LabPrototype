using LabPrototype.ViewModels.Components.Settings;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs.Settings
{
    public class MeterCreateDialogViewModel : DialogViewModelBase
    {
        public MeterFormCreateViewModel MeterFormCreateViewModel { get; set; } = new();

        public ICommand? CreateCommand { get; private set; }
        public ICommand? CancelCommand { get; private set; }

        public MeterCreateDialogViewModel() : base()
        {
            CreateCommand = ReactiveCommand.Create(Create);
            CancelCommand = CloseCommand;
        }

        public void Create()
        {
            if (MeterFormCreateViewModel.Create())
            {
                Close();
            }
        }
    }
}
