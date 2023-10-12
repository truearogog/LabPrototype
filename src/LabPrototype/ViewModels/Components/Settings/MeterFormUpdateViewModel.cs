using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterFormUpdateViewModel : MeterFormBaseViewModel
    {
        private readonly IMeterService _meterService;
        private readonly IMeasurementTypeService _measurementTypeService;
        private readonly IMeasurementTypeStore _measurementTypeStore;
        public ObservableCollection<MeasurementTypeFormViewModel> MeasurementTypeForms { get; } = new();

        public MeterFormUpdateViewModel() : base()
        {
            _meterService = GetRequiredService<IMeterService>();
            _measurementTypeService = GetRequiredService<IMeasurementTypeService>();
            _measurementTypeStore = GetRequiredService<IMeasurementTypeStore>();
        }

        public bool Update()
        {
            var updatedMeter = Submit((store, model) => store.Update(_meterService, model));
            if (updatedMeter is not null)
            {
                foreach (var measurementTypeForm in MeasurementTypeForms)
                {
                    if (!measurementTypeForm.Validate().Any())
                    {
                        var model = Mapper.Map<MeasurementType>(measurementTypeForm);
                        _measurementTypeStore.Update(_measurementTypeService, model);
                    }
                }

                return true;
            }
            return false;
        }

        protected override void OnModelSet()
        {
            base.OnModelSet();

            MeasurementTypeForms.Clear();
            var measurementTypes = _meterService.GetMeasurementTypes(Model.Id);
            foreach (var measurementType in measurementTypes)
            {
                var form = Mapper.Map<MeasurementTypeFormViewModel>(measurementType);
                MeasurementTypeForms.Add(form);
            }
        }
    }
}
