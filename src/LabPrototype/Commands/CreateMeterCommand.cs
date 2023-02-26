using LabPrototype.DependencyInjection;
using LabPrototype.Domain.Factories;
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
    public class CreateMeterCommand : AsyncCommandBase
    {
        private readonly CreateMeterDialogViewModel _createMeterDialogViewModel;
        private readonly IMeterService _meterService;

        public CreateMeterCommand(CreateMeterDialogViewModel createMeterDialogViewModel, IMeterService meterService)
        {
            _createMeterDialogViewModel = createMeterDialogViewModel;
            _meterService = meterService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            MeterDetailFormViewModel formViewModel = _createMeterDialogViewModel.MeterDetailFormViewModel;

            try
            {
                Meter meter = MeterFactory.CreateMeter(
                    Guid.NewGuid(),
                    formViewModel.SerialCode,
                    formViewModel.Name,
                    formViewModel.Address,
                    MeterType.All[formViewModel.SelectedMeterTypeIndex].Id
                );

                await _meterService.Create(meter);

                _createMeterDialogViewModel.CloseCommand.Execute(default);
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
