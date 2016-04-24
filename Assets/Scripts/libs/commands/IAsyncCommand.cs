using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    interface IAsyncCommand: ICommand
    {
        void RegisterCompleteCallback(CommandCallback callback);
    }
}
