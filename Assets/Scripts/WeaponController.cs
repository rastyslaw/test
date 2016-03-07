using UnityEngine;
using System.Collections.Generic; 

public class WeaponController : MonoBehaviour {
    
    private float duration = 0.5f;
    
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private int pooledAmount = 8;

    private const float WEAPON_SIZE = 2.0f;
    private Animator animator;

    private List<GameObject> fireballs;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>(); 
    }

    void Start ()
    {
        fireballs = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false); 
            fireballs.Add(obj);
        }
        InvokeRepeating("Fire", duration, duration);
        animator.speed = 2/duration*0.28f;
    }
    public void Control()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            print("space");
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
           Debug.Log("RightControl");
        }
    }

    void Fire()
    {
        for (int i = 0; i < fireballs.Count; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                float posX = transform.position.x - WEAPON_SIZE * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
                float posY = transform.position.y + WEAPON_SIZE * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
                fireballs[i].transform.position = new Vector3(posX, posY, transform.position.z);
                fireballs[i].transform.rotation = transform.rotation;
                fireballs[i].SetActive(true);
                break; 
            }
        }
    }
   
}