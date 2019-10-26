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
        SceneManager.LoadScene(sceneName);
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject == player && isActive){
            NextLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Levels.killCount >= killsRequired){
            isActive = true;
	    mr.material = active;
	    text.text = "Portal Active";
        }
	else {
		text.text = Levels.killCount.ToString() + "/" + killsRequired.ToString();
	}
    }
}
