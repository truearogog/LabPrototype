using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPrototype.Services
{
    public class DialogResultEventArgs<TResult> : EventArgs
    {
        public TResult Result { get; }

        public DialogResultEventArgs(TResult result)
        {
            Result = result;
        }
    }
}
