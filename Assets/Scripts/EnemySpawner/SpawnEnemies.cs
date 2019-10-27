using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
	public GameObject cave;
	public float distanceToSpawn;
	public float xSpawnSize;
	public float zSpawnSize;
	public float xSize;
	public float zSize;
	public float cooldown;
	public int maxToSpawn;
	public List<GameObject> enemies;
	public List<float> chanceToSpawn;

	private int currSpawned = 0;
	private float currCooldown = 0;
	void Update() {
		if ((currSpawned - Levels.killCount) < maxToSpawn && currCooldown < 0) {
			for (int i = 0; i < enemies.Count; i++) {
				if (Random.value < chanceToSpawn[i]) {
					GameObject go = Instantiate(enemies[i]);
					float targetX = Random.Range(xSize - xSpawnSize, xSpawnSize);
					float targetZ = Random.Range(zSize - zSpawnSize, zSpawnSize);
					while (cave.GetComponent<CaveGenerator>().heightmap[(int) targetX, (int) targetZ] == 1) {
						targetX = Random.Range(xSize - xSpawnSize, xSpawnSize);
						targetZ = Random.Range(zSize - zSpawnSize, zSpawnSize);	
					}
					//Vector3 targetX = Vector3.right * (Random.value - 0.5f) * distanceToSpawn;
					//Vector3 targetZ = Vector3.forward * (Random.value - 0.5f) * distanceToSpawn;
					go.transform.position = new Vector3(targetX - xSize/2, transform.position.y, targetZ - zSize/2);
					//go.transform.rotation = Quaternion.Euler(Random.value * 360, 0, 0);
					currCooldown = cooldown;
					currSpawned++;
				}
			}
		}
		currCooldown -= Time.deltaTime;
	}
}
