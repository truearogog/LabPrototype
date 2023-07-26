namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementGroupSchemaMeasurementTypeEntity : EntityBase
    {
        public int MeasurementGroupSchemaId { get; set; }
        public virtual MeasurementGroupSchemaEntity? MeasurementGroupSchema { get; set; }

        public int MeasurementTypeId { get; set; }
        public virtual MeasurementTypeEntity? MeasurementType { get; set; }
    }
}
