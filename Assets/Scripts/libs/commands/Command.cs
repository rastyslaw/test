﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace commands
{
    class Command : ICommand
    {
        public virtual void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
