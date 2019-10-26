using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Image bar;
    public Text text;
    private float health, maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth pHeath = player.GetComponent<PlayerHealth>();
        maxHealth = pHeath.MaxHealth;
        health = pHeath.health;
        text.text = health.ToString() + "/" + maxHealth.ToString();

        float h = health/maxHealth * 300.0f;
        h = h/2.0f;

        bar.rectTransform.anchoredPosition = new Vector2(h, 35);
        bar.rectTransform.sizeDelta = new Vector2(h*2, 15);

        float hRatio = health/maxHealth;
        if(hRatio >= 0.5) {
            bar.color = Color.Lerp(Color.yellow, Color.green, hRatio*2 -1);
        } else {
            bar.color = Color.Lerp(Color.red, Color.yellow, hRatio*2);
        }
    }
}
