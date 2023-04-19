using Avalonia.Controls;
using LabPrototype.Services.Models;

namespace LabPrototype.Views.Models
{
    public class DialogWindowBase<TResult> : WindowBase<TResult>
        where TResult : WindowResultBase
    {
        private Window ParentWindow => (Window)Owner;

        protected DialogWindowBase() : base()
        {

        }

        protected override void OnOpened()
        {
            base.OnOpened();
            LockSize();
        }
    }

    public class DialogWindowBase : DialogWindowBase<WindowResultBase>
    {

    }
}
