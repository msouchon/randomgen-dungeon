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
    private Vector3 targetPoint;
    private float Timer = 0;
    
    public float WALK_TIME;
    // state varies between 0-7
    public int state;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        state = 0;
        targetPoint = point1;
        transform.position = point4;
    }

    // Update is called once per frame
    void Update()
    {        
        Timer += Time.deltaTime;
        if (Timer > WALK_TIME){
            state += 1;
            state %= 8;
            Timer %= WALK_TIME;
        } 


        switch (state)
        {
            case 0:
                targetPoint = point1;
                break;
            case 2:
                targetPoint = point2;
                break;
            case 4:
                targetPoint = point3;
                break;
            case 6:
                targetPoint = point4;
                break;

        }
        if (state % 2 == 1){
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPoint, speed));
        }
        else{
            Timer += Time.deltaTime; // doubles speed of timer counter
            Vector3 targetDir = targetPoint - transform.position;
            rb.MoveRotation(Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, speed/2, 0.0f)));
        }
        

        /*
        if (state ==0){
            rb.MovePosition(Vector3.MoveTowards(transform.position, point1, speed));
            if (transform.position == point1){
                state += 1;
            }
        }
        else if (state == 1){
            Vector3 targetDir = point2 - transform.position;
            rb.MoveRotation(Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, speed/2, 0.0f)));
            if (targetDir == transform.forward){
                
                transform.position = new Vector3(10f,10f,10f);
            }
        }



        else if (state ==2){
            rb.MovePosition(Vector3.MoveTowards(transform.position, point2, speed));
            if (transform.position == point2){
                state += 1;
            }
        }
        else if (state ==3){
            rb.MovePosition(Vector3.MoveTowards(transform.position, point3, speed));
        } */

    }
}
