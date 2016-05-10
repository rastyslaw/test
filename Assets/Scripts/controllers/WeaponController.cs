using UnityEngine;
using System.Collections.Generic; 

public class WeaponController : MonoBehaviour {
    
    private float duration = 0.7f;
    
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private int pooledAmount = 12;

    private const float WEAPON_SIZE = 2.0f;
    private const float ANGLE = 15.0f;

    private Animator animator;

    private List<GameObject> bullets;
    private List<GameObject> selectedBullets;

    void Awake()
    {
        selectedBullets = new List<GameObject>();
        animator = gameObject.GetComponent<Animator>(); 
    }

    void Start ()
    {
        bullets = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false); 
            bullets.Add(obj);
        }
        InvokeRepeating("Fire", duration, duration);
        animator.speed = 2/duration*0.28f;
    }
   
    void Fire()
    {
        int count = (int) DataModel.GetValue(Names.WEAPON);
        selectedBullets.Clear();

        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                float posX = transform.position.x - WEAPON_SIZE * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
                float posY = transform.position.y + WEAPON_SIZE * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
                bullets[i].transform.position = new Vector3(posX, posY, transform.position.z);
                selectedBullets.Add(bullets[i]);
                bullets[i].SetActive(true);
                count--;
                if (count == 0)
                {
                    break;
                } 
            }
        }

        float bonusAngle = ANGLE*(selectedBullets.Count-1);
        float angle = bonusAngle/2;
        foreach (var bullet in selectedBullets)
        {
            bullet.transform.rotation = transform.rotation;
            //bullet.transform.Rotate(0, 0, angle);
            bullet.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0);
            //bullet.transform.rotation *= Quaternion.Euler(0, 0, angle);
            angle -= ANGLE;
        }
    }
   
}