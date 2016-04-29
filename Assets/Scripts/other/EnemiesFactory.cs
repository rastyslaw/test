using UnityEngine;
using System.Collections;

public class EnemiesFactory : MonoBehaviour {

    public GameObject basic;
    
    public GameObject Build(EnemyTypes type)
    {
        GameObject enemy = (GameObject)Instantiate(basic, Vector3.zero, Quaternion.identity);
        enemy.name = type.ToString();
        return enemy;
    }
    
}
