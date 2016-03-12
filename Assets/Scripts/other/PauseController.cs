using UnityEngine;
using System.Collections;

public static class PauseController
{
    public static bool Paused 
    {
        get { return Time.timeScale < 0.01f; }
        set { Time.timeScale = value ? 0 : 1; }
    }
}