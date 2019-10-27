using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalTutorial : MonoBehaviour
{
    public GameObject player;

    public string sceneName;
    public int killsRequired;
    public Material active;
    public Material inactive;
    public Text text;
    private MeshRenderer mr;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentInChildren<MeshRenderer>();
        mr.material = inactive;
        isActive = false;
        player = GameObject.Find("Player");
    }

    public enum state
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH,
        FIFTH
    }
    private state currentState = state.FIRST;

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject == player && isActive && currentState == state.FIRST)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal1();
            c.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.SECOND;
        }
        else if (c.gameObject == player && currentState == state.SECOND)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal2();
            c.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.THIRD;
        }
        else if (c.gameObject == player && currentState == state.THIRD && player.GetComponent<Spells>().playerSpells.Count >= 4)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal3();
            c.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.FOURTH;
        }
        else if (c.gameObject == player && currentState == state.FOURTH)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal4();
            c.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.FIFTH;
        }
        else if (c.gameObject == player && currentState == state.FIFTH && isActive)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currentState == state.FIRST)
        {
            if (Levels.killCount >= killsRequired)
            {
                isActive = true;
                mr.material = active;
                text.text = "Portal Active";
            }
            else
            {
                isActive = false;
                mr.material = inactive;
                text.text = Levels.killCount.ToString() + "/" + killsRequired.ToString();
            }
        }
        else if (currentState == state.SECOND)
        {
            if (Tutorial.currentState == Tutorial.state.DASH2)
            {
                isActive = true;
                mr.material = active;
                text.text = "Portal Active";
            }
            else
            {
                isActive = false;
                mr.material = inactive;
                text.text = "Mouse 2 to dash";
            }
        }
        else if (currentState == state.THIRD)
        {
            if (player.GetComponent<Spells>().playerSpells.Count >= 5)
            {
                isActive = true;
                mr.material = active;
                text.text = "Portal Active";
            }
            else
            {
                isActive = false;
                mr.material = inactive;
                text.text = "Get spells";
            }
        }
        else if (currentState == state.FOURTH)
        {
            isActive = true;
            mr.material = active;
            text.text = "Portal Active";
        }
        else if (currentState == state.FIFTH)
        {
            if (Levels.killCount >= 5)
            {
                isActive = true;
                mr.material = active;
                text.text = "Portal Active";
            }
            else
            {
                isActive = false;
                mr.material = inactive;
                text.text = Levels.killCount.ToString() + "/5";
            }
        }
    }
}
