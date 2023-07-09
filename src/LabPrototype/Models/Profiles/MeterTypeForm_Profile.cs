using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeterTypeForm_Profile : Profile
    {
        public MeterTypeForm_Profile()
        {
            CreateMap<MeterType, MeterTypeForm>();
            CreateMap<MeterTypeForm, MeterType>();
        }
    }
}
