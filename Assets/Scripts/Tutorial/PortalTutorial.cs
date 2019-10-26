using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTutorial : MonoBehaviour
{
    public GameObject player;

    public int KILLSREQUIRED;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    public enum state
    {
        FIRST,
        SECOND,
        THIRD,
        FOURTH
    }
    private state currentState = state.FIRST;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player && isActive && currentState == state.FIRST)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal1();
            col.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.SECOND;
        }
        else if (col.gameObject == player && currentState == state.SECOND)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal2();
            col.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.THIRD;
        }
        else if (col.gameObject == player && currentState == state.THIRD && player.GetComponent<Spells>().playerSpells.Count >= 4)
        {
            GameObject.Find("LevelController").GetComponent<Tutorial>().portal3();
            col.transform.position = new Vector3(-8, 1, 0);
            GameObject.Find("CaveGenerator").GetComponent<CaveGenerator>().generateCave(false);
            currentState = state.FOURTH;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Levels.killCount >= KILLSREQUIRED)
        {
            isActive = true;
        }
    }
}
