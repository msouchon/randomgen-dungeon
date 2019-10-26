using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float startingHealth = 100;

	public float currentHealth;

	public Material hitMaterial;
	public float flashDuration = 0.1f;
	public GameObject fallApart;

	private MeshRenderer mr;

	void Start() {
		currentHealth = startingHealth;
		mr = GetComponent<MeshRenderer>();
	}

	public void ApplyDamage(float damage) {
		StartCoroutine(Flash());
		currentHealth -= damage;
		if (currentHealth <= 0)
			StartCoroutine(Die());
	}

	IEnumerator Die() {
		Instantiate(fallApart, transform.position, transform.rotation);
		Destroy(this.gameObject);
    
    int dropChance = Random.Range(0, 5);
    // make a drop
    if (dropChance == 0)
      {
          Spells playerSpells = GameObject.Find("Player").GetComponent<Spells>();
          int spellDrop = Random.Range(0, playerSpells.spells.Count);
          if (playerSpells.spells[spellDrop].drop)
          {
              GameObject d = Instantiate(playerSpells.spells[spellDrop].drop, transform.position, Quaternion.Euler(0, 180, 0));
	      d.transform.position -= Vector3.up * 0.9f;
          }
      }
		yield return null;
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
