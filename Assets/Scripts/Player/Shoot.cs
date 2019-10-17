using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public GameObject bullet;
	public float bulletSpeed = 20;

	void Update() {
		if (Input.GetKeyDown("space")) {
			GameObject b = Instantiate(bullet);
			b.transform.position = transform.position;
			b.GetComponent<BulletController>().velocity = transform.right * bulletSpeed;
		}
	}

}
