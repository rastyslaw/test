using UnityEngine;
using System.Collections;
using commands.test;
using UnityEngine.SceneManagement;

public class ParseXMLCommand : MonoBehaviour {
    
	void Awake ()
	{
	    MacroCommand macroCommand = new MacroCommand();
        macroCommand.RegisterCompleteCallback(OnComplete);
        macroCommand.Execute();
    }

    void OnComplete(bool success)
    {
        SceneManager.LoadScene(SceneId.MAIN);
    }
}
