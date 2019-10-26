using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
	public GameObject player;
	public float speed;
	public float awarenessRange;
	public float explodeRange;
	public int flashes;
	public float flashInterval = 1.0f;
	public float flashDuration = 0.2f;
	public GameObject explosion;
	public float explosionDuration;
	public float explosionRadius;
	public float explosionDamage = 100f;
	public GameObject fallApart;

	public Material flashMaterial;

	private Rigidbody rb;
	private MeshRenderer mr;
	private Material m;
	private bool exploding = false;

	void Start() {
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		mr = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
		m = mr.material;
	}

	void Update() {
		if (player.GetComponent<Invisibility>().visible) {
			int layerMask = (1 << 8);
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if (distance <= awarenessRange) {
				if (!Physics.Linecast(transform.position, player.transform.position, layerMask)) {
					transform.LookAt(player.transform.position);
					rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
				}
			}
			if (!exploding && distance <= explodeRange) {
				exploding = true;
				StartCoroutine(Explode());
			}
		}
	}

	IEnumerator Explode() {
		for (int i = 0; i < flashes; i++) {
			yield return new WaitForSeconds(flashInterval);
			mr.material = flashMaterial;
			yield return new WaitForSeconds(flashDuration);
			mr.material = m;
		}
		GetComponent<Health>().ApplyDamage(GetComponent<Health>().startingHealth);
		GameObject g = Instantiate(explosion);
		g.transform.position = transform.position;
		Destroy(g, explosionDuration);
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider hC in hitColliders) {
			if (hC.gameObject.GetComponent<Health>() != null) {
				hC.gameObject.GetComponent<Health>().ApplyDamage(explosionDamage);
			}
		}

	}
}
