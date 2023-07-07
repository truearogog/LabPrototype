using LabPrototype.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Domain.Models.Forms
{
    public class ColorSchemeForm : FormBase
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
        public string PrimaryColor
        {
            get => _primaryColor;
            set => ValidateAndSetRef(ref _primaryColor, value, ref PrimaryColorValidation.ValidationResults);
        }
        private string _primaryColor = string.Empty;
        public ValidationResultObject PrimaryColorValidation { get; set; } = new();

        [Required]
        public string SecondaryColor
        {
            get => _secondaryColor;
            set => ValidateAndSetRef(ref _secondaryColor, value, ref SecondaryColorValidation.ValidationResults);
        }
        private string _secondaryColor = string.Empty;
        public ValidationResultObject SecondaryColorValidation { get; set; } = new();
    }
}
