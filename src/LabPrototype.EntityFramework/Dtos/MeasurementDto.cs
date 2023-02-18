using System.ComponentModel.DataAnnotations;

namespace LabPrototype.EntityFramework.Dtos
{
    public class MeasurementDto
    {
        [Key]
        public Guid Id { get; set; }
        public double Q1 { get; }
        public double Q2 { get; }
        public double DeltaQ { get; }
        public double P1 { get; }
        public double P2 { get; }
        public double P { get; }
        public double t1 { get; }
        public double t2 { get; }
        public double t3 { get; }
        public double p1 { get; }
        public double p2 { get; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public Guid MeterId { get; set; }
        public MeterDto Meter { get; set; }
    }
}
