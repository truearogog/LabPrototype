using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeasurementGroupArchive_Profile : Profile
    {
        public MeasurementGroupArchive_Profile()
        {
            CreateMap<MeasurementGroupArchive, MeasurementGroupArchiveEntity>();
            CreateMap<MeasurementGroupArchiveEntity, MeasurementGroupArchive>();
        }
    }
}
