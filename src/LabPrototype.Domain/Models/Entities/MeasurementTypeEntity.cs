namespace LabPrototype.Domain.Models.Entities
{
    public class MeasurementTypeEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeasurementGroupSchemaMeasurementTypeEntity> MeasurementGroupSchemaMeasurementTypes { get; set; } 
            = new List<MeasurementGroupSchemaMeasurementTypeEntity>();
    }
}
