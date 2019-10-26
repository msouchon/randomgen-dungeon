using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	public GameObject laser;
	public GameObject laserEffect;
	public float laserDamage = 100f;
	public float laserDelay = 1.0f;
	public float laserWidth = 0.5f;
	public float laserDuration = 0.5f;
	public float maxDist = 100f;

	void Update() {
		if (Input.GetKeyDown("space")) {
			StartCoroutine(ShootLaser());
		}
	}

	IEnumerator ShootLaser() {
		GameObject e = Instantiate(laserEffect);
		e.transform.position = transform.position + Vector3.down;
		e.transform.SetParent(transform);
		Destroy(e, laserDelay);
		yield return new WaitForSeconds(laserDelay);
		GameObject g = Instantiate(laser);
		Destroy(g, laserDuration);
		g.transform.position = transform.position;
		RaycastHit[] hits = Physics.SphereCastAll(transform.position, laserWidth/2, transform.forward, maxDist);
		RaycastHit finalHit = new RaycastHit();
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.layer == 8) {
				finalHit = hit;
				break;
			}
		}
		foreach (RaycastHit hit in hits) {
			if (hit.distance < finalHit.distance) {
				if (hit.collider.gameObject.GetComponent<Health>() != null) {
					hit.collider.gameObject.GetComponent<Health>().ApplyDamage(laserDamage);
				}
			}
		}
		LineRenderer lr = g.GetComponent<LineRenderer>();
		lr.positionCount = 2;
		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, finalHit.point);
	}
}
