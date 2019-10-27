using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update

    public enum state
    {
        WELCOME,
        SHOOT,
        SHOOT2,
        PORTAL,
        DASH,
        DASH2,
        SPELLS,
        ENEMIES,
        PORTAL2
    }

    public static state currentState = state.WELCOME;

    private string welcome = "Welcome to Dungeon Wizard!\n\nUse WASD to move. Try moving around now!";
    private string shoot = "Press Mouse 1 to shoot, aim with your mouse pointer.\n\nTry aiming at the green enemy and shooting it!";
    private string shoot2 = "Now shoot it again to kill it!";
    private string portal = "Good job!\n\n You have now killed enough enemies to open the portal!\nMove into the portal to continue.";
    private string dash = "Use Mouse 2 to dash, aim with your mouse pointer.\n\nTry dashing now.";
    private string dash2 = "Dashing can be used for movement and to push enemies and certain spells around.\nWatch out for cases where you could use this.\n\nEnter the portal to continue.";
    private string spells = "Killing enemies will cause certain spells to be dropped.\nWhen you pick up a spell it will be assigned to a key 1 through 5.\n\nTry killing some enemies and pick up the spells.";
    private string spells2 = "Press key 1-5 to use these new skills\n\nNow go through the portal to continue.";
    private string enemies = "Some enemies may try and kill you!\nAvoid explosions and bullets and watch out for your health. Some may even try and ambush you through walls.\nTry using your new spells on these enemies!\nEnter portal to continue.";
    private string portalText = "In the world portals will be hidden and require exploration to find.\nThey will also require a certain kill count to activate.\n\nTry getting kill count and enter the portal to finish the tutorial!";

    public Text Text;
    public GameObject stillEnemy, shootingEnemy, explodingEnemy;

    private GameObject stillEnemySpawned;
    private bool spawned;
    List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.WELCOME)
        {
            Text.text = welcome;
            if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
            {
                currentState = state.SHOOT;
            }
        }

        if (currentState == state.SHOOT)
        {
            Text.text = shoot;
            if (!stillEnemySpawned && !spawned)
            {
                stillEnemySpawned = Instantiate(stillEnemy, new Vector3(8, 1, 0), Quaternion.identity);
                spawned = true;
            }

            if (stillEnemySpawned.GetComponent<Health>().currentHealth < 50 && stillEnemySpawned.GetComponent<Health>().currentHealth > 0)
            {
                currentState = state.SHOOT2;
            }

        }

        if (currentState == state.SHOOT2)
        {
            Text.text = shoot2;
            if (!stillEnemySpawned && spawned)
            {
                currentState = state.PORTAL;
            }
        }

        if (currentState == state.PORTAL)
        {
            Text.text = portal;
        }

        if (currentState == state.DASH)
        {
            Text.text = dash;
            if (Input.GetMouseButton(1))
            {
                currentState = state.DASH2;
                spawned = false;
            }
        }

        if (currentState == state.DASH2)
        {
            Text.text = dash2;
            if (!spawned)
            {
                Instantiate(stillEnemy, new Vector3(9, 1, 0), Quaternion.identity);
                Instantiate(stillEnemy, new Vector3(8, 1, 3), Quaternion.identity);
                Instantiate(stillEnemy, new Vector3(8, 1, -3), Quaternion.identity);
                Instantiate(stillEnemy, new Vector3(4, 1, 2), Quaternion.identity);
                Instantiate(stillEnemy, new Vector3(6, 1, 6), Quaternion.identity);
                Instantiate(stillEnemy, new Vector3(3, 1, -5), Quaternion.identity);
                spawned = true;
            }
        }

        if (currentState == state.SPELLS)
        {
            if (!spawned)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Bullet");
                foreach (GameObject obj in objs)
                {
                    Destroy(obj);
                }
                objs = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject obj in objs)
                {
                    Destroy(obj);
                }

                GameObject g = Instantiate(stillEnemy, new Vector3(8, 1, 6), Quaternion.identity);
                g.GetComponent<Health>().tutorialSpell = Spells.SpellsEnum.SHOOTLIGHTNING;
                g = Instantiate(stillEnemy, new Vector3(8, 1, 3), Quaternion.identity);
                g.GetComponent<Health>().tutorialSpell = Spells.SpellsEnum.LIGHTNINGBALL;
                g = Instantiate(stillEnemy, new Vector3(8, 1, -0), Quaternion.identity);
                g.GetComponent<Health>().tutorialSpell = Spells.SpellsEnum.INVISIBILITY;
                g = Instantiate(stillEnemy, new Vector3(8, 1, -3), Quaternion.identity);
                g.GetComponent<Health>().tutorialSpell = Spells.SpellsEnum.BOMB;
                g = Instantiate(stillEnemy, new Vector3(8, 1, -6), Quaternion.identity);
                g.GetComponent<Health>().tutorialSpell = Spells.SpellsEnum.LASER;
                spawned = true;
            }

            GameObject player = GameObject.Find("Player");
            if (player.GetComponent<Spells>().playerSpells.Count >= 5)
            {
                Text.text = spells2;
            }
            else
            {
                Text.text = spells;
            }
        }

        if (currentState == state.ENEMIES)
        {
            Text.text = enemies;
            if (!spawned)
            {
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, 6), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, 2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, -2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, -6), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(shootingEnemy, new Vector3(12, 1, -2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(explodingEnemy, new Vector3(12, 1, 2), Quaternion.identity));
                spawned = true;
            }
            int c = 0;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (!spawnedEnemies[i])
                {
                    c++;
                }
            }
            if (c == spawnedEnemies.Count)
            {
                spawnedEnemies = new List<GameObject>();
                spawned = false;
            }
        }

        if (currentState == state.PORTAL2)
        {
            Text.text = portalText;
            if (!spawned)
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Bullet");
                foreach (GameObject obj in objs)
                {
                    Destroy(obj);
                }
                objs = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject obj in objs)
                {
                    Destroy(obj);
                }
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, 6), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, 2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, -2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(stillEnemy, new Vector3(8, 1, -6), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(shootingEnemy, new Vector3(14, 1, -2), Quaternion.identity));
                spawnedEnemies.Add(Instantiate(explodingEnemy, new Vector3(14, 1, 2), Quaternion.identity));
                spawned = true;
            }
            int c = 0;
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                if (!spawnedEnemies[i])
                {
                    c++;
                }
            }
            if (c == spawnedEnemies.Count)
            {
                spawnedEnemies = new List<GameObject>();
                spawned = false;
            }
        }
    }

    public void portal1()
    {
        currentState = state.DASH;
    }

    public void portal2()
    {
        currentState = state.SPELLS;
        spawned = false;
    }

    public void portal3()
    {
        currentState = state.ENEMIES;
        spawned = false;
    }

    public void portal4()
    {
        currentState = state.PORTAL2;
        spawned = false;
        Levels.killCount = 0;
    }
}
