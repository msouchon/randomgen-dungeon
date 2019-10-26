using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverUI;
    void Start()
    {
        gameOverUI.SetActive(false);
    }
    void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if(Levels.isDead){
            GameOver();
        }
    }
}
