namespace LabPrototype.Domain.Models
{
    public readonly partial struct ColorScheme
    {
        public string Name { get; }
        public string Primary { get; }
        public string Secondary { get; }

        public ColorScheme(string name, string primary, string secondary)
        {
            Name = name;
            Primary = primary;
            Secondary = secondary;
        }
    }
}
