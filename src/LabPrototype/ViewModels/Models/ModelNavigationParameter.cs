namespace LabPrototype.ViewModels.Models
{
    public class ModelNavigationParameter<T> : NavigationParameterBase
    {
        public T? Model { get; init; } = default;
    }
}
