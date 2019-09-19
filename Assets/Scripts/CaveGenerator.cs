using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CaveGenerator : MonoBehaviour
{
	public int xSize;
	public int ySize;

	public string seed;
	public bool randomSeed;

	[Range(0, 1)] public float wallChance;
	public int iterations;
	public bool invert;
	public int vonNeumannN;
	[Range(0, 20)] public int wallThreshold;

	private int[,] heightmap;
	private System.Random random;

	void Start() {
		random = new System.Random();
		generateCave();
	}

	void Update() {
		if (Input.GetButtonDown("Fire1")) generateCave();
	}

	// Generates a cave using a cellular automaton
	void generateCave() {
		heightmap = new int[xSize, ySize];
		
		// Set sides as walls and do initial random fill
		for (int x = 0; x < xSize; x++) {
			for (int y = 0; y < ySize; y++) {
				// Edges are always walls
				if (x == 0 || y == 0 || x == xSize-1 || y == ySize-1)
					heightmap[x, y] = 1;
				else if (UnityEngine.Random.value > wallChance)
					heightmap[x, y] = 1;
				else
					heightmap[x, y] = 0;
			}
		}
		
		for (int i = 0; i < iterations; i++) {
			int x = random.Next(0, xSize);
			int y = random.Next(0, ySize);
			if (getVonNeumannNeighbourCount(x, y, vonNeumannN) > wallThreshold) {
				heightmap[x, y] = invert ? 0 : 1;				
			}
			else heightmap[x, y] = invert ? 1 : 0;
		}

		for (int i = 0; i < 1; i++) {
			for (int x = 0; x < xSize; x++) {
				for (int y = 0; y < ySize; y++) {
					if (getMooreNeighbourCount(x, y) > 4) {
						heightmap[x, y] = invert ? 0 : 1;
					}
					else heightmap[x, y] = invert ? 1: 0;
				}
			}
		}
	}

	// Gets the number of walls in the Moore neighbourhood
	// of a tile. This is the surrounding 8 cells.
	int getMooreNeighbourCount(int xTarget, int yTarget) {
		int count = 0;
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (
					xTarget + x > 0 && xTarget + x < xSize - 1 &&
					yTarget + y > 0 && yTarget + y < ySize - 1
				   )
					count += heightmap[xTarget + x, yTarget + y];
				else count ++;
			}
		}
		return count;
	}

	// Gets the number of walls in the Von Neumann Neighbourhood
	// defined by cells within a manhattan distance of n.
	int getVonNeumannNeighbourCount(int xTarget, int yTarget, int n) {
		int count = 0;
		for (int x = -n; x <= n; x++) {
			for (int y = -n; y <= n; y++) {
				if (Mathf.Abs(x + y) > n) continue;
				if (
					xTarget + x > 0 && xTarget + x < xSize - 1 &&
					yTarget + y > 0 && yTarget + y < ySize - 1
				   )
					count += heightmap[xTarget + x, yTarget + y];
				else count ++;
			}
		}
		return count;
	}
	
	// Testing for view of the cave
	void OnDrawGizmos() {
		if (heightmap != null) {
			for (int y = 0; y < ySize; y++) {
				for (int x = 0; x < xSize; x++) {
					Gizmos.color = (heightmap[x, y] == 1) ? Color.black : Color.white;
					Vector3 position = new Vector3(-xSize/2 + x + 0.5f, 0, -ySize/2 + y + 0.5f);
					Gizmos.DrawCube(position, Vector3.one);
				}
			}
		}
	}
}
