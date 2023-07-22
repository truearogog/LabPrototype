using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Domain.Models.Presentation.Measurements;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabPrototype.ViewModels.Components
{
    public class MeasurementHistoryTableViewModel : MeasurementHistoryViewModelBase
    {
        private readonly IMeasurementGroupService _measurementGroupService;
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeterTypeMeasurementTypeService _meterTypeMeasurementTypeService;

        public struct _MeasurementGroup
        {
            public DateTime DateTime { get; set; }
            public double[] Values { get; set; }

            public _MeasurementGroup(DateTime dateTime, int count)
            {
                DateTime = dateTime;
                Values = new double[count];
            }
        }

        public IEnumerable<_MeasurementGroup>? MeasurementGroups { get; set; }

        public MeasurementHistoryTableViewModel()
        {
            _measurementGroupService = GetRequiredService<IMeasurementGroupService>();
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _meterTypeMeasurementTypeService = GetRequiredService<IMeterTypeMeasurementTypeService>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void Update(Meter? meter, MeasurementGroupArchive? archive, Func<Measurement, double>? valueSelector = null)
        {
            if (meter is not null && archive is not null)
            {
                var measurementGroups = _measurementGroupService.GetAll(x => x.MeasurementGroupArchiveId.Equals(archive.Id));
                if (measurementGroups.Any())
                {
                    var meterTypeMeasurementTypes = _meterTypeMeasurementTypeService.GetAll(x => x.MeterTypeId.Equals(meter.Id));

                    // measurement types ordered by sort order
                    var measurementTypes = 
                        _meterTypeService
                        .GetMeasurementTypes(meter.MeterTypeId)
                        .OrderBy(x => meterTypeMeasurementTypes.FirstOrDefault(y => y.MeasurementTypeId.Equals(x.Id))?.SortOrder ?? int.MaxValue);

                    // create dictionary to know which mt in what place
                    var measurementTypeDictionary = new Dictionary<int, int>();
                    var i = 0;
                    foreach (var measurementType in measurementTypes)
                    {
                        measurementTypeDictionary[measurementType.Id] = i++;
                    }

                    // create sorted arrays of measurenents
                    var measurementGroupArray = new _MeasurementGroup[measurementGroups.Count()];
                    i = 0;
                    foreach (var measurementGroup in measurementGroups)
                    {
                        measurementGroupArray[i] = new _MeasurementGroup(measurementGroup.DateTime, measurementTypes.Count());
                        foreach (var measurement in measurementGroup.Measurements ?? Enumerable.Empty<Measurement>())
                        {
                            var index = measurementTypeDictionary[measurement.MeasurementTypeId];
                            measurementGroupArray[i].Values[index] = valueSelector?.Invoke(measurement) ?? 0d;
                        }
                        i++;
                    }

                    MeasurementGroups = measurementGroupArray;
                    UpdateView(measurementTypes.AsEnumerable());
                    this.RaisePropertyChanged(nameof(MeasurementGroups));
                }
            }
        }
    }
}
