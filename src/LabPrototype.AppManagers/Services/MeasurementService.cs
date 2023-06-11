using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementService : ServiceBase<MeasurementEntity, Measurement>, IMeasurementService
    {
        public MeasurementService(IMapper mapper, IMeasurementRepository repository) : base(mapper, repository)
        {
        }
    }
}
