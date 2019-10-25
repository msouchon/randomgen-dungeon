using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBallSummon : MonoBehaviour
{
    public GameObject lightningBall;
    public int flashes = 3;
    public float flashInterval = 1.0f;
    public float flashDuration = 0.2f;
    public Material flashMaterial;
    public string keyInput = "9";

    private MeshRenderer mr;
    private Material m;

    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown(keyInput))
        {
            StartCoroutine(Summon());
        }
    }

    IEnumerator Summon()
    {
        GameObject g = Instantiate(lightningBall, this.gameObject.transform);
        mr = g.GetComponent<MeshRenderer>();
        m = mr.material;
        for (int i = 0; i < flashes; i++)
        {
            yield return new WaitForSeconds(flashInterval);
            mr.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);
            mr.material = m;
        }
        Destroy(g);
    }
}
