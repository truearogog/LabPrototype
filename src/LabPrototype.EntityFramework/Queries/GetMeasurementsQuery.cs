using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.EntityFramework.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LabPrototype.EntityFramework.Queries
{
    public class GetMeasurementsQuery : IGetMeasurementsQuery
    {
        private readonly LabDbContextFactory _contextFactory;

        public GetMeasurementsQuery(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Measurement>> Execute(Guid meterId)
        {
            using (var context = _contextFactory.Create())
            {
                IEnumerable<MeasurementDto> measurementDtos = await context.Measurements.Where(x => x.MeterId.Equals(meterId)).ToListAsync();
                return measurementDtos.Select(x => new Measurement(x.Created, x.Q1, x.Q2, x.P, x.t1)).ToList();
            }
        }
    }
}
