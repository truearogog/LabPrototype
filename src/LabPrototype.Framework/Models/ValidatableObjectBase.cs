using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LabPrototype.Framework.Models
{
    public class ValidatableObjectBase
    {
        protected ValidatableObjectBase()
        {
        }

        protected TRef ValidateAndSetOut<TRef>(ref TRef backingField, TRef newValue, out ICollection<ValidationResult> results, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var context = new ValidationContext(this) { MemberName = propertyName };
            results = new List<ValidationResult>();

            if (!Validator.TryValidateProperty(newValue, context, results))
            {
                return backingField;
            }

            backingField = newValue;
            return newValue;
        }

        protected TRef ValidateAndSetRef<TRef>(ref TRef backingField, TRef newValue, ref ICollection<ValidationResult>? results, [CallerMemberName] string? propertyName = null)
        {
            var _newValue = ValidateAndSetOut(ref backingField, newValue, out var _results, propertyName);
            results = _results;
            return _newValue;
        }

        public bool Validate(out ICollection<ValidationResult> results, bool recursive = false)
        {
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, new ValidationContext(this), results, true);
        }
    }
}
