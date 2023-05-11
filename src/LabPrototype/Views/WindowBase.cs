using Avalonia.Controls;
using LabPrototype.ViewModels;
using System;

namespace LabPrototype.Views
{
    public class WindowBase : Window
    {
        protected WindowViewModelBase? ViewModel => DataContext as WindowViewModelBase;
        protected Control? Overlay;

        protected WindowBase()
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
            MaxWidth = MinWidth = Width;
            MaxHeight = MinHeight = Height;
        }

        private void SubscribeToViewModelEvents()
        {
            if (ViewModel != null)
            {
                ViewModel.CloseRequested += ViewModelOnCloseRequested;
                ViewModel.EnableRequested += ViewModelOnEnableRequested;
                ViewModel.DisableRequested += ViewModelOnDisableRequested;
            }
        }

        private void UnsubscribeFromViewModelEvents()
        {
            if (ViewModel != null)
            {
                ViewModel.CloseRequested -= ViewModelOnCloseRequested;
                ViewModel.EnableRequested -= ViewModelOnEnableRequested;
                ViewModel.DisableRequested -= ViewModelOnDisableRequested;
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
            if (ViewModel != null)
            {
                ViewModel.GetWindow = () => this;
            }
            SubscribeToViewModelEvents();
        }

        private void ViewModelOnCloseRequested(object? sender, EventArgs e)
        {
            UnsubscribeFromViewModelEvents();
            UnsubscribeFromViewEvents();
            ViewModel?.Dispose();
            
            Close();
        }

        private void Cleanup()
        {
            UnsubscribeFromViewModelEvents();
            UnsubscribeFromViewEvents();
            ViewModel?.Dispose();
        }

        private void ViewModelOnEnableRequested(object? sender, EventArgs e)
        {
            if (Overlay != null)
            {
                Overlay.ZIndex = -1;
                Overlay.IsVisible = false;
            }
        }

        private void ViewModelOnDisableRequested(object? sender, EventArgs e)
        {
            if (Overlay != null)
            {
                Overlay.ZIndex = 1000;
                Overlay.IsVisible = true;
            }
        }
    }
}
