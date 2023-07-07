using LabPrototype.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Domain.Models.Forms
{
    public class MeterTypeForm : FormBase
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
        public string Description
        {
            get => _description;
            set => ValidateAndSetRef(ref _description, value, ref DescriptionValidation.ValidationResults);
        }
        private string _description = string.Empty;
        public ValidationResultObject DescriptionValidation { get; set; } = new();

        public int ColorSchemeId { get; set; }
    }
}
