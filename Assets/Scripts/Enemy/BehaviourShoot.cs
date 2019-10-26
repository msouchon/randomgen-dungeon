using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourShoot : MonoBehaviour
{
	public GameObject player;
	public GameObject bullet;
	public float speed;
	public float awarenessRange;
	public float avoidanceRange;
	public float shootCooldown;
	public float duration;
	public float bulletSpeed;


	private Rigidbody rb;
	private bool shooting = false;

	void Start() {
		rb = GetComponent<Rigidbody>();
		player = GameObject.Find("Player");
		StartCoroutine(Shoot());
	}

	void Update() {
		if (player.GetComponent<Invisibility>().visible) {
			int layerMask = (1 << 8);
			float distance = Vector3.Distance(transform.position, player.transform.position);
			if (distance <= awarenessRange) {
				if (!Physics.Linecast(transform.position, player.transform.position, layerMask)) {
					int direction = 1;
					if (distance < avoidanceRange) direction = -1;
					transform.LookAt(player.transform.position);
					rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime * direction));
					shooting = true;
				}
				else shooting = false;
			}
			else shooting = false;
		}
		else shooting = false;
	}

	IEnumerator Shoot() {
		shooting = true;
		while (true) {
			if (shooting) {
				GameObject b = Instantiate(bullet);
				b.transform.position = transform.position;
				b.GetComponent<EnemyBulletController>().direction = transform.forward;
				b.GetComponent<EnemyBulletController>().speed = bulletSpeed;
				Destroy(b, duration);
			}
			yield return new WaitForSeconds(shootCooldown);
		}
	}
}
