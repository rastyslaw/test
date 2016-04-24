using commands;
using UnityEngine;

namespace Assets.Scripts.libs.test
{
    class TestCommand3: Command
    {
        public override void Execute()
        {
            Debug.Log(this.ToString() + " execute");
        }
    }
}
