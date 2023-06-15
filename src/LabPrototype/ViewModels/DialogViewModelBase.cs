using LabPrototype.Framework.Extensions;
using LabPrototype.Services.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels
{
    public abstract class DialogViewModelBase<TResult> : ViewModelBase
        where TResult : DialogResultBase
    {
        public event EventHandler<DialogResultEventArgs<TResult>> CloseRequested;

        public ICommand CloseCommand { get; }

        public DialogViewModelBase() : base()
        {
            CloseCommand = ReactiveCommand.Create(Close);
        }

        protected void Close() => Close(default);

        protected void Close(TResult result)
        {
            var args = new DialogResultEventArgs<TResult>(result);

            CloseRequested.Raise(this, args);
        }
    }

    public abstract class DialogViewModelBase : DialogViewModelBase<DialogResultBase>
    {
    }
}
