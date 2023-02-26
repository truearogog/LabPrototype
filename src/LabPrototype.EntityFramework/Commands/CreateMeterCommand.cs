using LabPrototype.Domain.Commands;
using LabPrototype.Domain.Models;
using LabPrototype.EntityFramework.Dtos;

namespace LabPrototype.EntityFramework.Commands
{
    public class CreateMeterCommand : ICreateMeterCommand
    {
        private readonly LabDbContextFactory _contextFactory;

        public CreateMeterCommand(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Meter meter)
        {
            using (var context = _contextFactory.Create())
            {
                MeterDto meterDto = new MeterDto()
                {
                    Id = meter.Id,
                    SerialCode = meter.SerialCode,
                    Name = meter.Name,
                    Address = meter.Address,
                    TypeId = meter.TypeId,
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                };

                context.Meters.Add(meterDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
