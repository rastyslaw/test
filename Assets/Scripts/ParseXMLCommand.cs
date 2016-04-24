using UnityEngine;
using System.Collections;
using commands.test;
using UnityEngine.SceneManagement;

public class ParseXMLCommand : MonoBehaviour {
    
	void Start () 
    {
        EnemyContainer cont = EnemyContainer.Load(Names.ENEMIES);
        DataModel.SetValue(Names.ENEMIES, cont.enemies);

        Test test = new Test();
    }
}
