using Avalonia.Data;
using LabPrototype.Factories;
using LabPrototype.Framework.Models;

namespace LabPrototype.Models.Forms
{
    public class FormBase : ValidatableObjectBase<DataValidationException, DataValidationExceptionFactory>
    {
        public int Id { get; set; }
    }
}
