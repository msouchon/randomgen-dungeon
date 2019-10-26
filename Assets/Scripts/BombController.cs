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
	public float targetRadius;
	public float speed;
	public float explosionDamage = 100f;
	public Material flashMaterial;
	
	private MeshRenderer mr;
	private Material m;
	private bool targetFound = false;
	private GameObject target;
	private Vector3 nextPosition;
	private Rigidbody rb;

	void Start() {
		mr = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
		m = mr.material;
		rb = GetComponent<Rigidbody>();
		StartCoroutine(Explode());
	}

	void Update() {
		if (!targetFound) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, targetRadius, (1 << 9));
			if (hitColliders.Length > 0) {
				targetFound = true;
				target = hitColliders[0].gameObject;
			}
		}
		if (target != null) {
			rb.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime));
		}
		else {
			targetFound = false;
			rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
		}
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
