using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public string sceneName;
    public int killsRequired;
    public Material active;
    public Material inactive;
    
    private MeshRenderer mr;
    
    private bool isActive;
    
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
	      mr.material = inactive;
        isActive = false;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == player && isActive){
            NextLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Levels.killCount > killsRequired){
            isActive = true;
        }
    }
}
