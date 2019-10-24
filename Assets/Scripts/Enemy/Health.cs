using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float startingHealth = 100;

	public float currentHealth;

	void Start() {
		currentHealth = startingHealth;
	}

	public void ApplyDamage(float damage) {
		currentHealth -= damage;
		if (currentHealth <= 0)
			Destroy(this.gameObject);
	}
}
