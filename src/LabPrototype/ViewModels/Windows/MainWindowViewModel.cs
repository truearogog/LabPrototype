using LabPrototype.Commands;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MeterListingViewModel MeterListingViewModel { get; }

        public ICommand LoadMetersCommand { get; }

        public MainWindowViewModel()
        {
            MeterListingViewModel = new MeterListingViewModel(this);

            IMeterService meterService = GetRequiredService<IMeterService>();
            LoadMetersCommand = new LoadMetersCommand(meterService);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public static MainWindowViewModel LoadViewModel()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.LoadMetersCommand.Execute(null);
            return viewModel;
        }
    }
}