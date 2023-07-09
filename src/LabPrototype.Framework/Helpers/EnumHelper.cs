using LabPrototype.Framework.Models;

namespace LabPrototype.Framework.Helpers
{
    public class EnumHelper
    {
        public static IEnumerable<EnumModel<TEnum>> GetEnumModels<TEnum>()
            where TEnum : struct, Enum
        {
            var values = Enum.GetValues(typeof(TEnum)) as TEnum[];
            var enumModels = values?.Select(x => new EnumModel<TEnum>(Enum.GetName(x) ?? string.Empty, x)) ?? Enumerable.Empty<EnumModel<TEnum>>();
            return enumModels;
        }
    }
}
