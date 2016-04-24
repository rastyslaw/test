using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    class AsyncCommand : IAsyncCommand 
    {
        private List<CommandCallback> _listeners;

        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
        
        public void RegisterCompleteCallback(CommandCallback callback)
        {
            if (_listeners == null)
            {
                _listeners = new List<CommandCallback>();
            }
            _listeners.Add(callback);
        }

        protected virtual void DispatchComplete(bool success)
        {
            foreach (CommandCallback listener in _listeners)
            {
                listener(success);
            }
        }
    }
}
