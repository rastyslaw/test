using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private float speed = 10.0f;
    private int damage; 
    private Sprite bulletSprite; 

    private CircleCollider2D circleCollider;
//  private ParticleSystem trail;
    private SpriteRenderer _bulletSpriteRenderer;

    private const string IS_ENEMY = "enemy";

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public Sprite BulletSprite
    {
        set
        {
            bulletSprite = value;
            _bulletSpriteRenderer.sprite = bulletSprite;
        }
    }

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
//      trail = GetComponentInChildren<ParticleSystem>();
        _bulletSpriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == IS_ENEMY)
        {
            transform.rotation = Quaternion.identity;
            circleCollider.enabled = false;
            _bulletSpriteRenderer.enabled = false;
//          trail.Stop();

            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.CurHealth -= Damage; 
        }
    }

    void OnEnable()
    {
        circleCollider.enabled = true;
        _bulletSpriteRenderer.enabled = true; 
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Update ()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        if (transform.position.y > 6 || (transform.position.x > 4 || transform.position.x < -4))
        {
            Destroy();
        }
    }
   
}
