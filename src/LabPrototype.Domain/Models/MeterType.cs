namespace LabPrototype.Domain.Models
{
    public readonly partial struct MeterType
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Type Type { get; }

        public MeterType(int id, string name, string description, Type type)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
        }
    }
}
