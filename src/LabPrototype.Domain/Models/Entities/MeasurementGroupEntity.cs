﻿namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementGroupEntity : EntityBase
    {        
        public DateTime DateTime { get; set; }
        public IEnumerable<double> AverageValues { get; set; } = Enumerable.Empty<double>();
        public IEnumerable<double> SummaryValues { get; set; } = Enumerable.Empty<double>();

        public int MeasurementGroupArchiveId { get; set; }
        public virtual MeasurementGroupArchiveEntity? MeasurementGroupArchive { get; set; }

        public int MeasurementGroupSchemaId { get; set; }
        public virtual MeasurementGroupSchemaEntity? MeasurementGroupSchema { get; set; }
    }
}
