using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WeaponController : MonoBehaviour {
    
    [SerializeField]
    private GameObject bullet;

    private float _speed;
    private int _pooledAmount;

    private const float WEAPON_SIZE = 2.0f;
    private const float ANGLE = 15.0f;

    private Animator animator;
    private WeaponVO _weaponData;
    private Sprite _bulletSprite;
    private List<GameObject> bullets;
    private List<GameObject> selectedBullets;

    void Awake()
    {
        selectedBullets = new List<GameObject>();
        animator = gameObject.GetComponent<Animator>(); 
    }

    void Start ()
    {
        DataModel.SetValue(Names.WEAPON_ID, 3);
        List<WeaponVO> weapons = DataModel.GetValue(Names.WEAPONS) as List<WeaponVO>;
        int weaponId = (int)DataModel.GetValue(Names.WEAPON_ID);

        IEnumerable<WeaponVO> result = from weapon in weapons where weapon.Id == weaponId select weapon;
        _weaponData = result.ElementAt(0);

        Texture2D texture = Resources.Load("bullets/" + _weaponData.Resource) as Texture2D; 
        _bulletSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 128f);

        _speed = _weaponData.Speed;
        _pooledAmount = (int)Mathf.Ceil(1.5f /_speed) * weaponId;

        bullets = new List<GameObject>();
        for (int i = 0; i < _pooledAmount; i++)
        {
            GameObject obj = Instantiate(bullet);
            obj.SetActive(false); 
            bullets.Add(obj);
        }
        InvokeRepeating("Fire", _speed, _speed); 
        animator.speed = 2/_speed*0.28f;

        Messenger.AddListener<bool>(EventTypes.STAGE_COMPLETED, OnStageCompleted);
    }

    void OnStageCompleted(bool isWin)
    {
       CancelInvoke("Fire");
       animator.Stop();
    }

    void Fire()
    {
        int count = _weaponData.Id;
        selectedBullets.Clear();

        Bullet selectedBullet;
        GameObject bulletPrefab;
        for (int i = 0; i < bullets.Count; i++)
        {
            bulletPrefab = bullets[i];
            if (!bulletPrefab.activeInHierarchy)
            {
                selectedBullet = bulletPrefab.GetComponent<Bullet>();
                selectedBullet.Damage = _weaponData.Damage;
                selectedBullet.BulletSprite = _bulletSprite; 
                float posX = transform.position.x - WEAPON_SIZE * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad);
                float posY = transform.position.y + WEAPON_SIZE * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad);
                bulletPrefab.transform.position = new Vector3(posX, posY, transform.position.z);
                selectedBullets.Add(bulletPrefab);
                bulletPrefab.SetActive(true);
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
            bullet.transform.rotation *= Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), 1);
            //bullet.transform.rotation *= Quaternion.Euler(0, 0, angle);
            angle -= ANGLE;
        }
    }

    void OnDestroy() 
    {
        Messenger.RemoveListener<bool>(EventTypes.STAGE_COMPLETED, OnStageCompleted);
    }
}