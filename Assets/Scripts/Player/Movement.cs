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

	void Start() {
		rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate() {
		if (enableMovement) {
			Vector3 x, z;
			x = Vector3.right * Input.GetAxis("Horizontal") * speed; 
			z = Vector3.forward * Input.GetAxis("Vertical") * speed;
			rb.MovePosition(rb.transform.position + x + z);
		}

		Vector3 position = c.WorldToScreenPoint(transform.position);
		Vector3 direction = Input.mousePosition - position;
		float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
	}
}
