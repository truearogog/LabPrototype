using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeterType_Profile : Profile
    {
        public MeterType_Profile()
        {
            CreateMap<MeterType, MeterTypeEntity>();
            CreateMap<MeterTypeEntity, MeterType>();
        }
    }
}
