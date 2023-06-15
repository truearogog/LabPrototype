using LabPrototype.Framework.Extensions;
using ReactiveUI;
using Splat;
using System;

namespace LabPrototype.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable
    {
        public virtual void Dispose()
        {

        }

        protected static T GetRequiredService<T>() => Locator.Current.GetRequiredService<T>();
    }
}