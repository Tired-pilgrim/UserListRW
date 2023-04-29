using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VievModelLib
{
    public interface IDialogsService
    {
        T? Get<T>() where T : Delegate;
    }
}
