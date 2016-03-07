using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private bool inAttack;  

    private const float bottom = 5.0f; 

    private float distance = 2.0f;
    private float speed = 1.1f;
    private float attackSpeed = 2.0f;
    private float damage = 5.0f;

    private const float MaxHealth = 100.0f;
    private float _curHealth;

    private GameObject _healtBar;
    private IEnemy enemy;

    public float CurHealth
    {
        get { return _curHealth; }
        set
        { 
            _curHealth = value; 
            if (_curHealth <= 0) 
            {
                CancelInvoke("Attack"); 
                Destroy(gameObject);
                return;
            }
            _healtBar.transform.localScale = new Vector3(_curHealth / MaxHealth, _healtBar.transform.localScale.y, _healtBar.transform.localScale.z);
        }
    }
    
    void Start()
    {
        enemy = GetComponent<IEnemy>(); 
        GameObject canvas = transform.Find("Canvas").gameObject;
        if (canvas != null)  
        {
            _healtBar = canvas.transform.Find("fill").gameObject;
        } 
        _curHealth = MaxHealth;  
    }

    void Update()
    {
        if (transform.position.y > -(bottom - distance))
        {
            transform.Translate(0, -speed*Time.deltaTime, 0);
        }
        else if(!inAttack)
        {
            inAttack = true;
            InvokeRepeating("Attack", attackSpeed, attackSpeed);
        }
    }

    void Attack() 
    {
        enemy.Attack(); 
        Messenger.Broadcast<float>(EventTypes.DAMAGE, damage);   
    }
}
