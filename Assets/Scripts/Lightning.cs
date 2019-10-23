using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
	public GameObject target;

	[Range(0.1f, 10)] public float distThreshold = 3.0f;
	[Range(0.1f, 10)] public float arcLength = 1f;
	[Range(0.1f, 10)] public float arcVariation = 1f;
	[Range(0.1f, 10)] public float delay = 0.1f;

	private LineRenderer lr;

	void Start() {
		lr = GetComponent<LineRenderer>();
		lr.SetVertexCount(1);
		StartCoroutine("Bolts");
	}

	IEnumerator Bolts() {
		while (true) {
		Vector3 currPos = transform.position;
		lr.SetPosition(0, currPos);
		int currVert = 1;
		while (Vector3.Distance(currPos, target.transform.position) > distThreshold) {
			lr.SetVertexCount(currVert + 1);
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
		lr.SetVertexCount(currVert + 1);
		lr.SetPosition(currVert, target.transform.position);
		yield return new WaitForSeconds(delay);
		}
	}
}
