using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
	public float distanceToSpawn;
	public float cooldown;
	public int maxToSpawn;
	public List<GameObject> enemies;
	public List<float> chanceToSpawn;

	private int currSpawned = 0;
	private float currCooldown = 0;
	void Update() {
		if (currSpawned < maxToSpawn && currCooldown < 0) {
			for (int i = 0; i < enemies.Count; i++) {
				if (Random.value < chanceToSpawn[i]) {
					GameObject go = Instantiate(enemies[i]);
					Vector3 targetX = Vector3.right * (Random.value - 0.5f) * distanceToSpawn;
					Vector3 targetZ = Vector3.forward * (Random.value - 0.5f) * distanceToSpawn;
					go.transform.position = transform.position + targetX + targetZ;
					//go.transform.rotation = Quaternion.Euler(Random.value * 360, 0, 0);
					currCooldown = cooldown;
					currSpawned++;
				}
			}
		}
		currCooldown -= Time.deltaTime;
	}
}
