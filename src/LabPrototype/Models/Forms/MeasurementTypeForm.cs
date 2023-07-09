using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Models.Forms
{
    public class MeasurementTypeForm : FormBase
    {
        [Required]
        public string Name { get => _name; set => ValidateAndSetThrow(ref _name, value); }
        private string _name = string.Empty;

        [Required]
        public string Unit { get => _unit; set => ValidateAndSetThrow(ref _unit, value); }
        private string _unit = string.Empty;

        public int ColorSchemeId { get; set; }
    }
}
