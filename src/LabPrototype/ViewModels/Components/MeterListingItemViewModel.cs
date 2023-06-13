using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Main;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views.Windows;
using ReactiveUI;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components
{
    public class MeterListingItemViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;

        private Meter _meter;
        public Meter Meter
        {
            get => _meter;
            set => this.RaiseAndSetIfChanged(ref _meter, value);
        }

        public ICommand OpenMeterCommand { get; }

        public MeterListingItemViewModel(Meter meter)
        {
            _windowService = GetRequiredService<IWindowService>();

            Meter = meter;

            OpenMeterCommand = ReactiveCommand.Create(OpenMeter);
        }

        private void OpenMeter()
        {
            var parameter = new MeterNavigationParameter(Meter);
            _windowService.ShowWindow<MeterWindow, MeterWindowViewModel, MeterNavigationParameter>(parameter);
        }
    }
}
