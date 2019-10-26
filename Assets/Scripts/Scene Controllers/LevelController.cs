using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    void Update()
    {
        
    }
}
