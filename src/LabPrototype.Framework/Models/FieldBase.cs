using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Framework.Models
{
    public class FieldBase<T> : ValidatableObjectBase
    {
        private T? _value = default;
        public T? Value
        {
            get => _value;
            set
            {
                ValidateAndSet(ref _value, value, out var results);
                ValidationResults = results;
            }
        }

        public IEnumerable<ValidationResult> ValidationResults { get; private set; } = Enumerable.Empty<ValidationResult>();
        public string ValidationError => ValidationResults.FirstOrDefault()?.ErrorMessage ?? string.Empty;
    }
}
