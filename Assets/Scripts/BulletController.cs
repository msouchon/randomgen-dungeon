using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public Vector3 velocity;

	void Update() {
		this.transform.Translate(velocity * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player") {
			Destroy(this.gameObject);
		}
	}
}
