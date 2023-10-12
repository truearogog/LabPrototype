namespace LabPrototype.Domain.Models.Presentation
{
    public abstract class PresentationModelBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
