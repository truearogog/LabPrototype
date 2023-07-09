using AutoMapper;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models.Forms;

namespace LabPrototype.Models.Profiles
{
    public class ColorSchemeForm_Profile : Profile
    {
        public ColorSchemeForm_Profile()
        {
            CreateMap<ColorScheme, ColorSchemeForm>();
            CreateMap<ColorSchemeForm, ColorScheme>();
        }
    }
}
