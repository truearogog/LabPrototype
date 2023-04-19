using System.Threading.Tasks;
using LabPrototype.Services.Models;

namespace LabPrototype.Services.Interfaces
{
    public interface IWindowService
    {
        Task ShowDialogAsync(string viewModelName);

        Task ShowDialogAsync<TParameter>(string viewModelName, TParameter parameter)
            where TParameter : NavigationParameterBase;

        Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
            where TResult : DialogResultBase;

        Task<TResult> ShowDialogAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : DialogResultBase
            where TParameter : NavigationParameterBase;

        Task ShowWindowAsync(string viewModelName);

        Task ShowWindowAsync<TParameter>(string viewModelName, TParameter parameter)
            where TParameter : NavigationParameterBase;

        Task<TResult> ShowWindowAsync<TResult>(string viewModelName)
            where TResult : WindowResultBase;

        Task<TResult> ShowWindowAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : WindowResultBase
            where TParameter : NavigationParameterBase;
    }
}
