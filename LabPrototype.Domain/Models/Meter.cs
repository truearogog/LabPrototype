using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Domain.Models
{
    public class Meter
    {
        public Guid Id { get; set; }
        public string SerialCode { get; }
        public string Name { get; }
        public string Address { get; }

        public Meter()
        {

        }

        public Meter(Guid id, string serialCode, string name, string address)
        {
            Id = id;
            SerialCode = serialCode;
            Name = name;
            Address = address;
        }
    }
}
