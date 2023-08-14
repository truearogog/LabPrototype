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
        private readonly IMeterTypeService _meterTypeService;
        private readonly IMeasurementGroupSchemaService _measurementGroupSchemaService;

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
            _measurementGroupRepository = GetRequiredService<IMeasurementGroupRepository>();
            _meterTypeService = GetRequiredService<IMeterTypeService>();
            _measurementGroupSchemaService = GetRequiredService<IMeasurementGroupSchemaService>();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public void Update(Meter? meter, MeasurementGroupArchive? archive, MeasurementDisplayMode displayMode)
        {
            if (meter is not null && archive is not null)
            {
                var measurementGroups = _measurementGroupRepository.GetAll().Where(x => x.MeasurementGroupArchiveId.Equals(archive.Id)).ToList();
                if (measurementGroups.Any())
                {
                    // create dictionary that contains schema id -> measurement type -> from to index
                    var schemas = _measurementGroupSchemaService.GetAll(x => x.MeterTypeId.Equals(meter.MeterTypeId));
                    var currentSchema = schemas.OrderByDescending(x => x.Created).First();
                    var schemaTypeIndexes = new Dictionary<int, Dictionary<int, int>>();
                    foreach (var schema in schemas)
                    {
                        var typeIndexes = new Dictionary<int, int>();
                        var measurements = _measurementGroupSchemaService.GetMeasurementTypes(schema.Id);
                        var i = 0;
                        foreach (var measurement in measurements)
                        {
                            typeIndexes.Add(measurement.Id, i++);
                        }
                        schemaTypeIndexes.Add(schema.Id, typeIndexes);
                    }

                    // get current schema measurement types
                    var measurementTypes = _meterTypeService.GetMeasurementTypes(meter.MeterTypeId);
                    var measurementGroupArray = new _MeasurementGroup[measurementGroups.Count()];
                    var j = 0;
                    foreach (var measurementGroup in measurementGroups)
                    {
                        var group = displayMode.ValueSelector?.Invoke(measurementGroup);
                        var _measurementGroup = new _MeasurementGroup(measurementGroup.DateTime, group?.Count() ?? 0);
                        foreach (var measurementType in measurementTypes)
                        {
                            var index = schemaTypeIndexes[measurementGroup.MeasurementGroupSchemaId][measurementType.Id];
                            var currentIndex = schemaTypeIndexes[currentSchema.Id][measurementType.Id];
                            _measurementGroup.Values[currentIndex] = group?.ElementAt(index) ?? 0;
                        }
                        measurementGroupArray[j++] = _measurementGroup;
                    }

                    MeasurementGroups = measurementGroupArray;
                    UpdateView(measurementTypes);
                    this.RaisePropertyChanged(nameof(MeasurementGroups));
                }
            }
        }
    }
}
