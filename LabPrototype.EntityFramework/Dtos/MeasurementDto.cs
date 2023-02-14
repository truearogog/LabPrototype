using System.ComponentModel.DataAnnotations;

namespace LabPrototype.EntityFramework.Dtos
{
    public class MeasurementDto
    {
        [Key]
        public Guid Id { get; set; }
        public float Q1 { get; }
        public float Q2 { get; }
        public float DeltaQ { get; }
        public float P1 { get; }
        public float P2 { get; }
        public float P { get; }
        public float t1 { get; }
        public float t2 { get; }
        public float t3 { get; }
        public float p1 { get; }
        public float p2 { get; }

        public Guid MeterId { get; set; }
        public MeterDto Meter { get; set; }
    }
}
