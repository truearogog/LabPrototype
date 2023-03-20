using ReactiveUI;
using System;

namespace LabPrototype.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        public virtual void Dispose()
        {

        }
    }
}