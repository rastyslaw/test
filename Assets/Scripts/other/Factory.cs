using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

    public GameObject basic;

    public enum enemiesType
    {
        Enemy1,
        Enemy2,
    }

    public GameObject Build(enemiesType type)
    {
        GameObject enemy = (GameObject)Instantiate(basic, Vector3.zero, Quaternion.identity);
        enemy.name = type.ToString();
        Texture2D _texture = Resources.Load("monster2") as Texture2D;  
        enemy.GetComponent<SpriteRenderer>().sprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 128f);
        enemy.AddComponent(System.Type.GetType(type.ToString()));
        return enemy;
    }
    
}
