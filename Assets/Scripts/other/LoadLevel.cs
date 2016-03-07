using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour {

    public Texture2D emptyProgressBar; // Set this in inspector.
    public Texture2D fullProgressBar; // Set this in inspector.

    private AsyncOperation async = null; // When assigned, load is in progress.

    private IEnumerator LoadALevel(string levelName)
    {
        async = SceneManager.LoadSceneAsync(levelName);
        yield return async; 
    }

    void OnGUI()
    {
        if (async != null)
        {
            GUI.DrawTexture(new Rect(0, 0, 100, 50), emptyProgressBar); 
            GUI.DrawTexture(new Rect(0, 0, 100 * async.progress, 50), fullProgressBar);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(0, 0, 100, 50), string.Format("{0:N0}%", async.progress * 100f));
        }
    }
}
