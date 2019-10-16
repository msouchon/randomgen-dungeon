using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;
    public Vector3 point4;

    // state varies between 0-7
    private int state;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance;
        if (state ==0){
             distance = Vector3.Distance(transform.position, point1);
            rb.MovePosition(Vector3.MoveTowards(transform.position, point1, speed));
            if (distance < 0.2){
                state += 1;
            }
        }
        else if (state ==1){
             distance = Vector3.Distance(transform.position, point2);

            rb.MovePosition(Vector3.MoveTowards(transform.position, point2, speed));
            if (distance < 0.2){
                state += 1;
            }
        }
        else if (state ==2){
             distance = Vector3.Distance(transform.position, point3);

            rb.MovePosition(Vector3.MoveTowards(transform.position, point3, speed));
            if (distance < 0.2){
                state += 1;
            }
        }
        else if (state ==3){
             distance = Vector3.Distance(transform.position, point4);

            rb.MovePosition(Vector3.MoveTowards(transform.position, point4, speed));
            if (distance < 0.2){
                state += 1;
            }
        }
        
        state %= 4;
    }
}
