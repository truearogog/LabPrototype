using Avalonia.Controls;
using LabPrototype.Services.WindowService;
using LabPrototype.ViewModels;
using System;

namespace LabPrototype.Views
{
    public abstract class DialogWindowBase<TResult> : Window
        where TResult : DialogResultBase
    {
        private Window ParentWindow => (Window)Owner;

        protected DialogViewModelBase<TResult>? ViewModel => DataContext as DialogViewModelBase<TResult>;

        protected DialogWindowBase()
        {
            SubscribeToViewEvents();
        }

        protected virtual void OnOpened()
        {

        }

        private void OnOpened(object? sender, EventArgs e)
        {
            OnOpened();
        }

        protected virtual void OnClosed()
        {

        }

        private void OnClosed(object? sender, EventArgs e)
        {
            Cleanup();
            OnClosed();
        }

        protected void LockSize()
        {
            CanResize = false;
        }

        private void SubscribeToViewModelEvents()
        {
            if (ViewModel != null)
            {
                ViewModel.CloseRequested += ViewModelOnCloseRequested;
            }
        }

        private void UnsubscribeFromViewModelEvents()
        {
            if (ViewModel != null)
            {
                ViewModel.CloseRequested -= ViewModelOnCloseRequested;
            }
        }

        private void SubscribeToViewEvents()
        {
            DataContextChanged += OnDataContextChanged;
            Opened += OnOpened;
            Closed += OnClosed;
        }

        private void UnsubscribeFromViewEvents()
        {
            DataContextChanged -= OnDataContextChanged;
            Opened -= OnOpened;
            Closed -= OnClosed;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            SubscribeToViewModelEvents();
        }

        private void ViewModelOnCloseRequested(object? sender, DialogResultEventArgs<TResult> args)
        {
            Cleanup();

            Close(args.Result);
        }

        private void Cleanup()
        {
            UnsubscribeFromViewModelEvents();
            UnsubscribeFromViewEvents();
            ViewModel?.Dispose();
        }
    }

    public class DialogWindowBase : DialogWindowBase<DialogResultBase>
    {

    }
}
