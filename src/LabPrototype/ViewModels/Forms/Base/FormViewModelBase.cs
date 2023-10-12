using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.ViewModels.Forms.Base
{
    public class FormViewModelBase : ViewModelBase
    {
        public int Id { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext? validationContext = null)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), results, true);
            return results;
        }
    }
}
