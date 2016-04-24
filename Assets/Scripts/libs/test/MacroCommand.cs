using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.libs.test;
using UnityEngine;

namespace commands.test
{
    class MacroCommand: SequenceMacro
    {
        public override void Prepare()
        {
           Add<TestCommand1>();
           Add<ParallelTestCommand>();
           Add<TestCommand3>();

            RegisterCompleteCallback(OnComplete);
        }

        void OnComplete(bool success)
        {
            Debug.Log("All commands have been executed");
        }
    }
}
