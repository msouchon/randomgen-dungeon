using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
	public int flashes;
	public float flashInterval;
	public float flashDuration;
	public GameObject explosion;
	public float explosionDuration;
	public float explosionRadius;
	public float explosionDamage = 100f;
	public Material flashMaterial;
	
	private MeshRenderer mr;
	private Material m;

	void Start() {
		mr = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
		m = mr.material;
		StartCoroutine(Explode());
	}
	
	IEnumerator Explode() {
		for (int i = 0; i < flashes; i++) {
			yield return new WaitForSeconds(flashInterval);
			mr.material = flashMaterial;
			yield return new WaitForSeconds(flashDuration);
			mr.material = m;
		}
		GameObject g = Instantiate(explosion);
		g.transform.position = transform.position;
		Destroy(g, explosionDuration);
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider hC in hitColliders) {
			if (hC.gameObject.GetComponent<Health>() != null) {
				hC.GetComponent<Health>().ApplyDamage(explosionDamage);
			}
		}
		Destroy(this.gameObject);
	}
}
