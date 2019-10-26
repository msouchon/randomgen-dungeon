using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float startingHealth = 100;

	public float currentHealth;

	public Material hitMaterial;
	public float flashDuration = 0.1f;
	public GameObject explosion;

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
		Instantiate(explosion, transform.position, transform.rotation);

		int dropChance = Random.Range(0, 5);
		Debug.Log(dropChance);
		// make a drop
		if(dropChance == 1) {
			Spells playerSpells = GameObject.Find("Player").GetComponent<Spells>();
			int spellDrop = Random.Range(0, playerSpells.spells.Count-1);
			Debug.Log(playerSpells.spells[spellDrop].spell);
			if(playerSpells.spells[spellDrop].drop) {
				Instantiate(playerSpells.spells[spellDrop].drop, transform.position, Quaternion.identity);
			}
		}

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
