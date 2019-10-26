using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    public float cooldown = 3;
    private float TimeLeft = 3;
    private float time = 0.0f;
    private float interpolationPeriod = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = cooldown;
        GetComponent<Text>().text = TimeLeft.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;
            TimeLeft -= interpolationPeriod;

            if (TimeLeft > 1)
            {
                GetComponent<Text>().text = ((int)TimeLeft + 1).ToString();
            }
            else
            {
                if (TimeLeft < 0.1f)
                {
                    TimeLeft = cooldown;
                    GetComponent<Text>().text = TimeLeft.ToString();
                    this.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    GetComponent<Text>().text = TimeLeft.ToString();
                }
            }
        }
    }
}
