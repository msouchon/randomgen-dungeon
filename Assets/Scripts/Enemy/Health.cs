using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int startingHealth = 100;

	public int currentHealth;

	void Start() {
		currentHealth = startingHealth;
	}

	public void ApplyDamage(int damage) {
		currentHealth -= damage;
		if (currentHealth <= 0)
			Destroy(this.gameObject);
	}
}
