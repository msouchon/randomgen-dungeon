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
    
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
	mr.material = inactive;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(sceneName);
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
