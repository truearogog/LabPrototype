using LabPrototype.ViewModels.Models;

namespace LabPrototype.ViewModels
{
    public abstract class ParametrizedWindowViewModelBase<TParameter> : WindowViewModelBase
        where TParameter : NavigationParameterBase
    {
        public abstract void Activate(TParameter parameter);
    }
}
