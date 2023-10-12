using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementGroupRepository _measurementGroupRepository;
        private readonly IMeterService _meterService;

        public struct _MeasurementGroup
        {
            public DateTime DateTime { get; set; }
            public double[] Values { get; set; }
        }

        public IEnumerable<_MeasurementGroup>? MeasurementGroups { get; set; }

        public MeasurementHistoryTableViewModel()
        {
            _measurementGroupRepository = GetRequiredService<IMeasurementGroupRepository>();
            _meterService = GetRequiredService<IMeterService>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void Update(Meter? meter, Archive? archive, MeasurementDisplayMode displayMode)
        {
            if (meter is not null && archive is not null)
            {
                var measurementGroups = _measurementGroupRepository.GetAll().Where(x => x.MeasurementGroupArchiveId.Equals(archive.Id)).ToList();
                if (measurementGroups.Any())
                {
                    // get current schema measurement types
                    var measurementTypes = _meterService.GetMeasurementTypes(meter.Id);
                    var measurementGroupArray = new _MeasurementGroup[measurementGroups.Count];
                    var measurementGroupIndex = 0;
                    foreach (var measurementGroup in measurementGroups)
                    {
                        var group = displayMode.ValueSelector?.Invoke(measurementGroup)!.ToArray();
                        measurementGroupArray[measurementGroupIndex++] = new()
                        {
                            DateTime = measurementGroup.DateTime,
                            Values = group!
                        };
                    }

                    MeasurementGroups = measurementGroupArray;
                    UpdateView(measurementTypes);
                    this.RaisePropertyChanged(nameof(MeasurementGroups));
                }
            }
        }
    }
}
