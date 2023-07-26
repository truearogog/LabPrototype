namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementGroupSchemaEntity : EntityBase
    {
        public int MeterTypeId { get; set; }
        public virtual MeterTypeEntity? MeterType { get; set; }

        public virtual ICollection<MeasurementGroupEntity> MeasurementGroups { get; set; } 
            = new List<MeasurementGroupEntity>();
        public virtual ICollection<MeasurementGroupSchemaMeasurementTypeEntity> MeasurementGroupSchemaMeasurementTypes { get; set; } 
            = new List<MeasurementGroupSchemaMeasurementTypeEntity>();
    }
}
