namespace LabPrototype.ViewModels.Models
{
    public class ModelNavigationParameter<T> : NavigationParameterBase
    {
        public T? Model { get; }

        public ModelNavigationParameter(T? model)
        {
            Model = model;
        }
    }
}
