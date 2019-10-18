using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTowards : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;
    public GameObject player;

    public float EPS;
    public float MAX_DIST;
    public float MIN_DIST;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < MIN_DIST){
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, -speed*Time.deltaTime));
        }
        else if (distance < MAX_DIST && distance > MIN_DIST + EPS){
            rb.MovePosition(Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime));
        }
        transform.LookAt(player.transform);
    }


}
