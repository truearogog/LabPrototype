using Avalonia.Controls;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views;
using System;
using System.Threading.Tasks;
using WindowBase = LabPrototype.Views.WindowBase;

namespace LabPrototype.Services.Interfaces
{
    public interface IWindowService
    {
        Window ShowWindow<TView, TViewModel>()
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase, new();
        Window ShowWindow<TView, TViewModel>(Func<TViewModel> viewModelFactory)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase;
        Window ShowWindow<TView, TViewModel, TParameter>(TParameter parameter)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase, new()
            where TParameter : NavigationParameterBase;
        Window ShowWindow<TView, TViewModel, TParameter>(Func<TViewModel> viewModelFactory, TParameter parameter)
            where TView : WindowBase, new()
            where TViewModel : WindowViewModelBase
            where TParameter : NavigationParameterBase;

        Task ShowDialogAsync<TView, TViewModel>(WindowViewModelBase parent)
            where TView : DialogWindowBase, new()
            where TViewModel : DialogViewModelBase<DialogResultBase>, new();
        Task<TResult> ShowDialogAsync<TView, TViewModel, TResult>(WindowViewModelBase parent)
            where TView : DialogWindowBase<TResult>, new()
            where TResult : DialogResultBase
            where TViewModel : DialogViewModelBase<TResult>, new();
        Task ShowDialogAsync<TView, TViewModel, TParameter>(WindowViewModelBase parent, TParameter parameter)
            where TView : DialogWindowBase, new()
            where TViewModel : DialogViewModelBase<DialogResultBase>, new()
            where TParameter : NavigationParameterBase;
        Task<TResult> ShowDialogAsync<TView, TViewModel, TResult, TParameter>(WindowViewModelBase parent, TParameter parameter)
            where TView : DialogWindowBase<TResult>, new()
            where TResult : DialogResultBase
            where TViewModel : DialogViewModelBase<TResult>, new()
            where TParameter : NavigationParameterBase;
    }
}
