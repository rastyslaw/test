using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    class ParallelMacro: AbstractMacro
    {
        private int _executionCount;
        private int _queueLen;
        private bool _running;
        private bool _success = true;

        public override void Execute()
        {
            if (HasCommand())
            {
                _running = true;
                _queueLen = _queue.Count; 
                while (_queue.Count > 0)
                {
                    if (!_success)
                    {
                        break; 
                    }
                    ExecuteCommand();
               }
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
            _executionCount++;
            _success &= success;
            if (_running && _executionCount == _queueLen)
            {
                DispatchComplete(success); 
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
