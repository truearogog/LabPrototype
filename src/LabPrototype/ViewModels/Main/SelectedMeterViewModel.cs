using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Dialogs;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Main
{
    public class SelectedMeterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IMeterService _meterService;
        private readonly ISelectedMeterService _selectedMeterService;
        public Meter SelectedMeter => _selectedMeterService.SelectedMeter;

        public MeterDetailsViewModel MeterDetailsViewModel { get; }

        public ICommand OpenUpdateMeterCommand { get; }
        public ICommand OpenDeleteMeterCommand { get; }

        public SelectedMeterViewModel(IDialogService dialogService, IMeterService meterService, ISelectedMeterService selectedmeterService)
        {
            _dialogService = dialogService;
            _meterService = meterService;
            _selectedMeterService = selectedmeterService;
            _selectedMeterService.SubscribeSelectedMeterChanged(SelectedmeterService_SelectedMeterChanged);

            MeterDetailsViewModel = new MeterDetailsViewModel();

            OpenUpdateMeterCommand = ReactiveCommand.CreateFromTask(ShowUpdateMeterDialogAsync);
            OpenDeleteMeterCommand = ReactiveCommand.CreateFromTask(ShowDeleteMeterDialogAsync);
        }

        private async Task ShowUpdateMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(UpdateMeterDialogViewModel), parameter);
        }

        private async Task ShowDeleteMeterDialogAsync()
        {
            MeterNavigationParameter parameter = new MeterNavigationParameter(SelectedMeter);
            await _dialogService.ShowDialogAsync(nameof(DeleteMeterDialogViewModel), parameter);
        }

        private void SelectedmeterService_SelectedMeterChanged()
        {
            this.RaisePropertyChanged(nameof(SelectedMeter));

            MeterDetailsViewModel.Meter = SelectedMeter;
        }
    }
}
