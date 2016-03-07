using UnityEngine;
using System.Collections;

public class EnemiesController : MonoBehaviour
{
    [SerializeField]
    private float duration = 3f;
    
    private EnemiesFactory _enemiesFactory;

    void Awake()
    {
        _enemiesFactory = GetComponent<EnemiesFactory>(); 
    }

    void Start ()
    {
        InvokeRepeating("CreateEnemy", duration, duration);
        Messenger.Broadcast<WindowsId>(EventTypes.SHOW_WINDOW, WindowsId.InfoWindow); 
    } 
	
	void CreateEnemy()
	{
        EnemiesFactory.enemiesType enemiesType = Utils.RandomEnumValue<EnemiesFactory.enemiesType>();  
        GameObject enemy = _enemiesFactory.Build(enemiesType);
	    AddSprite(enemy, Resources.Load("monster") as Texture2D);

        enemy.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), 6.5f, 0);
	}

    void AddSprite(GameObject enemy, Texture2D _texture)  
    {
        Sprite newSprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 128f);
        SpriteRenderer sprRenderer = enemy.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = newSprite; 
     }

}
