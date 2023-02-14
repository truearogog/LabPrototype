using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.EntityFramework.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.EntityFramework.Queries
{
    public class GetAllMetersQuery : IGetAllMetersQuery
    {
        private readonly LabDbContextFactory _contextFactory;

        public GetAllMetersQuery(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Meter>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                IEnumerable<MeterDto> meterDtos = await context.Meters.ToListAsync();
                return meterDtos.Select(x => new Meter(x.Id, x.SerialCode, x.Name, x.Address));
            }
        }
    }
}
