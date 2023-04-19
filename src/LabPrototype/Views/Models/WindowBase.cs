using Avalonia;
using Avalonia.Controls;
using LabPrototype.Services.Models;
using LabPrototype.ViewModels.Dialogs;
using LabPrototype.ViewModels.Windows;
using System;

namespace LabPrototype.Views.Models
{
    public class WindowBase<TResult> : Window
        where TResult : WindowResultBase
    {
        protected WindowViewModelBase<TResult> ViewModel => (WindowViewModelBase<TResult>)DataContext;

        protected WindowBase()
        {
            SubscribeToViewEvents();
        }

        protected virtual void OnOpened()
        {

        }

        private void OnOpened(object sender, EventArgs e)
        {
            OnOpened();
        }

        protected void LockSize()
        {
            MaxWidth = MinWidth = Width;
            MaxHeight = MinHeight = Height;
        }

        private void SubscribeToViewModelEvents() => ViewModel.CloseRequested += ViewModelOnCloseRequested;

        private void UnsubscribeFromViewModelEvents() => ViewModel.CloseRequested -= ViewModelOnCloseRequested;

        private void SubscribeToViewEvents()
        {
            DataContextChanged += OnDataContextChanged;
            Opened += OnOpened;
        }

        private void UnsubscribeFromViewEvents()
        {
            DataContextChanged -= OnDataContextChanged;
            Opened -= OnOpened;
        }

        private void OnDataContextChanged(object sender, EventArgs e) => SubscribeToViewModelEvents();

        private void ViewModelOnCloseRequested(object sender, WindowResultEventArgs<TResult> args)
        {
            UnsubscribeFromViewModelEvents();
            UnsubscribeFromViewEvents();

            Close(args.Result);
        }
    }

    public class WindowBase : WindowBase<WindowResultBase>
    {

    }
}
