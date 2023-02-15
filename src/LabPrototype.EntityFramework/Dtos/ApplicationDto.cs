namespace LabPrototype.EntityFramework.Dtos
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid? MeterGuid { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
