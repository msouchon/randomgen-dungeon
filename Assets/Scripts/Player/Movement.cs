using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Rigidbody rb;
	public float speed;
	public float maxScreenMovement;
	public Camera c;
	public bool enableMovement = true;

	private Quaternion rotation;

	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update() {
		Plane plane = new Plane(Vector3.up, -transform.position.y);
		Ray ray = c.ScreenPointToRay(Input.mousePosition);
		float hitDistance;

		if (plane.Raycast(ray, out hitDistance)) {
			Vector3 mousePos = ray.GetPoint(hitDistance);
			rotation = Quaternion.LookRotation(mousePos - transform.position, Vector3.up);
		}
	}
	void FixedUpdate() {
		if (enableMovement) {
			Vector3 x, z;
			x = Vector3.right * Input.GetAxis("Horizontal") * speed; 
			z = Vector3.forward * Input.GetAxis("Vertical") * speed;
			rb.MovePosition(rb.transform.position + x + z);
		}
		transform.rotation = rotation;
	}
}
