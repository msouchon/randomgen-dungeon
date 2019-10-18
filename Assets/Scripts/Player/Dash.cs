using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
	public float dashForce = 100f;
	public float trailTime = 1f;

	private float currentTrailTime = 0;
	private TrailRenderer tr;
	private Rigidbody rb;

	public GameObject panel;

	void Start() {
		rb = GetComponent<Rigidbody>();
		tr = GetComponent<TrailRenderer>();
		tr.emitting = false;
	}

	void Update() {
		if (Input.GetKeyDown("left shift")) {
			tr.emitting = true;
			currentTrailTime = trailTime;
			rb.velocity = Vector3.zero;
			rb.AddForce(transform.right * dashForce, ForceMode.VelocityChange);
			GetComponent<Movement>().enableMovement = false;
			panel.SetActive(true);
		}
		if (currentTrailTime <= 0) {
			tr.emitting = false;
			GetComponent<Movement>().enableMovement = true;
		}
		else currentTrailTime -= Time.deltaTime;
	}
}
