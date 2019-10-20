using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public GameObject bullet;
	public float bulletSpeed = 20;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Input.GetKeyDown("space")) {
			GameObject b = Instantiate(bullet);
			b.transform.position = transform.position;
			b.GetComponent<BulletController>().velocity = transform.forward * bulletSpeed;
		}
	}

}
