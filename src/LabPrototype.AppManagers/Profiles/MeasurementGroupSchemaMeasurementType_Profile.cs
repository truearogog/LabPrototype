using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeasurementGroupSchemaMeasurementType_Profile : Profile
    {
        public MeasurementGroupSchemaMeasurementType_Profile()
        {
            CreateMap<MeasurementGroupSchemaMeasurementType, MeasurementGroupSchemaMeasurementTypeEntity>();
            CreateMap<MeasurementGroupSchemaMeasurementTypeEntity, MeasurementGroupSchemaMeasurementType>();
        }
    }
}
