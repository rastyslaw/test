using UnityEngine;

namespace commands.test
{
    class TestCommand1: Command
    {
        public override void Execute()
        {
            Debug.Log(this.ToString() + " execute");
        }
    }
}
