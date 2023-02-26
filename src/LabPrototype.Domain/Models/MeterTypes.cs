using LabPrototype.Domain.Models.Meters;

namespace LabPrototype.Domain.Models
{
    public readonly partial struct MeterType
    {
        public static MeterType MeterOne { get; } = new MeterType(1, "Meter one", "Big meter!", typeof(MeterOne));
        public static MeterType MeterTwo { get; } = new MeterType(2, "Meter two", "Small meter...", typeof(MeterTwo));
        public static List<MeterType> All { get; } = new List<MeterType>() { MeterOne, MeterTwo };
    }
}
