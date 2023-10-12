using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class Archive_Profile : Profile
    {
        public Archive_Profile()
        {
            CreateMap<Archive, ArchiveEntity>();
            CreateMap<ArchiveEntity, Archive>();
        }
    }
}
