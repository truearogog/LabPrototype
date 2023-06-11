using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterTypeService : ServiceBase<MeterTypeEntity, MeterType>, IMeterTypeService
    {
        public MeterTypeService(IMapper mapper, IMeterTypeRepository repository) : base(mapper, repository)
        {
        }
    }
}
