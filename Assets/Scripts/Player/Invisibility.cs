using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
	public float duration = 5f;
	public Material invisMaterial;
	public Material normalMaterial;
	public bool visible = true;
	public GameObject effect;
	
	private MeshRenderer mr;
	private MeshRenderer[] mrChildren;

	void Start() {
		mr = GetComponent<MeshRenderer>();
		mrChildren = GetComponentsInChildren<MeshRenderer>();
	}

	void Update() {
		if (visible && Input.GetKeyDown("space")) {
			StartCoroutine(GoInvisible());
		}
	}

	IEnumerator GoInvisible() {
		visible = false;
		GameObject g = Instantiate(effect);
		g.transform.position = transform.position;
		g.transform.SetParent(transform);
		Destroy(g, duration);
		foreach (MeshRenderer mrC in mrChildren) {
			mrC.material = invisMaterial;
		}
		yield return new WaitForSeconds(duration);
		visible = true;
		foreach (MeshRenderer mrC in mrChildren) {
			mrC.material = normalMaterial;
		}
	}
}
