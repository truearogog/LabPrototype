using LabPrototype.Services.Interfaces;
using LabPrototype.Services.Models;
using System.Threading.Tasks;

namespace LabPrototype.Services.Implementations
{
    public class WindowService : IWindowService
    {
        public WindowService()
        {

        }

        public Task ShowWindowAsync(string viewModelName)
        {
            throw new System.NotImplementedException();
        }

        public Task ShowWindowAsync<TParameter>(string viewModelName, TParameter parameter) where TParameter : NavigationParameterBase
        {
            throw new System.NotImplementedException();
        }

        public Task<TResult> ShowWindowAsync<TResult>(string viewModelName) where TResult : WindowResultBase
        {
            throw new System.NotImplementedException();
        }

        public Task<TResult> ShowWindowAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : WindowResultBase
            where TParameter : NavigationParameterBase
        {
            throw new System.NotImplementedException();
        }
    }
}
