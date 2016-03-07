using UnityEngine;

public class WindowsFactory : MonoBehaviour {

    static public GameObject Build(GameObject prefab) 
    {
        GameObject window = (GameObject)Instantiate(prefab);
        if (window != null)
        {
            Transform t = window.transform;
            t.SetParent(GameObject.FindObjectOfType<Canvas>().gameObject.transform, false); 
            //t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            //window.layer = parent.layer;
        } 
        return window;
    }
}