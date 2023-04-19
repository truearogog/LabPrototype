using System;

namespace LabPrototype.Services.Models
{
    public class WindowResultEventArgs<TResult> : EventArgs
    {
        public TResult Result { get; }

        public WindowResultEventArgs(TResult result)
        {
            Result = result;
        }
    }
}
