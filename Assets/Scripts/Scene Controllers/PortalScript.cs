using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public string sceneName;
    public int killsRequired;
    public Material active;
    public Material inactive;
    public Text text;
    public bool boss = false;

    private MeshRenderer mr;

    private bool isActive;

    void Start()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        mr.material = inactive;
        isActive = false;
        player = GameObject.Find("Player");
    }
    public void NextLevel()
    {
        Levels.playerSpells = player.GetComponent<Spells>().playerSpells;
        Levels.depth++;
        Levels.bossScene = false;
        SceneManager.LoadScene(sceneName);
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject == player && isActive)
        {
            if (!boss)
            {
                NextLevel();
            }
            else
            {
                Levels.playerSpells = player.GetComponent<Spells>().playerSpells;
                Levels.depth++;
                Levels.bossDead = false;
                Levels.bossScene = true;
                SceneManager.LoadScene("BossScene");
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!Levels.bossScene)
        {
            if (Levels.killCount >= killsRequired)
            {
                isActive = true;
                mr.material = active;
                if (boss)
                {
                    text.text = "Portal Unstable";
                }
                else
                {
                    text.text = "Portal Active";
                }
            }
            else
            {
                text.text = Levels.killCount.ToString() + "/" + killsRequired.ToString();
            }
        }
        else
        {
            if (Levels.bossDead)
            {
                isActive = true;
                mr.material = active;
                text.text = "Portal Active";
            }
            else
            {
                text.text = "Kill The Boss";
            }
        }
    }
}