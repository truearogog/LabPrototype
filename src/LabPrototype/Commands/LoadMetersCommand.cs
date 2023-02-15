using LabPrototype.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LabPrototype.Commands
{
    public class LoadMetersCommand : AsyncCommandBase
    {
        private readonly IMeterService _meterService;

        public LoadMetersCommand(IMeterService meterService)
        {
            _meterService = meterService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _meterService.Load();
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }
    }
}
