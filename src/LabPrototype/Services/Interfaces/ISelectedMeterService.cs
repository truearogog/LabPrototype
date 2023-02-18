using LabPrototype.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Services.Interfaces
{
    public interface ISelectedMeterService
    {
        Meter SelectedMeter { get; set; }
        void SubscribeSelectedMeterUpdated(Action handler);
        void UnsubscribeSelectedMeterUpdated(Action handler);
    }
}
