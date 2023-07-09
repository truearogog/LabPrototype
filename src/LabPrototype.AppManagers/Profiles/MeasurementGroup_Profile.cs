using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeasurementGroup_Profile : Profile
    {
        public MeasurementGroup_Profile()
        {
            CreateMap<MeasurementGroup, MeasurementGroupEntity>();
            CreateMap<MeasurementGroupEntity, MeasurementGroup>();
        }
    }
}
