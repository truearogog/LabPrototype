using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementService : ServiceBase<MeasurementEntity, Measurement, IMeasurementRepository>, IMeasurementService
    {
        public MeasurementService(IMapper mapper, IMeasurementRepository repository) : base(mapper, repository)
        {
        }
    }
}
