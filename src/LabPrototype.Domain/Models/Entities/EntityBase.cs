namespace LabPrototype.Domain.Models.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
