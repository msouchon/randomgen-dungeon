using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	public float cooldown = 1;
	public float distance = 10;

	private float currentCooldown = 0;
	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		if (Input.GetKeyDown("space")) {
			dash();
		}
		if (currentCooldown > 0)
		currentCooldown -= Time.deltaTime;
	}

	void dash() {
		if (currentCooldown <= 0) {
		Debug.Log("attempted dash");
			rb.MovePosition(transform.position + transform.forward * distance);
			currentCooldown = cooldown;
		}
	}
}
