using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MainViewModel : ViewModelBase
    {
        public MeterListingViewModel MeterListingViewModel { get; }

        public ICommand LoadMetersCommand { get; }

        public MainViewModel(IWindowService dialogService, IMeterService meterService)
        {

            MeterListingViewModel = new MeterListingViewModel(dialogService, meterService);

            LoadMetersCommand = new LoadMetersCommand(meterService);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public static MainViewModel LoadViewModel(IWindowService dialogService, IMeterService meterService)
        {
            var viewModel = new MainViewModel(dialogService, meterService);
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}
