using UnityEngine;

public class PauseOnSuspend : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnApplicationPause(bool pauseStatus)
    {
       // if (pauseStatus && !PauseController.Paused)
        //{
            PauseController.Paused = pauseStatus; 
        //}
    }
}
