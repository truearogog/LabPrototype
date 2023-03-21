using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.EntityFramework.Queries
{
    public class TestGetMeasurementsQuery : IGetMeasurementsQuery
    {
        private readonly LabDbContextFactory _contextFactory;

        public TestGetMeasurementsQuery(LabDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Measurement>> Execute(Guid meterId)
        {
            return GetMeasurements();
        }

        private IEnumerable<Measurement> GetMeasurements()
        {
            var date = DateTime.UtcNow.Date;
            var _measurement = new Measurement(0, 0, 0, 0);
            for (int i = 0; i < 500_000; ++i)
            {
                Measurement measurement = new Measurement(
                    _measurement.Q1 + Random.Shared.Next(-2, 3),
                    _measurement.Q2 + Random.Shared.Next(-2, 3),
                    _measurement.P + Random.Shared.Next(-2, 3),
                    _measurement.T + Random.Shared.Next(-2, 3)
                );
                measurement.DateTime = date;
                _measurement = measurement;
                yield return measurement;
                date = date.AddMinutes(30);
            }
        }
    }
}
