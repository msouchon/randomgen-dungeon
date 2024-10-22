﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float damage = 25;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.MovePosition(rb.transform.position + direction * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerHealth>() != null)
            {
                other.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);
                Destroy(this.gameObject);
            }
        }
    }

}
