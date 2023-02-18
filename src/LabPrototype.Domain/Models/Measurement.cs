namespace LabPrototype.Domain.Models
{
    public class Measurement
    {
        public DateTime DateTime { get; set; }
        public double Q1 { get;}
        public double Q2 { get; }
        public double DeltaQ => Math.Abs(Q1 - Q2);
        public double P { get; }
        public double T { get; }

        public Measurement()
        {

        }

        public Measurement(double q1, double q2, double p, double t)
        {
            Q1 = q1;
            Q2 = q2;
            P = p;
            T = t;
        }
    }
}
