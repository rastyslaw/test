using UnityEngine;

public class WindowsFactory : MonoBehaviour
{
    private static Canvas canvas;

    public static GameObject Build(GameObject prefab)
    {
        GameObject window = Instantiate(prefab);
        if (canvas == null)
        {
            canvas = GameObject.FindObjectOfType<Canvas>();
        }
        if (window != null)
        {
            Transform t = window.transform;
            t.SetParent(canvas.gameObject.transform, false);
            //t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            //window.layer = parent.layer;
        }
        return window;
    }

    public static GameObject InstantiatePrefab(GameObject prefab)
    {
        return Instantiate(prefab);
    }
}