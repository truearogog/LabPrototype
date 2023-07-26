using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class MeasurementGroupSchema_Profile : Profile
    {
        public MeasurementGroupSchema_Profile()
        {
            CreateMap<MeasurementGroupSchema, MeasurementGroupSchemaEntity>();
            CreateMap<MeasurementGroupSchemaEntity, MeasurementGroupSchema>();
        }
    }
}
