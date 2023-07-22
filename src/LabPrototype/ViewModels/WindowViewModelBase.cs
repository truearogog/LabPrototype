using ReactiveUI;
using System;
using System.Windows.Input;
using WindowBase = LabPrototype.Views.WindowBase;

namespace LabPrototype.ViewModels
{
    public abstract class WindowViewModelBase : ViewModelBase
    {
        public event EventHandler? CloseRequested;
        public event EventHandler? EnableRequested;
        public event EventHandler? DisableRequested;

        public Func<WindowBase>? GetWindow { get; set; }

        public ICommand CloseCommand { get; }

        protected WindowViewModelBase()
        {
            CloseCommand = ReactiveCommand.Create(Close);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Enable()
        {
            EnableRequested?.Invoke(this, EventArgs.Empty);
        }

        public void Disable()
        {
            DisableRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
