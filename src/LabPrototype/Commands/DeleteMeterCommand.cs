using LabPrototype.DependencyInjection;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using Serilog.Core;
using Splat;
using System;
using System.Threading.Tasks;

namespace LabPrototype.Commands
{
    public class DeleteMeterCommand : AsyncCommandBase
    {
        private readonly DeleteMeterDialogViewModel _deleteMeterDialogViewModel;
        private readonly IMeterService _meterService;

        public DeleteMeterCommand(DeleteMeterDialogViewModel deleteMeterDialogViewModel, IMeterService meterService)
        {
            _deleteMeterDialogViewModel = deleteMeterDialogViewModel;
            _meterService = meterService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Meter meter = _deleteMeterDialogViewModel.Meter;

            try
            {
                await _meterService.Delete(meter.Id);

                _deleteMeterDialogViewModel.CloseCommand.Execute(null);
            }
            catch (Exception ex)
            {
                var logger = Locator.Current.GetRequiredService<Logger>();
                logger.Error(ex.Message);


            }
            finally
            {

            }
        }
    }
}
