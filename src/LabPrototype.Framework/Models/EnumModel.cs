namespace LabPrototype.Framework.Models
{
    public readonly record struct EnumModel<TEnum>(string Name, TEnum Value)
        where TEnum : Enum
    {
    }
}
