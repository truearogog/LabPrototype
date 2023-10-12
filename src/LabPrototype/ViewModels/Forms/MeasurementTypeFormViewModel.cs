using LabPrototype.ViewModels.Forms.Base;
using ReactiveUI;
using System.ComponentModel.DataAnnotations;

namespace LabPrototype.ViewModels.Forms
{
    public class MeasurementTypeFormViewModel : FormViewModelBase
    {
        [Required]
        public string Unit { get => _unit; set => this.RaiseAndSetIfChanged(ref _unit, value); }
        private string _unit = string.Empty;

        [Required]
        public string IntegrationUnit { get => _integrationUnit; set => this.RaiseAndSetIfChanged(ref _integrationUnit, value); }
        private string _integrationUnit = string.Empty;

        [Required]
        public string PrimaryColor { get => _primaryColor; set => this.RaiseAndSetIfChanged(ref _primaryColor, value); }
        private string _primaryColor = string.Empty;

        [Required]
        public string SecondaryColor { get => _secondaryColor; set => this.RaiseAndSetIfChanged(ref _secondaryColor, value); }
        private string _secondaryColor = string.Empty;
    }
}
