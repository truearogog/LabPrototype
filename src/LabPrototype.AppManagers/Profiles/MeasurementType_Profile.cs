using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeasurementType_Profile : Profile
    {
        public MeasurementType_Profile()
        {
            CreateMap<MeasurementType, MeasurementTypeEntity>();
            CreateMap<MeasurementTypeEntity, MeasurementType>();
        }
    }
}
