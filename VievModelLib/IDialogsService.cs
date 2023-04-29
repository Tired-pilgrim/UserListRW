using System;

namespace VievModelLib
{
    public interface IDialogsService
    {
        T? Get<T>() where T : Delegate;
    }
}
