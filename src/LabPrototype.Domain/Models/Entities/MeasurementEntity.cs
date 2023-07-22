namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementEntity : EntityBase
    {
        public double Average { get; set; }
        public double Summary { get; set; }

        public int MeasurementTypeId { get; set; }
        public virtual MeasurementTypeEntity? MeasurementType { get; set; }

        public int MeasurementGroupId { get; set; }
        public virtual MeasurementGroupEntity? MeasurementGroup { get; set; }
    }
}
