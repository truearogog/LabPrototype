using LabPrototype.Domain.Models;
using System;

namespace LabPrototype.Services.Interfaces
{
    public interface ISelectedMeterService
    {
        Meter SelectedMeter { get; set; }
        void SubscribeSelectedMeterUpdated(Action handler);
        void UnsubscribeSelectedMeterUpdated(Action handler);
    }
}
