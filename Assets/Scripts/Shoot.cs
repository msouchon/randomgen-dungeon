using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

    private float timer = 0;

    public GameObject parentObj;
    // Start is called before the first frame update
    void Start()
    {
        this.ShootBullet();
    }
    private void ShootBullet(){
        GameObject a = Instantiate(bullet) as GameObject;
        a.transform.position = transform.position;
        a.transform.forward = transform.forward;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3){
            ShootBullet();
            timer %= 3;
        }
    }
}
