using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterService : ServiceBase<MeterEntity, Meter, IMeterRepository>, IMeterService
    {
        public MeterService(IMapper mapper, IMeterRepository repository) : base(mapper, repository)
        {
        }
    }
}
