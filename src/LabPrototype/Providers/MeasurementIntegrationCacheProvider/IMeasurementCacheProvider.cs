using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LabPrototype.Providers.IntegrationCacheProvider
{
    public interface IMeasurementCacheProvider
    {
        void AddMeasurements(int meterId, int archiveId, MeasurementDisplayMode displayMode, int measurementTypeId, IEnumerable<double> xs, IEnumerable<double> ys);
        (IEnumerable<double> xs, IEnumerable<double> ys) GetMeasurements(int meterId, int archiveId, MeasurementDisplayMode displayMode, int measurementTypeId);
        Task<DisplayMeasurementGroup> Integrate(int meterId, int archiveId, MeasurementDisplayMode displayMode, double dateTimeFrom, double dateTimeTo);
    }
}
