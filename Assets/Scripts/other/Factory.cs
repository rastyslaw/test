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
        enemy.AddComponent(System.Type.GetType(type.ToString()));
        return enemy;
    }
    
}
