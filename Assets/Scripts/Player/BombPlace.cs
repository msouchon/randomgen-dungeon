using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour
{
	public GameObject bomb;

	void Update() {
		if (Input.GetKeyDown("space")) {
			Instantiate(bomb, transform.position + transform.forward, transform.rotation);
		}
	}
}
