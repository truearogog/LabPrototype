using Avalonia.Data;
using LabPrototype.Framework.Factories;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Factories
{
    public class DataValidationExceptionFactory : IExceptionFactory<DataValidationException, ValidationResult>
    {
        public DataValidationException GetException(ValidationResult context)
        {
            return new DataValidationException(context.ErrorMessage);
        }
    }
}
