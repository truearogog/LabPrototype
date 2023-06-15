using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementTypeService : ServiceBase<MeasurementTypeEntity, MeasurementType, IMeasurementTypeRepository>, IMeasurementTypeService
    {
        public MeasurementTypeService(IMapper mapper, IMeasurementTypeRepository repository) : base(mapper, repository)
        {
        }
    }
}
