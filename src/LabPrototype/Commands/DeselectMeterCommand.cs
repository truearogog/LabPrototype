using LabPrototype.Services.Interfaces;

namespace LabPrototype.Commands
{
    public class DeselectMeterCommand : CommandBase
    {
        private readonly ISelectedMeterService _selectedMeterService;

        public DeselectMeterCommand(ISelectedMeterService selectedMeterService)
        {
            _selectedMeterService = selectedMeterService;
        }

        public override void Execute(object? parameter)
        {
            _selectedMeterService.SelectedMeter = null;
        }
    }
}
