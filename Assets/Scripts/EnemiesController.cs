using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private float duration = 3f;
    
    private Factory factory;

    void Awake()
    {
        factory = GetComponent<Factory>(); 
    }

    void Start ()
    {
        InvokeRepeating("CreateEnemy", duration, duration);
    } 
	
	void CreateEnemy()
	{
        Factory.enemiesType enemiesType = Utils.RandomEnumValue<Factory.enemiesType>();  
        GameObject enemy = factory.Build(enemiesType);
	    enemy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6.5f, 0);
	}

    void AddSprite(GameObject enemy, Texture2D _texture) 
    {
        Sprite newSprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 128f);
        SpriteRenderer sprRenderer = enemy.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = newSprite; 
     }

}
