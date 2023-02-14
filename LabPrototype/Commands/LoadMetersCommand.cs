using LabPrototype.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace LabPrototype.Commands
{
    public class LoadMetersCommand : AsyncCommandBase
    {
        private readonly IMeterService _meterStore;

        public LoadMetersCommand(IMeterService meterStore)
        {
            _meterStore = meterStore;
        }

        protected override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _meterStore.Load();
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
