using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
     
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NextLevel()
    {
        SceneManager.LoadScene("TestScene");
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == player){
            NextLevel();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
