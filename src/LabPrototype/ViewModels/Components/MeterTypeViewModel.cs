using LabPrototype.Domain.Models;
using ReactiveUI;

namespace LabPrototype.ViewModels.Components
{
    public class MeterTypeViewModel : ViewModelBase
    {
        private int _type;
        public int Id
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        public MeterTypeViewModel(MeterType type)
        {
            Id = type.Id;
            Name = type.Name;
            Description = type.Description;
        }
    }
}
