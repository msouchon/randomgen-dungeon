using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour, ISpell
{
    public float dashForce = 100f;
    public float trailTime = 1f;

    private float currentTrailTime = 0;
    private TrailRenderer tr;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<TrailRenderer>();
        tr.emitting = false;
    }

    void Update()
    {
        if (currentTrailTime <= 0)
        {
            tr.emitting = false;
            GetComponent<Movement>().enableMovement = true;
        }
        else currentTrailTime -= Time.deltaTime;
    }

    public void doAction()
    {
        GameObject icon = GameObject.Find("DashIcon(Clone)");
        if (icon && !icon.transform.GetChild(0).gameObject.activeSelf)
        {
            icon.transform.GetChild(0).gameObject.SetActive(true);

            tr.emitting = true;
            currentTrailTime = trailTime;
            rb.velocity = Vector3.zero;
            rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
            GetComponent<Movement>().enableMovement = false;
        }
    }
}
