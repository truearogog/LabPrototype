using LabPrototype.Domain.Commands;
using LabPrototype.EntityFramework.Dtos;

namespace LabPrototype.EntityFramework.Commands
{
    public class DeleteMeterCommand : IDeleteMeterCommand
    {
        private readonly LabDbContextFactory _contextFactory;

        public DeleteMeterCommand(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                MeterDto meter = new MeterDto
                {
                    Id = id,
                };

                context.Meters.Remove(meter);
                await context.SaveChangesAsync();
            }
        }
    }
}
