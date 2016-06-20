using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractEnemy: MonoBehaviour
{
    private string respackName = "monsters/";

    private EnemyVO data;

    public EnemyVO Data
    {
        get { return data; }
        set { data = value; }
    }

    void Awake()
    {
        /*
        List<EnemyVO> enemies = DataModel.GetValue(Names.ENEMIES) as List<EnemyVO>;
        string type = GetType().ToString();
        foreach (EnemyVO enemyData in enemies)
        {
            if (enemyData.type == type)
            {
                data = enemyData;
                break;
            }
        }
        */
    }

    void Start()
    {
        AddSprite(gameObject, Resources.Load(respackName + Data.resource) as Texture2D);
    }

    void AddSprite(GameObject enemy, Texture2D _texture)
    {
        Sprite newSprite = Sprite.Create(_texture, new Rect(0f, 0f, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 128f);
        SpriteRenderer sprRenderer = enemy.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = newSprite;
    }

    public abstract void Attack();

    public void Stop()
    {
        Enemy enemy = GetComponentInParent<Enemy>();
        enemy.StageCompleted = true;
    }
}
