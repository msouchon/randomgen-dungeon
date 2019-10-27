using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float MaxHealth = 1000;
    public float health = 1000;

    // Start is called before the first frame update
    void Start()
    {
        health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Levels.isDead = true;
            Destroy(this.gameObject);
        }
    }
}
