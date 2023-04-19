using Avalonia;
using LabPrototype.Services.Interfaces;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.ViewModels;
using LabPrototype.Views;
using Splat;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LabPrototype.DependencyInjection;
using LabPrototype.Services.Models;
using LabPrototype.Views.Models;

namespace LabPrototype.Services.Implementations
{
    public class WindowService : IWindowService
    {
        public WindowService()
        {

        }

        public async Task ShowWindowAsync(string viewModelName)
        {
            throw new System.NotImplementedException();
        }

        public async Task ShowWindowAsync<TParameter>(string viewModelName, TParameter parameter) 
            where TParameter : NavigationParameterBase
        {
            throw new System.NotImplementedException();
        }

        public async Task<TResult> ShowWindowAsync<TResult>(string viewModelName) 
            where TResult : WindowResultBase
        {
            throw new System.NotImplementedException();
        }

        public async Task<TResult> ShowWindowAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : WindowResultBase
            where TParameter : NavigationParameterBase
        {
            throw new System.NotImplementedException();
        }

        public async Task ShowDialogAsync(string viewModelName)
        {
            await ShowDialogAsync<DialogResultBase>(viewModelName);
        }

        public async Task ShowDialogAsync<TParameter>(string viewModelName, TParameter parameter)
            where TParameter : NavigationParameterBase
        {
            await ShowDialogAsync<DialogResultBase, TParameter>(viewModelName, parameter);
        }

        public async Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
            where TResult : DialogResultBase
        {
            var window = CreateView<TResult>(viewModelName);
            var viewModel = CreateViewModel<TResult>(viewModelName);
            Bind(window, viewModel);

            return await ShowDialogAsync(window);
        }

        public async Task<TResult> ShowDialogAsync<TResult, TParameter>(string viewModelName, TParameter parameter)
            where TResult : DialogResultBase
            where TParameter : NavigationParameterBase
        {
            var window = CreateView<TResult>(viewModelName);
            var viewModel = CreateViewModel<TResult>(viewModelName);
            Bind(window, viewModel);

            switch (viewModel)
            {
                case ParameterizedDialogViewModelBase<TResult, TParameter> parameterizedDialogViewModelBase:
                    parameterizedDialogViewModelBase.Activate(parameter);
                    break;
                case ParameterizedDialogViewModelBaseAsync<TResult, TParameter> parameterizedDialogViewModelBaseAsync:
                    await parameterizedDialogViewModelBaseAsync.ActivateAsync(parameter);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"{viewModel.GetType().FullName} doesn't support passing parameters!");
            }

            return await ShowDialogAsync(window);
        }

        private static void Bind(IDataContextProvider window, object viewModel) => window.DataContext = viewModel;

        private static DialogWindowBase<TResult> CreateView<TResult>(string viewModelName)
            where TResult : DialogResultBase
        {
            var viewType = GetViewType(viewModelName);
            if (viewType is null)
            {
                throw new InvalidOperationException($"View for {viewModelName} was not found!");
            }

            return (DialogWindowBase<TResult>)GetView(viewType);
        }

        private static DialogViewModelBase<TResult> CreateViewModel<TResult>(string viewModelName)
            where TResult : DialogResultBase
        {
            var viewModelType = GetViewModelType(viewModelName);
            if (viewModelType is null)
            {
                throw new InvalidOperationException($"View model {viewModelName} was not found!");
            }

            return (DialogViewModelBase<TResult>)GetViewModel(viewModelType);
        }

        private static Type? GetViewModelType(string viewModelName)
        {
            var viewModelsAssembly = Assembly.GetAssembly(typeof(ViewModelBase));
            if (viewModelsAssembly is null)
            {
                throw new InvalidOperationException("Broken installation!");
            }

            var viewModelTypes = viewModelsAssembly.GetTypes();

            return viewModelTypes.SingleOrDefault(t => t.Name == viewModelName);
        }

        private static object GetView(Type type) => Activator.CreateInstance(type);

        private static object GetViewModel(Type type) => Locator.Current.GetRequiredService(type);

        private static Type GetViewType(string viewModelName)
        {
            var viewsAssembly = Assembly.GetExecutingAssembly();
            var viewTypes = viewsAssembly.GetTypes();
            var viewName = viewModelName.Replace("ViewModel", string.Empty);

            return viewTypes.SingleOrDefault(t => t.Name == viewName);
        }

        private async Task<TResult> ShowDialogAsync<TResult>(DialogWindowBase<TResult> window)
            where TResult : DialogResultBase
        {
            var mainWindow = (MainWindow)_mainWindowProvider.GetMainWindow();

            mainWindow.ShowOverlay();
            var result = await window.ShowDialog<TResult>(mainWindow);
            mainWindow.HideOverlay();

            if (window is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return result;
        }
    }

}
