using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBulletController : MonoBehaviour
{
	public Vector3 direction;
	public float speed;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		rb.MovePosition(rb.transform.position + direction * Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Wall") {
			RaycastHit hit;
			Physics.Raycast(transform.position, direction, out hit);
			direction = Vector3.Reflect(direction, hit.normal);
		}
	}
}
