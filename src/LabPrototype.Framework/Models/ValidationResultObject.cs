using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Framework.Models
{
    public class ValidationResultObject
    {
        public ICollection<ValidationResult>? ValidationResults = default;
        public string ErrorMessage => ValidationResults?.FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}
