using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeterService : ServiceBase<MeterEntity, Meter>, IMeterService
    {
        public MeterService(IMapper mapper, IMeterRepository repository) : base(mapper, repository)
        {
        }
    }
}
