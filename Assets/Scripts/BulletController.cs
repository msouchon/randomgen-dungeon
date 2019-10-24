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
		rb.MovePosition(rb.transform.position + direction * Time.deltaTime * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player") {
			if (other.gameObject.GetComponent<Health>() != null) {
				other.gameObject.GetComponent<Health>().ApplyDamage(50);
			}
			if (other.gameObject.tag == "Wall") {
				RaycastHit hit;
				Physics.Raycast(transform.position, direction, out hit);
				direction = Vector3.Reflect(direction, hit.normal);
			}
			else if (other.gameObject.tag != "Bullet") {
				Destroy(this.gameObject);
			}
		}
	}

}
