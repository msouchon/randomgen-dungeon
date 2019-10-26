using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Levels : MonoBehaviour
{
    // Start is called before the first frame update

    public static int killCount;
    public static bool isDead;
    void Start()
    {
        killCount = 0;
        isDead = false;
    }

    public void MainMenu1()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public static void IncrementKillCount(){
        killCount += 1;
        Debug.Log(killCount);
    }

    void Update()
    {
        
    }
}
