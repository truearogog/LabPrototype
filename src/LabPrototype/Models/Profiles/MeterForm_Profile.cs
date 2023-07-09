using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class MeterForm_Profile : Profile
    {
        public MeterForm_Profile()
        {
            CreateMap<Meter, MeterForm>();
            CreateMap<MeterForm, Meter>();
        }
    }
}
