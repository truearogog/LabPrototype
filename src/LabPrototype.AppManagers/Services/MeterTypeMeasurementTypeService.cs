using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterTypeMeasurementTypeService : ServiceBase<MeterTypeMeasurementTypeEntity, MeterTypeMeasurementType, IMeterTypeMeasurementTypeRepository>, IMeterTypeMeasurementTypeService
    {
        public MeterTypeMeasurementTypeService(IMapper mapper, IMeterTypeMeasurementTypeRepository repository) : base(mapper, repository)
        {
        }
    }
}
