using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Models;

namespace LabPrototype.ViewModels
{
    public abstract class ParametrizedDialogViewModelBase<TResult, TParameter> : DialogViewModelBase<TResult>
        where TResult : DialogResultBase
        where TParameter : NavigationParameterBase
    {
        public abstract void Activate(TParameter parameter);
    }

    public abstract class ParametrizedDialogViewModelBase<TParameter> : ParametrizedDialogViewModelBase<DialogResultBase, TParameter>
        where TParameter : NavigationParameterBase
    {

    }
}
