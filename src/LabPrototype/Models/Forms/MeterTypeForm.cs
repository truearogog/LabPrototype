using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Models.Forms
{
    public class MeterTypeForm : FormBase
    {
        [Required]
        public string Name { get => _name; set => ValidateAndSetThrow(ref _name, value); }
        private string _name = string.Empty;

        [Required]
        public string Description { get => _description; set => ValidateAndSetThrow(ref _description, value); }
        private string _description = string.Empty;

        public int ColorSchemeId { get; set; }
    }
}
