﻿using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.Domain.IServices
{
    public interface IMeterService : IServiceBase<Meter>
    {
        IEnumerable<MeasurementType> GetMeasurementTypes(int id);
    }
}
