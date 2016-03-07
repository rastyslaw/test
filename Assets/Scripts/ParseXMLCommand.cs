using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ParseXMLCommand : MonoBehaviour {
    
	void Start () 
    {
        EnemyContainer cont = EnemyContainer.Load("enemies");

        DataModel.SetValue(Names.ENEMIES, cont);

        DataModel.SetValue(Names.HP, 100.0f); 

        SceneManager.LoadScene(1);
    }
}
