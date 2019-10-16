using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
	public int floorXVertices = 10;
	public int floorZVertices = 10;

	public Material floorMaterial;

	public void generateFloor(float xSize, float zSize) {
		if (GameObject.Find("Floor") != null) {
			Object.Destroy(GameObject.Find("Floor"));
		}
		GameObject floor = new GameObject();
		floor.name = "Floor";
		MeshFilter floorMesh = floor.AddComponent(typeof(MeshFilter)) as MeshFilter;
		floorMesh.mesh = createPlaneMesh(floorXVertices, floorZVertices);
		floor.AddComponent(typeof(MeshCollider));

		floor.transform.localScale = new Vector3((xSize-1)/floorXVertices, 1, (zSize-1)/floorZVertices);
		floor.transform.localPosition = new Vector3(-xSize/2, 0, -zSize/2);
		floor.AddComponent<MeshRenderer>();
		floor.GetComponent<Renderer>().material = floorMaterial;
	}

	// Creates a plane with a variable amount of vertices
	Mesh createPlaneMesh(int xVertices, int yVertices) {
		Mesh mesh = new Mesh();
		mesh.name = "FloorMesh";

		Vector3[] vertices = new Vector3[(xVertices + 1) * (yVertices + 1)];
		Vector2[] uvs = new Vector2[(xVertices + 1) * (yVertices + 1)];

		// Add all the vertices, and calc uvs
		int index = 0;
		for (float y = 0; y < yVertices + 1; y++) {
			for (float x = 0; x < xVertices + 1; x++) {
				vertices[index] = new Vector3(x, 0, y);
				uvs[index] = new Vector2(x / xVertices, y / yVertices);
				index += 1;
	  		}
		}
	
		// Add all the triangles iteratively
		index = 0;
		int[] triangles = new int[xVertices * yVertices * 6];
		for (int y = 0; y < yVertices; y++) {
			for (int x = 0; x < xVertices; x++) {
				triangles[index] = (xVertices + 1) * y + x;
				triangles[index + 1] = (xVertices + 1) * (y + 1) + x;
				triangles[index + 2] = (xVertices + 1) * y + x + 1;
				triangles[index + 3] = (xVertices + 1) * (y + 1) + x;
				triangles[index + 4] = (xVertices + 1) * (y + 1) + x + 1;
				triangles[index + 5] = (xVertices + 1) * y + x + 1;
				index += 6;
			}
		}

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}
}
