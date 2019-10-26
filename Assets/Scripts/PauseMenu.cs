using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Pause Menu implementation assisted by youtube video https://www.youtube.com/watch?v=JivuXdrIHK0
    public static bool paused = false;
    public GameObject pauseMenuUI;
    void Start()
    {
        Resume();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Levels.isDead){
            if (paused){
                Resume();
            } else{
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        paused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }
}
