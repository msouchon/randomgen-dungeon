using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
	public GameObject target;
	public float radius;

	[Range(0.1f, 10)] public float distThreshold = 3.0f;
	[Range(0.1f, 10)] public float arcLength = 1f;
	[Range(0.1f, 10)] public float arcVariation = 1f;
	[Range(0.1f, 10)] public float delay = 0.1f;

	private LineRenderer lr;

	void Start() {
		lr = GetComponent<LineRenderer>();
		lr.enabled = false;
	}

	public void BeginBolt() {
		StartCoroutine(Bolt());
	}

	IEnumerator Bolt() {
		lr = GetComponent<LineRenderer>();
		while (true) {
			if (target == null || (Vector3.Distance(transform.position, target.transform.position) > radius)) {
				Destroy(this.gameObject);
				break;
			}
			lr.enabled = true;
			int currVert = 1;
			Vector3 currPos = transform.position;
			lr.positionCount = currVert;
			lr.SetPosition(0, currPos);
			while (Vector3.Distance(currPos, target.transform.position) > distThreshold) {
				lr.positionCount++;
				Vector3 dir = target.transform.position - currPos;
				dir = dir.normalized;
				dir += new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
				dir = dir.normalized;
				dir *= Random.Range(arcVariation * arcLength, arcLength);
				dir += currPos;
				lr.SetPosition(currVert, dir);
				currVert++;
				currPos = dir;
			}
			lr.positionCount++;
			lr.SetPosition(currVert, target.transform.position);
			yield return new WaitForSeconds(delay);
		}
	}
}
