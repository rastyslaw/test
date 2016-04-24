using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    class SequenceMacro: AbstractMacro
    {
        private bool _running;
        private bool _success = true;

        public override void Execute()
        {
            _running = true;
            ExecuteNext(); 
        }

        void ExecuteNext()
        {
            if (HasCommand())
            {
                ExecuteCommand();
            }
            else
            {
                DispatchComplete(false);
            }
        }

        bool HasCommand()
        {
            return _queue.Count > 0; 
        }

        protected override void CommandCompleteHandler(bool success)
        {
            _success &= success;
            if (_success)
            {
                ExecuteNext(); 
            }
            else
            {
                DispatchComplete(false);
            }
        }

        protected override void DispatchComplete(bool success)
        {
            base.DispatchComplete(success);
            _running = false;
            _success = true;
        }
    }
}