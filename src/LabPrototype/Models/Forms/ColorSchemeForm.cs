using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Models.Forms
{
    public class ColorSchemeForm : FormBase
    {
        [Required]
        public string Name { get => _name; set => ValidateAndSetThrow(ref _name, value); }
        private string _name = string.Empty;

        [Required]
        public string PrimaryColor { get => _primaryColor; set => ValidateAndSetThrow(ref _primaryColor, value); }
        private string _primaryColor = string.Empty;

        [Required]
        public string SecondaryColor { get => _secondaryColor; set => ValidateAndSetThrow(ref _secondaryColor, value); }
        private string _secondaryColor = string.Empty;
    }
}
