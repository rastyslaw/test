using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
	void Start ()
	{
	    Button button = GetComponent<Button>(); 
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    { 
        PauseController.Paused = !PauseController.Paused;
        Messenger.Broadcast(EventTypes.PAUSE);
    }
}
