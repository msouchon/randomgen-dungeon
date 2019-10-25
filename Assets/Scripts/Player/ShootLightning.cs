using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLightning : MonoBehaviour, ISpell
{
    public GameObject lightningBullet;
    public float lightningBulletSpeed = 10;
    public float lightningDuration = 3.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void doAction()
    {
        GameObject icon = GameObject.Find("ShootLightningIcon(Clone)");
        if (icon && !icon.transform.GetChild(0).gameObject.activeSelf)
        {
            icon.transform.GetChild(0).gameObject.SetActive(true);

            GameObject b = Instantiate(lightningBullet);
            b.transform.position = transform.position;
            b.GetComponent<LightningBulletController>().direction = transform.forward;
            b.GetComponent<LightningBulletController>().speed = lightningBulletSpeed;
            Destroy(b, lightningDuration);
        }
    }

}
