using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public Vector3 direction;
	public float speed;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		this.transform.Translate(direction * Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player") {
			if (other.gameObject.GetComponent<Health>() != null) {
				other.gameObject.GetComponent<Health>().ApplyDamage(50);
			}
			RaycastHit hit;
			Physics.Raycast(transform.position, direction, out hit);
			direction = Vector3.Reflect(direction, hit.normal);
		}
	}

}
