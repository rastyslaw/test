using UnityEngine;
using System.Collections;

public class HpBar:MonoBehaviour {
    
    [SerializeField]
    private Texture2D emptyTex;

    [SerializeField]
    private Texture2D fullTex;

    private float value = 100.0f;
    private Vector2 pos = new Vector2(0, 0);
    private Vector2 size = new Vector2(100, 20);

    private IEnemy enemy;
    public IEnemy Enemy
    {
        get { return enemy; }
        set
        {
            enemy = value;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(pos.x, pos.y, size.x, size.y), emptyTex); 
        GUI.Box(new Rect(pos.x, pos.y, size.x * value, size.y), fullTex);
    }
    
    void Update()
    {
        value -= 0.01f; 
        print("value:" + value);
    }
}
