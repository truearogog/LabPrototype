using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementGroupSchemaMeasurementTypeService : 
        ServiceBase<MeasurementGroupSchemaMeasurementTypeEntity, MeasurementGroupSchemaMeasurementType, IMeasurementGroupSchemaMeasurementTypeRepository>, 
        IMeasurementGroupSchemaMeasurementTypeService
    {
        public MeasurementGroupSchemaMeasurementTypeService(IMapper mapper, IMeasurementGroupSchemaMeasurementTypeRepository repository) : base(mapper, repository)
        {
        }
    }
}
