using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementGroupService _measurementGroupService;
        private readonly IMeterTypeService _meterTypeService;

        public ObservableCollection<MeasurementGroup> MeasurementGroups { get; set; } = new();

        public MeasurementHistoryTableViewModel()
        {
            _measurementGroupService = GetRequiredService<IMeasurementGroupService>();
            _meterTypeService = GetRequiredService<IMeterTypeService>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void UpdateMeter(Meter? meter)
        {
            if (meter is not null)
            {
                var measurementGroups = _measurementGroupService.GetAll(x => x.MeterId.Equals(meter.Id));
                if (measurementGroups.Any())
                {
                    MeasurementGroups = new(measurementGroups);
                    var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                    var measurementTypeIds = measurementGroups.First().Measurements?.Select(x => x.MeasurementTypeId) ?? throw new Exception();
                    UpdateView(measurementTypes, measurementTypeIds);
                    this.RaisePropertyChanged(nameof(MeasurementGroups));
                }
            }
        }
    }
}
