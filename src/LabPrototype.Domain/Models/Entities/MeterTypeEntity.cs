namespace LabPrototype.Domain.Models.Entities
{
    public class MeterTypeEntity : EntityBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int ColorSchemeId { get; set; }
        public virtual ColorSchemeEntity? ColorScheme { get; set; }

        public virtual ICollection<MeasurementGroupSchemaEntity> MeasurementGroupSchemas { get; set; } 
            = new List<MeasurementGroupSchemaEntity>();
        public virtual ICollection<MeterEntity> Meters { get; set; } 
            = new List<MeterEntity>();
    }
}
