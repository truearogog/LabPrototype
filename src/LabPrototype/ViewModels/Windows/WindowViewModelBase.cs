using LabPrototype.Extensions;
using LabPrototype.Services.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Windows
{
    public class WindowViewModelBase<TResult> : ViewModelBase
        where TResult : WindowResultBase
    {
        public event EventHandler<WindowResultEventArgs<TResult>> CloseRequested;

        public ICommand CloseCommand { get; }

        protected WindowViewModelBase()
        {
            CloseCommand = ReactiveCommand.Create(Close);
        }

        protected void Close() => Close(default);

        protected void Close(TResult result)
        {
            var args = new WindowResultEventArgs<TResult>(result);

            CloseRequested.Raise(this, args);
        }
    }

    public class WindowViewModelBase : WindowViewModelBase<WindowResultBase>
    {

    }
}
