using AutoMapper;
using LabPrototype.Domain.IRepositories;
using LabPrototype.Domain.IServices;
using LabPrototype.Domain.Models.Entities;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Services
{
    public class ColorSchemeService : ServiceBase<ColorSchemeEntity, ColorScheme, IColorSchemeRepository>, IColorSchemeService
    {
        public ColorSchemeService(IMapper mapper, IColorSchemeRepository repository) : base(mapper, repository)
        {
        }
    }
}
