using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using LabPrototype.Providers.IntegrationCacheProvider;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabPrototype.Providers.MeasurementIntegrationCacheProvider
{
    public class MeasurementCacheProvider : IMeasurementCacheProvider
    {
        private Dictionary<int, Dictionary<int, Dictionary<MeasurementDisplayMode, Dictionary<int, (IEnumerable<double>, IEnumerable<double>)>>>> _meterCache = new();

        public void AddMeasurements(int meterId, int archiveId, MeasurementDisplayMode displayMode, int measurementTypeId, IEnumerable<double> xs, IEnumerable<double> ys)
        {
            _meterCache.TryAdd(meterId, new());
            var archiveCache = _meterCache[meterId];
            archiveCache.TryAdd(archiveId, new());
            var displayModeCache = archiveCache[archiveId];
            displayModeCache.TryAdd(displayMode, new());
            var measurementTypeCache = displayModeCache[displayMode];
            measurementTypeCache.TryAdd(measurementTypeId, (xs, ys));
        }

        public (IEnumerable<double> xs, IEnumerable<double> ys) GetMeasurements(int meterId, int archiveId, MeasurementDisplayMode displayMode, int measurementTypeId)
        {
            if (_meterCache.TryGetValue(meterId, out var archiveCache)
                && archiveCache.TryGetValue(archiveId, out var displayModeCache)
                && displayModeCache.TryGetValue(displayMode, out var measurementTypeCache)
                && measurementTypeCache.TryGetValue(measurementTypeId, out var xsys))
            {
                return xsys;
            }

            return default;
        }

        public async Task<DisplayMeasurementGroup> Integrate(int meterId, int archiveId, MeasurementDisplayMode displayMode, double dateTimeFrom, double dateTimeTo)
        {
            var result = new DisplayMeasurementGroup { MeterId = meterId };
            if (_meterCache.TryGetValue(meterId, out var archiveCache)
                && archiveCache.TryGetValue(archiveId, out var displayModeCache)
                && displayModeCache.TryGetValue(displayMode, out var measurementTypeCache))
            {
                foreach (var (measurementType, (xs, ys)) in measurementTypeCache)
                {
                    var valueTask = Task.FromResult(xs.Zip(ys).Where(x => x.First >= dateTimeFrom && x.First <= dateTimeTo).Select(x => x.Second).AsParallel().Sum());
                    var displayMeasurement = new DisplayMeasurement { MeasurementTypeId = measurementType, Value = await valueTask };
                    result.Measurements!.Add(displayMeasurement);
                }
            }

            return result;
        }
    }
}
