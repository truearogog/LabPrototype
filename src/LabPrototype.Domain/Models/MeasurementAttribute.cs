namespace LabPrototype.Domain.Models
{
    public class MeasurementAttribute
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public string Unit { get; }
        public Func<Measurement, object> ValueGetter { get; }
        public string BindingName { get; }
        public ColorScheme? ColorScheme { get; }

        public MeasurementAttribute(string name, string unit, Func<Measurement, object> valueGetter, string bindingName, ColorScheme? colorScheme)
        {
            Name = name;
            Unit = unit;
            ValueGetter = valueGetter;
            BindingName = bindingName;
            ColorScheme = colorScheme;
        }
    }
}
