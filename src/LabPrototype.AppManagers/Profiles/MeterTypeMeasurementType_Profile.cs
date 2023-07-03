using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeterTypeMeasurementType_Profile : Profile
    {
        public MeterTypeMeasurementType_Profile()
        {
            CreateMap<MeterTypeMeasurementType, MeterTypeMeasurementTypeEntity>();
            CreateMap<MeterTypeMeasurementTypeEntity, MeterTypeMeasurementType>();
        }
    }
}
