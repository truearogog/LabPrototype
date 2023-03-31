using LabPrototype.Domain.Models;
using LabPrototype.Domain.Queries;

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
            var _measurement = new Measurement(DateTime.UtcNow.Date, 0, 0, 0, 0);
            for (int i = 0; i < 500_000; ++i)
            {
                Measurement measurement = new Measurement(
                    DateTime.UtcNow.Date.AddMinutes(i * 30),
                    _measurement.Q1 + Random.Shared.Next(-2, 3),
                    _measurement.Q2 + Random.Shared.Next(-2, 3),
                    _measurement.P + Random.Shared.Next(-2, 3),
                    _measurement.T + Random.Shared.Next(-2, 3)
                );
                _measurement = measurement;
                yield return measurement;
            }
        }
    }
}
