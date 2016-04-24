using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    abstract class AbstractMacro : AsyncCommand, IMacro
    {
        protected readonly Queue<Type> _queue = new Queue<Type>();

        public AbstractMacro()
        {
            Prepare(); 
        }

        public virtual void Prepare()
        {
            throw new NotImplementedException();
        }

        public void Add(Type clasType)
        {
            _queue.Enqueue(clasType); 
        }

        public void Add<T>() 
        {
            _queue.Enqueue(typeof(T));
        }
       
        public void Remove()
        {
            _queue.Dequeue();
        }

        protected void ExecuteCommand()
        {
            Type clasType = _queue.Dequeue();
            ICommand command = Activator.CreateInstance(clasType) as ICommand;
            if (command != null)
            {
                bool isAsync = command is IAsyncCommand;
                if (isAsync)
                {
                    IAsyncCommand async = (IAsyncCommand) command;
                    async.RegisterCompleteCallback(CommandCompleteHandler);
                    command.Execute();
                }
                else
                { 
                    command.Execute(); 
                    CommandCompleteHandler(true);
                }
               
            }
            else
            {
                CommandCompleteHandler(true);
            }
        }

        protected virtual void CommandCompleteHandler(bool success)
        {
            throw new NotImplementedException();  
        }
    }
}
