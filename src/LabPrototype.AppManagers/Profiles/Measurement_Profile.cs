using AutoMapper;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class Measurement_Profile : Profile
    {
        public Measurement_Profile()
        {
            CreateMap<Measurement, MeasurementEntity>();
            CreateMap<MeasurementEntity, Measurement>();
        }
    }
}
