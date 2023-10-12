using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class ArchiveService : 
        ServiceBase<ArchiveEntity, Archive, IArchiveRepository>, 
        IArchiveService
    {
        public ArchiveService(IMapper mapper, IArchiveRepository repository) : base(mapper, repository)
        {
        }
    }
}
