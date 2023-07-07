using LabPrototype.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Domain.Models.Forms
{
    public class MeasurementTypeForm : FormBase
    {
        [Required]
        public string Name
        {
            get => _name;
            set => ValidateAndSetRef(ref _name, value, ref NameValidation.ValidationResults);
        }
        private string _name = string.Empty;
        public ValidationResultObject NameValidation { get; set; } = new();

        [Required]
        public string Unit
        {
            get => _unit;
            set => ValidateAndSetRef(ref _unit, value, ref UnitValidation.ValidationResults);
        }
        private string _unit = string.Empty;
        public ValidationResultObject UnitValidation { get; set; } = new();

        public int ColorSchemeId { get; set; }
    }
}
