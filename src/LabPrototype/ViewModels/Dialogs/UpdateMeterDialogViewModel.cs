using LabPrototype.Domain.IServices;
using LabPrototype.ViewModels.Components;
using LabPrototype.ViewModels.Models;
using System;

namespace LabPrototype.ViewModels.Dialogs
{
    public class UpdateMeterDialogViewModel : ParametrizedDialogViewModelBase<MeterNavigationParameter>
    {
        public MeterDetailFormViewModel MeterDetailFormViewModel { get; }

        public UpdateMeterDialogViewModel()
        {
            var meterService = GetRequiredService<IMeterService>();

            MeterDetailFormViewModel = new MeterDetailFormViewModel(CloseCommand, (meterStore, meter) => {
                meterStore.Update(meterService, meter);
                Close();
            });
        }

        public override void Dispose()
        {
            MeterDetailFormViewModel.Dispose();

            base.Dispose();
        }

        public override void Activate(MeterNavigationParameter parameter)
        {
            MeterDetailFormViewModel.Meter = parameter.Meter ?? throw new Exception();
        }
    }
}
