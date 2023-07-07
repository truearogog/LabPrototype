using LabPrototype.Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.Domain.Models.Forms
{
    public class MeterForm : FormBase
    {
        [Required]
        public string SerialCode
        {
            get => _serialCode;
            set => ValidateAndSetRef(ref _serialCode, value, ref SerialCodeValidation.ValidationResults);
        }
        private string _serialCode = string.Empty;
        public ValidationResultObject SerialCodeValidation { get; set; } = new();

        [Required]
        public string Name
        {
            get => _name;
            set => ValidateAndSetRef(ref _name, value, ref NameValidation.ValidationResults);
        }
        private string _name = string.Empty;
        public ValidationResultObject NameValidation { get; set; } = new();

        [Required]
        public string Address
        {
            get => _address;
            set => ValidateAndSetRef(ref _address, value, ref AddressValidation.ValidationResults);
        }
        private string _address = string.Empty;
        public ValidationResultObject AddressValidation { get; set; } = new();

        public int MeterTypeId { get; set; }
    }
}
