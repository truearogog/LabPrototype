using LabPrototype.DependencyInjection;
using LabPrototype.Domain.Models;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Dialogs;
using Serilog.Core;
using Splat;
using System;
using System.Threading.Tasks;

namespace LabPrototype.Commands
{
    public class UpdateMeterCommand : AsyncCommandBase
    {
        private readonly UpdateMeterDialogViewModel _updateMeterDialogViewModel;
        private readonly IMeterService _meterService;

        public UpdateMeterCommand(UpdateMeterDialogViewModel updateMeterDialogViewModel, IMeterService meterService)
        {
            _updateMeterDialogViewModel = updateMeterDialogViewModel;
            _meterService = meterService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            MeterDetailsFormViewModel formViewModel = _updateMeterDialogViewModel.MeterDetailsFormViewModel;

            try
            {
                Meter meter = new Meter(
                    formViewModel.Id,
                    formViewModel.SerialCode,
                    formViewModel.Name,
                    formViewModel.Address
                );

                await _meterService.Update(meter);

                _updateMeterDialogViewModel.CloseCommand.Execute(null);
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
