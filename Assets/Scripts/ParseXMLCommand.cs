using UnityEngine;
using System.Collections;
using commands.test;
using UnityEngine.SceneManagement;

public class ParseXMLCommand : MonoBehaviour {
    
	void Awake () 
    {
        new MacroCommand().Execute();
    }
}
