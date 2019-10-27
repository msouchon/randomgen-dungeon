using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWander : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    public float bulletSpeed;

    public GameObject bullet;

    public int BULLETNUM1;

    public float BULLETDURATION;

    public Vector3 point1;
    public Vector3 point2;
    public Vector3 point3;
    public Vector3 point4;
    private Vector3 targetPoint;
    private float Timer = 0;

    private int Health;
    private int INITIALHEALTH;

    public float SHOOTTIME;
    private float difficulty;
    
    public float WALK_TIME;
    // state varies between 0-7
    public int state;

    public float bulletTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        state = 0;
        targetPoint = point1;
        transform.position = point4;
        bulletTimer = 0;
        INITIALHEALTH = 2000;
        Health = 200;
        difficulty = ((float) (INITIALHEALTH-Health)/(float) INITIALHEALTH)*5 + 1;
        Debug.Log(difficulty);
        Debug.Log("HELLO");
    }

    // Update is called once per frame
    
    void Shoot(){
        Debug.Log(difficulty);
        GameObject b;
        Vector3 bulletDirection;
        float angle;
        int bulletNum = ((int) difficulty)*4;
        for(int i=0; i<bulletNum; i++){
            angle = (i*2*Mathf.PI)/bulletNum;
            bulletDirection = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            b = Instantiate(bullet);
            b.transform.position = transform.position;
            b.transform.localScale = new Vector3(16f/bulletNum, 16f/bulletNum, 16f/bulletNum);
		    b.GetComponent<BossBulletController>().direction = bulletDirection;
		    b.GetComponent<BossBulletController>().speed = bulletSpeed*difficulty;
		    Destroy(b, BULLETDURATION);
            
        }
    }
    void Update()
    {        
        Timer += Time.deltaTime;
        bulletTimer += Time.deltaTime*difficulty;
        if (Timer > WALK_TIME){
            state += 1;
            state %= 8;
            Timer %= WALK_TIME;
        } 
        if (bulletTimer > SHOOTTIME){
            bulletTimer %= SHOOTTIME;
            Shoot();
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
            rb.MovePosition(Vector3.MoveTowards(transform.position, targetPoint, speed*Time.deltaTime));
        }
        else{
            Timer += Time.deltaTime; // doubles speed of timer counter
            Vector3 targetDir = targetPoint - transform.position;
            rb.MoveRotation(Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, speed*Time.deltaTime/2, 0.0f)));
        }
        

    }
}