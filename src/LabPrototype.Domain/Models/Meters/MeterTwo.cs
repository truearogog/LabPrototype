namespace LabPrototype.Domain.Models.Meters
{
    public class MeterTwo : Meter
    {
        protected override List<MeasurementAttribute> _measurementAttributes { get; } = new List<MeasurementAttribute>()
        {
            new MeasurementAttribute("Q1",  "m³/h", x => x.Q1,      ColorScheme.Red),
            new MeasurementAttribute("Q2",  "m³/h", x => x.Q2,      ColorScheme.Blue),
            new MeasurementAttribute("ΔQ",  "m³/h", x => x.DeltaQ,  ColorScheme.Yellow),
        };

        public MeterTwo(Guid id, string serialCode, string name, string address) : base(id, serialCode, name, address, MeterType.MeterTwo.Id)
        {

        }
    }
}
