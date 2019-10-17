using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public GameObject player;
	public float smoothTime = 0.3f;
	private Transform target;
	private Vector3 velocity = Vector3.zero;

	void Start() {
		target = player.GetComponent<Rigidbody>().transform;
	}

	void FixedUpdate() {
		Vector3 destination = new Vector3(target.position.x, 20, target.position.z-20);
		transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, smoothTime);
	}
}
