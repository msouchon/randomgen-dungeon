﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public GameObject bullet;
	public float bulletSpeed = 20;
	public float duration = 4.0f;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Input.GetKeyDown("space")) {
			GameObject b = Instantiate(bullet);
			b.transform.position = transform.position;
			b.GetComponent<BulletController>().direction = transform.forward;
			b.GetComponent<BulletController>().speed = bulletSpeed;
			Destroy(b, duration);
		}
	}

}
