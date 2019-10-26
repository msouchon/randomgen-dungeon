using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject player;

    public int KILLSREQUIRED;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("TestScene");
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player && isActive)
        {
            NextLevel();
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
