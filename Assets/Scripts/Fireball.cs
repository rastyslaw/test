using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float speed = 10.0f;
    private float damage = 30.0f; 
    private CircleCollider2D circleCollider;
//  private ParticleSystem trail;
    private SpriteRenderer bullet;
    private const string IS_ENEMY = "enemy"; 

    void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
//      trail = GetComponentInChildren<ParticleSystem>();
        bullet = GetComponent<SpriteRenderer>(); 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == IS_ENEMY)
        {
            transform.rotation = Quaternion.identity;
            circleCollider.enabled = false;
            bullet.enabled = false;
//          trail.Stop();

            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.CurHealth -= damage; 
        }
    }

    void OnEnable()
    {
        circleCollider.enabled = true;
        bullet.enabled = true; 
        Invoke("Destroy", 4f);
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
    }
   
}
