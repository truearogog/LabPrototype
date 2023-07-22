namespace LabPrototype.Domain.Models.Presentation
{
    public class MeasurementGroupArchive : PresentationModelBase
    {
        public string Name { get; set; } = string.Empty;
        public ushort DiscretizationMinutes { get; set; }
        public ushort MinDiscretizationMinutes { get; set; }
        public ushort MaxDescretizationMinutes { get; set; }
        public byte DiscretizationMonths { get; set; }
        public byte Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }
        public int MeterId { get; set; }
    }
}
