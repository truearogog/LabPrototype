using LabPrototype.Framework.Models;

namespace LabPrototype.Domain.Models.Forms
{
    public class MeterTypeMeasurementTypeForm : FormBase
    {
        public int MeterTypeId { get; set; }
        public int MeasurementTypeId { get; set; }
        public int SortOrder { get; set; }
    }
}
