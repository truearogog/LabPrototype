using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class MeasurementGroupArchiveService : 
        ServiceBase<MeasurementGroupArchiveEntity, MeasurementGroupArchive, IMeasurementGroupArchiveRepository>, 
        IMeasurementGroupArchiveService
    {
        public MeasurementGroupArchiveService(IMapper mapper, IMeasurementGroupArchiveRepository repository) : base(mapper, repository)
        {
        }
    }
}
