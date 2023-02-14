using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Domain.Models
{
    public class Measurement
    {
        public DateTime DateTime { get; }
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
    }
}
