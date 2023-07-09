using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeasurementTypeForm_Profile : Profile
    {
        public MeasurementTypeForm_Profile()
        {
            CreateMap<MeasurementType, MeasurementTypeForm>();
            CreateMap<MeasurementTypeForm, MeasurementType>();
        }
    }
}
