namespace LabPrototype.Domain.Models
{
    public class MeasurementAttribute
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Unit { get; }
        public Func<Measurement, object> ValueGetter { get; }
        public ColorScheme ColorScheme { get; }

        public MeasurementAttribute(string name, string unit, Func<Measurement, object> valueGetter, ColorScheme colorScheme)
        {
            Name = name;
            Unit = unit;
            ValueGetter = valueGetter;
            ColorScheme = colorScheme;
        }
    }
}
