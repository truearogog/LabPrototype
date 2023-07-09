using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeterTypeMeasurementTypeForm_Profile : Profile
    {
        public MeterTypeMeasurementTypeForm_Profile()
        {
            CreateMap<MeterTypeMeasurementType, MeterTypeMeasurementTypeForm>();
            CreateMap<MeterTypeMeasurementTypeForm, MeterTypeMeasurementType>();
        }
    }
}
