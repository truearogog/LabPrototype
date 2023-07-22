using Avalonia.Controls;
using Avalonia;
using System;
using System.Threading.Tasks;
using LabPrototype.ViewModels;
using LabPrototype.Views;
using WindowBase = LabPrototype.Views.WindowBase;
using LabPrototype.ViewModels.Models;

namespace LabPrototype.Services.WindowService
{
    public class WindowService : IWindowService
    {
        public WindowService()
        {
        }

        public Window ShowWindow<TView, TViewModel>()
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase, new()
        {
            var viewModel = CreateInstance<TViewModel>() ?? throw new InvalidOperationException();
            return ShowWindow<TView, TViewModel>(viewModel);
        }

        public Window ShowWindow<TView, TViewModel>(Func<TViewModel> viewModelFactory)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase
        {
            var viewModel = viewModelFactory();
            return ShowWindow<TView, TViewModel>(viewModel);
        }

        private Window ShowWindow<TView, TViewModel>(TViewModel viewModel)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase
        {
            var window = CreateInstance<TView>() ?? throw new InvalidOperationException();
            Bind(window, viewModel);
            window.Show();
            return window;
        }

        public Window ShowWindow<TView, TViewModel, TParameter>(TParameter parameter)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase, new()
            where TParameter : NavigationParameterBase
        {
            var viewModel = CreateInstance<TViewModel>() ?? throw new InvalidOperationException();
            return ShowWindow<TView, TViewModel, TParameter>(viewModel, parameter);
        }

        public Window ShowWindow<TView, TViewModel, TParameter>(Func<TViewModel> viewModelFactory, TParameter parameter)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase
            where TParameter : NavigationParameterBase
        {
            var viewModel = viewModelFactory();
            return ShowWindow<TView, TViewModel, TParameter>(viewModel, parameter);
        }

        private Window ShowWindow<TView, TViewModel, TParameter>(TViewModel viewModel, TParameter parameter)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase
            where TParameter : NavigationParameterBase
        {
            var window = CreateInstance<TView>() ?? throw new InvalidOperationException();
            Bind(window, viewModel);
            switch (viewModel)
            {
                case ParametrizedWindowViewModelBase<TParameter> parametrizedWindowViewModelBase:
                    parametrizedWindowViewModelBase.Activate(parameter);
                    break;
                default:
                    throw new InvalidOperationException($"{viewModel.GetType().FullName} doesn't support passing parameters!");
            }
            window.Show();
            return window;
        }

        public async Task ShowDialogAsync<TView, TViewModel>(WindowViewModelBase parent)
            where TView : DialogWindowBase, new()
            where TViewModel : DialogViewModelBase<DialogResultBase>, new()
            => await ShowDialogAsync<TView, TViewModel, DialogResultBase>(parent);

        public async Task<TResult> ShowDialogAsync<TView, TViewModel, TResult>(WindowViewModelBase parent)
            where TView : DialogWindowBase<TResult>, new()
            where TResult : DialogResultBase
            where TViewModel : DialogViewModelBase<TResult>, new()
        {
            var window = CreateInstance<TView>() ?? throw new InvalidOperationException();
            var viewModel = CreateInstance<TViewModel>() ?? throw new InvalidOperationException();
            Bind(window, viewModel);

            return await ShowDialogAsync(window, parent);
        }

        public async Task ShowDialogAsync<TView, TViewModel, TParameter>(WindowViewModelBase parent, TParameter parameter)
            where TView : DialogWindowBase, new()
            where TViewModel : DialogViewModelBase<DialogResultBase>, new()
            where TParameter : NavigationParameterBase
            => await ShowDialogAsync<TView, TViewModel, DialogResultBase, TParameter>(parent, parameter);

        public async Task<TResult> ShowDialogAsync<TView, TViewModel, TResult, TParameter>(WindowViewModelBase parent, TParameter parameter)
            where TView : DialogWindowBase<TResult>, new()
            where TResult : DialogResultBase
            where TViewModel : DialogViewModelBase<TResult>, new()
            where TParameter : NavigationParameterBase
        {
            var window = CreateInstance<TView>() ?? throw new InvalidOperationException();
            var viewModel = CreateInstance<TViewModel>() ?? throw new InvalidOperationException();
            Bind(window, viewModel);

            switch (viewModel)
            {
                case ParametrizedDialogViewModelBase<TParameter> parametrizedDialogViewModelBase:
                    parametrizedDialogViewModelBase.Activate(parameter);
                    break;
                default:
                    throw new InvalidOperationException($"{viewModel.GetType().FullName} doesn't support passing parameters!");
            }

            return await ShowDialogAsync(window, parent);
        }

        private async Task<TResult> ShowDialogAsync<TResult>(DialogWindowBase<TResult> window, WindowViewModelBase parent)
            where TResult : DialogResultBase
        {
            var parentWindow = parent.GetWindow?.Invoke() 
                ?? throw new MemberAccessException($"{nameof(WindowViewModelBase)} -> {nameof(WindowViewModelBase.GetWindow)} is null!");
            parent.Disable();
            var result = await window.ShowDialog<TResult>(parentWindow);
            parent.Enable();
            return result;
        }

        private static T? CreateInstance<T>()
            where T : class, new()
        {
            return Activator.CreateInstance<T>();
        }

        private static void Bind(IDataContextProvider view, object viewModel) => view.DataContext = viewModel;
    }
}
