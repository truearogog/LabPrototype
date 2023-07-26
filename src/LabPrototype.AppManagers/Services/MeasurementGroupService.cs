using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementGroupService : 
        ServiceBase<MeasurementGroupEntity, MeasurementGroup, IMeasurementGroupRepository>, 
        IMeasurementGroupService
    {
        public MeasurementGroupService(IMapper mapper, IMeasurementGroupRepository repository) : base(mapper, repository)
        {
        }
    }
}
