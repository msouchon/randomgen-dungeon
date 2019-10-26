using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlace : MonoBehaviour, ISpell
{
	public GameObject bomb;

	void Update() {
	}

	public void doAction() {
		GameObject icon = GameObject.Find("BombIcon(Clone)");
        if (icon && !icon.transform.GetChild(0).gameObject.activeSelf)
        {
            icon.transform.GetChild(0).gameObject.SetActive(true);

			Instantiate(bomb, transform.position + transform.forward, transform.rotation);
		}
	}
}
