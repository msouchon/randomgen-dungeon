using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour, ISpell
{
    public GameObject bullet;
    public float bulletSpeed = 20;
    public float duration = 4.0f;

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
        GameObject icon = GameObject.Find("ShootIcon(Clone)");
        if (icon && !icon.transform.GetChild(0).gameObject.activeSelf)
        {
            icon.transform.GetChild(0).gameObject.SetActive(true);

            GameObject b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.GetComponent<BulletController>().direction = transform.forward;
            b.GetComponent<BulletController>().speed = bulletSpeed;
            Destroy(b, duration);
        }
    }

}
