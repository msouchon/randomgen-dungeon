using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Campaign()
    {
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    public void Survival()
    {
        SceneManager.LoadScene("SurvivalScene");
    }

    public void instructions()
    {
        SceneManager.LoadScene("TutorialScene");

    }
}
