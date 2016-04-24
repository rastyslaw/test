using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using commands;
using UnityEngine;

namespace Assets.Scripts.libs.test
{
    class TestCommand4: AsyncCommand
    {
        public override void Execute()
        {
            Debug.Log(this.ToString() + " execute");

            TimerCallback tm = new TimerCallback(finish);
            new System.Threading.Timer(tm, null, 3000, -1);
        }

        void finish(object obj)
        {
            DispatchComplete(true);
        }

        protected override void DispatchComplete(bool success)
        {
            Debug.Log(this.ToString() + " finish");
            base.DispatchComplete(success);
        }
    }
}
