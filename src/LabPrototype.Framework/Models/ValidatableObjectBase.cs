using LabPrototype.Framework.Factories;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LabPrototype.Framework.Models
{
    public class ValidatableObjectBase<TException, TExceptionFactory>
        where TException : Exception
        where TExceptionFactory : IExceptionFactory<TException, ValidationResult>, new()
    {
        private readonly TExceptionFactory _exceptionFactory;

        protected ValidatableObjectBase()
        {
            _exceptionFactory = new TExceptionFactory();
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

        protected TRef ValidateAndSetThrow<TRef>(ref TRef backingField, TRef newValue, [CallerMemberName] string? propertyName = null)
        {
            ValidateAndSetOut(ref backingField, newValue, out var _results, propertyName);
            if (_results.Any())
            {
                throw _exceptionFactory.GetException(_results.First());
            }
            return newValue;
        }

        public bool Validate(out ICollection<ValidationResult> results, bool recursive = false)
        {
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(this, new ValidationContext(this), results, true);
        }
    }
}
