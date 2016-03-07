﻿using UnityEngine;
using System.Collections.Generic; 

public class WeaponController : MonoBehaviour {

    [SerializeField]
    private float duration = 0.5f;
    
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private int pooledAmount = 8;

    private const float WEAPON_SIZE = 2.0f;

    private List<GameObject> fireballs;   

    // Use this for initialization
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