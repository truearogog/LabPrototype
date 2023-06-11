using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;

namespace LabPrototype.AppManagers.Stores
{
    public class ColorSchemeStore : StoreBase<ColorScheme>, IColorSchemeStore
    {
        public ColorSchemeStore(IColorSchemeService service) : base(service)
        {
        }
    }
}
