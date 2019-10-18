using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    private int TimeLeft = 3;
    private float time = 0.0f;
    private float interpolationPeriod = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
 
        if (time >= interpolationPeriod) {
            time = time - interpolationPeriod;
            TimeLeft -= 1;
            if(TimeLeft == 0) {
                TimeLeft = 3;
                this.transform.parent.gameObject.SetActive(false);
            }
            GetComponent<Text>().text = TimeLeft.ToString();
            // execute block of code here
        }
    }
}
