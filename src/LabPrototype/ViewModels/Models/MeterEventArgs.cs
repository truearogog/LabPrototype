using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.ViewModels.Models
{
    public class MeterEventArgs : EventArgs
    {
        public Meter Meter { get; set; }
        public MeterEventArgs(Meter meter)
        {
            Meter = meter;
        }
    }
}
