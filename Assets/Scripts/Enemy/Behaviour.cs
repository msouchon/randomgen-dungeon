using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
	public GameObject player;
	public float speed;
	public float awarenessRange;

	private Rigidbody rb;

	void Start() {
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
	}

	void Update() {
		float distance = Vector3.Distance(transform.position, player.transform.position);
		if (distance <= awarenessRange) {
			transform.LookAt(player.transform);
			rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
		}
	}
}
