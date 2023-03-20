using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.Services.Interfaces
{
    public interface ISelectedMeterService
    {
        event Action<Meter> SelectedMeterUpdated;

        Meter SelectedMeter { get; set; }
    }
}
