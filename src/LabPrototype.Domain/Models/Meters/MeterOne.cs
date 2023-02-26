namespace LabPrototype.Domain.Models.Meters
{
    public class MeterOne : Meter
    {
        protected override List<MeasurementAttribute> _measurementAttributes { get; } = new List<MeasurementAttribute>()
        {
            new MeasurementAttribute("Q1",  "m³/h", x => x.Q1,      ColorScheme.Red),
            new MeasurementAttribute("Q2",  "m³/h", x => x.Q2,      ColorScheme.Blue),
            new MeasurementAttribute("ΔQ",  "m³/h", x => x.DeltaQ,  ColorScheme.Yellow),
            new MeasurementAttribute("P",   "MW",   x => x.P,       ColorScheme.Purple),
            new MeasurementAttribute("T",   "°C",   x => x.T,       ColorScheme.Green),
        };

        public MeterOne(Guid id, string serialCode, string name, string address) : base(id, serialCode, name, address, MeterType.MeterOne.Id)
        {

        }
    }
}
