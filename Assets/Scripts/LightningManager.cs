using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{
	public int maxBolts = 4;
	public float radius = 3f;
	public float delay = 0.1f;

	public List<GameObject> targets = new List<GameObject>();
	
	private List<GameObject> bolts = new List<GameObject>();
	private LineRenderer modelLR;

	void Start() {
		modelLR = GetComponent<LineRenderer>();
		StartCoroutine(ProcBolts());
	}
	IEnumerator ProcBolts() {
		while (true) {
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, (1 << 9));
			foreach (Collider hC in hitColliders) {
				if (!targets.Contains(hC.gameObject)) {
						targets.Add(hC.gameObject);
					if (bolts.Count < maxBolts) {
						GameObject g = new GameObject();
						g.AddComponent<LineRenderer>();
						g.GetComponent<LineRenderer>().colorGradient = modelLR.colorGradient;
						g.GetComponent<LineRenderer>().material = modelLR.material;
						g.GetComponent<LineRenderer>().startWidth = modelLR.startWidth;
						g.GetComponent<LineRenderer>().endWidth = modelLR.endWidth;
						g.GetComponent<LineRenderer>().widthCurve = modelLR.widthCurve;
						g.AddComponent<Lightning>();
						g.transform.SetParent(transform);
						g.transform.position = transform.position;
						g.name = "bolt";
						g.GetComponent<Lightning>().target = hC.gameObject;
						g.GetComponent<Lightning>().radius = radius;
						g.GetComponent<Lightning>().BeginBolt();
						bolts.Add(g);
					}
				}
			}
			for (int i = 0; i < targets.Count; i++) {
				if (Vector3.Distance(transform.position, targets[i].transform.position) > radius) {
					targets[i] = null;
				}
			}
			targets.RemoveAll(target => target == null);
			bolts.RemoveAll(bolt => bolt == null);
			yield return new WaitForSeconds(delay);
		}
	}
}
