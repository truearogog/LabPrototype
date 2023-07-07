using AutoMapper;
using LabPrototype.Domain.Entities;
using LabPrototype.Domain.Models.Forms;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Profiles
{
    public class ColorScheme_Profile : Profile
    {
        public ColorScheme_Profile()
        {
            CreateMap<ColorScheme, ColorSchemeEntity>();
            CreateMap<ColorSchemeEntity, ColorScheme>();

            CreateMap<ColorScheme, ColorSchemeForm>();
            CreateMap<ColorSchemeForm, ColorScheme>();
        }
    }
}
