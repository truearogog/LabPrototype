using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using LabPrototype.EntityFramework.Dtos;
using Microsoft.EntityFrameworkCore;
using System;

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
            return GetMeasurements();

            /*
            using (var context = _contextFactory.Create())
            {
                IEnumerable<MeasurementDto> measurementDtos = await context.Measurements.Where(x => x.MeterId.Equals(meterId)).ToListAsync();
                return measurementDtos.Select(x => new Measurement(x.Q1, x.Q2, x.P, x.t1)).ToList();
            }
            */
        }

        private IEnumerable<Measurement> GetMeasurements()
        {
            var date = DateTime.UtcNow.Date;
            var _measurement = new Measurement(0, 0, 0, 0);
            for (int i = 0; i < 1000; ++i)
            {
                Measurement measurement = new Measurement(
                    _measurement.Q1 + Random.Shared.Next(-10, 11),
                    _measurement.Q2 + Random.Shared.Next(-10, 11),
                    _measurement.P + Random.Shared.Next(-10, 11),
                    _measurement.T + Random.Shared.Next(-10, 11)
                );
                measurement.DateTime = date;
                _measurement = measurement;
                yield return measurement;
                date = date.AddMinutes(30);
            }
        }
    }
}
