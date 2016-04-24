using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using commands;

namespace Assets.Scripts.libs.test
{
    class ParallelTestCommand: ParallelMacro
    {
        public override void Prepare()
        {
            Add<TestCommand2>();
            Add<TestCommand4>();
        }
    }
}
