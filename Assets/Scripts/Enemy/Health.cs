using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float startingHealth = 100;

	public float currentHealth;

	public Material hitMaterial;
	public float flashDuration = 0.1f;

	private MeshRenderer mr;

	void Start() {
		currentHealth = startingHealth;
		mr = GetComponent<MeshRenderer>();
	}

	public void ApplyDamage(float damage) {
		StartCoroutine(Flash());
		currentHealth -= damage;
		if (currentHealth <= 0)
			//Destroy(this.gameObject);
			StartCoroutine(Die());
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(flashDuration);
		Destroy(this.gameObject);
	}
	
	IEnumerator Flash() {
		Material tmp = mr.material;
		mr.material = hitMaterial;
		float healthRatio = currentHealth / startingHealth;
		Color c = mr.material.color;
		c.r = 1 - healthRatio;
		mr.material.color = c;
		yield return new WaitForSeconds(flashDuration);
		mr.material = tmp;
	}
}
