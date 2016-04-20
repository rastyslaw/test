using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private bool inAttack;  

    private const float bottom = 5.0f; 

    private float MaxHealth;
    private float _curHealth;

    private GameObject _healtBar;
    private AbstractEnemy enemy;

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
        enemy = GetComponent<AbstractEnemy>(); 
        GameObject canvas = transform.Find("Canvas").gameObject;
        if (canvas != null)  
        {
            _healtBar = canvas.transform.Find("fill").gameObject;
        } 
        _curHealth = MaxHealth = enemy.Data.hp;  
    }

    void Update()
    {
        if (transform.position.y > -(bottom - enemy.Data.distance))
        {
            transform.Translate(0, -enemy.Data.speed *Time.deltaTime, 0);
        }
        else if(!inAttack)
        {
            inAttack = true;
            InvokeRepeating("Attack", enemy.Data.attackSpeed, enemy.Data.attackSpeed);
        }
    }

    void Attack() 
    {
        enemy.Attack(); 
        Messenger.Broadcast<float>(EventTypes.DAMAGE, enemy.Data.damage);   
    }
}
