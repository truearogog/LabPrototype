using LabPrototype.Domain.Commands;
using LabPrototype.Domain.Models;
using LabPrototype.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.EntityFramework.Commands
{
    public class UpdateMeterCommand : IUpdateMeterCommand
    {
        private readonly LabDbContextFactory _contextFactory;

        public UpdateMeterCommand(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Meter meter)
        {
            using (var context = _contextFactory.Create())
            {
                MeterDto meterDto = new MeterDto
                {
                    Id = meter.Id,
                    SerialCode = meter.SerialCode,
                    Name = meter.Name,
                    Address = meter.Address,
                    TypeId = meter.TypeId,
                    Updated = DateTime.Now
                };

                context.Meters.Update(meterDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
