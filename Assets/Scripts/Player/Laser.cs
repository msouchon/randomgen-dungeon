using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, ISpell
{
    public GameObject laser;
    public GameObject laserEffect;
    public float laserDamage = 100f;
    public float laserDelay = 1.0f;
    public float laserWidth = 0.5f;
    public float laserDuration = 0.5f;
    public float maxDist = 100f;

    void Update()
    {

    }

    IEnumerator ShootLaser()
    {
        GameObject e = Instantiate(laserEffect);
        e.transform.position = transform.position + Vector3.down / 2;
        e.transform.SetParent(transform);
        Destroy(e, laserDelay);
        yield return new WaitForSeconds(laserDelay);
        GameObject g = Instantiate(laser);
        Destroy(g, laserDuration);
        g.transform.position = transform.position;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, laserWidth / 2, transform.forward, maxDist);
        RaycastHit finalHit = new RaycastHit();
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.layer == 8)
            {
                finalHit = hit;
                break;
            }
        }
        foreach (RaycastHit hit in hits)
        {
            if (hit.distance < finalHit.distance)
            {
                if (hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hit.collider.gameObject.GetComponent<Health>().ApplyDamage(laserDamage);
                }
            }
        }
        LineRenderer lr = g.GetComponent<LineRenderer>();
        lr.positionCount = 3;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.forward);
        lr.SetPosition(2, finalHit.point);
    }

    public void doAction()
    {
        GameObject icon = GameObject.Find("LaserIcon(Clone)");
        if (icon && !icon.transform.GetChild(0).gameObject.activeSelf)
        {
            icon.transform.GetChild(0).gameObject.SetActive(true);

            StartCoroutine(ShootLaser());
        }
    }
}
