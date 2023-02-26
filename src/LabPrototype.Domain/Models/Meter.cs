namespace LabPrototype.Domain.Models
{
    public abstract class Meter
    {
        public Guid Id { get; set; }
        public string SerialCode { get; }
        public string Name { get; }
        public string Address { get; }
        public int TypeId { get; }

        protected abstract List<MeasurementAttribute> _measurementAttributes { get; }
        public List<MeasurementAttribute> MeasurementAttributes => _measurementAttributes;

        public Meter(Guid id, string serialCode, string name, string address, int typeId)
        {
            Id = id;
            SerialCode = serialCode;
            Name = name;
            Address = address;
            TypeId = typeId;
        }
    }
}
