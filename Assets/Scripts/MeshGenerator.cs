using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public int wallSize = 10;
	public MeshFilter walls;
	public SquareGrid sGrid;
	List<Vector3> vertices;
	List<int> triangles;

	// Given a vertex index it matches to the triangles that contain that vertex index
	Dictionary<int,List<Triangle>> triangleDict = new Dictionary<int,List<Triangle>>();
	// Stores if we have checked that this vertex index has already been looked at to form a wall
	HashSet<int> checkedVertIndex = new HashSet<int>();
	// Stores a list of vertex indexes which correspond to walls
	List<List<int>> wallVertPaths = new List<List<int>>();

	public void generateMesh(int[,] map, float squareSize) {

		triangleDict.Clear();
		checkedVertIndex.Clear();
		wallVertPaths.Clear();

		sGrid = new SquareGrid(map, squareSize, wallSize);

		int gridX = sGrid.grid.GetLength(0);
		int gridY = sGrid.grid.GetLength(1);
		
		vertices = new List<Vector3>();
		triangles = new List<int>();

		for (int x = 0; x < gridX; x++) {
			for (int y = 0; y < gridY; y++) {
				buildMeshFromSquare(sGrid.grid[x, y]);
			}
		}

		Mesh mesh = new Mesh();
		GetComponent<MeshFilter>().mesh = mesh;

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		generateWallMesh();
	}

	void generateWallMesh() {
		buildWallVertPaths();

		Mesh wallMesh = new Mesh();
		List<int> wallTriangles = new List<int>();
		List<Vector3> wallVertices = new List<Vector3>();

		foreach (List<int> wallPath in wallVertPaths) {
			for (int i = 0; i < wallPath.Count - 1; i++) {
				int current = wallVertices.Count;
				wallVertices.Add(vertices[wallPath[i]]);
				wallVertices.Add(vertices[wallPath[i + 1]]);
				wallVertices.Add(vertices[wallPath[i]] - wallSize * Vector3.up);
				wallVertices.Add(vertices[wallPath[i + 1]] - wallSize * Vector3.up);

				wallTriangles.Add(current + 3);
				wallTriangles.Add(current);
				wallTriangles.Add(current + 1);
				wallTriangles.Add(current);
				wallTriangles.Add(current + 3);
				wallTriangles.Add(current + 2);
			}
		}
		wallMesh.vertices = wallVertices.ToArray();
		wallMesh.triangles = wallTriangles.ToArray();
		walls.mesh = wallMesh;
	}

	void buildMeshFromSquare(Square s) {
		// Add all vertices which have a true state to a list
		// for building the mesh
		List<Node> sNodes = new List<Node>();
		switch (s.cellType) {
			// Corners made up of one triangle
			case 0:
				break;
			case 1:
				sNodes.Add(s.leftCent); sNodes.Add(s.botCent); sNodes.Add(s.botLeft);
				break;
			case 2:
				sNodes.Add(s.botRight); sNodes.Add(s.botCent); sNodes.Add(s.rightCent);
				break;
			case 4:
				sNodes.Add(s.topRight); sNodes.Add(s.rightCent); sNodes.Add(s.topCent);
				break;
			case 8:
				sNodes.Add(s.topLeft); sNodes.Add(s.topCent); sNodes.Add(s.leftCent);
				break;
			// Flat sides made up of two triangles
			case 3:
				sNodes.Add(s.rightCent); sNodes.Add(s.botRight); sNodes.Add(s.botLeft); sNodes.Add(s.leftCent);
				break;
			case 6:
				sNodes.Add(s.topCent); sNodes.Add(s.topRight); sNodes.Add(s.botRight); sNodes.Add(s.botCent);
				break;
			case 9:
				sNodes.Add(s.topLeft); sNodes.Add(s.topCent); sNodes.Add(s.botCent); sNodes.Add(s.botLeft);
				break;
			case 12:
				sNodes.Add(s.topLeft); sNodes.Add(s.topRight); sNodes.Add(s.rightCent); sNodes.Add(s.leftCent);
				break;
			// Diagonals made up of four triangles
			case 5:
				sNodes.Add(s.topCent); sNodes.Add(s.topRight); sNodes.Add(s.rightCent);
				sNodes.Add(s.botCent); sNodes.Add(s.botLeft); sNodes.Add(s.leftCent);
				break;
			case 10:
				sNodes.Add(s.topLeft); sNodes.Add(s.topCent); sNodes.Add(s.rightCent);
				sNodes.Add(s.botRight); sNodes.Add(s.botCent); sNodes.Add(s.leftCent);
				break;
			// Corners made up of 3 triangles, inverse of the first set of cases
			case 7:
				sNodes.Add(s.topCent); sNodes.Add(s.topRight); sNodes.Add(s.botRight); sNodes.Add(s.botLeft); sNodes.Add(s.leftCent);
				break;
			case 11:
				sNodes.Add(s.topLeft); sNodes.Add(s.topCent); sNodes.Add(s.rightCent); sNodes.Add(s.botRight); sNodes.Add(s.botLeft);
				break;
			case 13:
				sNodes.Add(s.topLeft); sNodes.Add(s.topRight); sNodes.Add(s.rightCent); sNodes.Add(s.botCent); sNodes.Add(s.botLeft);
				break;
			case 14:
				sNodes.Add(s.topLeft); sNodes.Add(s.topRight); sNodes.Add(s.botRight); sNodes.Add(s.botCent); sNodes.Add(s.leftCent);
				break;
			// Filled square
			case 15:
				sNodes.Add(s.topLeft); sNodes.Add(s.topRight); sNodes.Add(s.botRight); sNodes.Add(s.botLeft);
				// As this is a filled square it will not be a part of the walls
				checkedVertIndex.Add(s.topLeft.vertIndex);
				checkedVertIndex.Add(s.topRight.vertIndex);
				checkedVertIndex.Add(s.botRight.vertIndex);
				checkedVertIndex.Add(s.botLeft.vertIndex);
				break;
		}
		// Give each vertex index to the triangles list if it doesnt already have it
		// and add it to the list of vertices
		for (int i = 0; i < sNodes.Count; i++) {
			if (sNodes[i].vertIndex < 0) {
				sNodes[i].vertIndex = vertices.Count;
				vertices.Add(sNodes[i].position);
			}
		}
		// Make the triangles
		if (sNodes.Count >= 3) {
			addTriangle(sNodes[0], sNodes[1], sNodes[2]);
		}
		if (sNodes.Count >= 4) {
			addTriangle(sNodes[0], sNodes[2], sNodes[3]);
		}
		if (sNodes.Count >= 5) {
			addTriangle(sNodes[0], sNodes[3], sNodes[4]);
		}
		if (sNodes.Count >= 6) {
			addTriangle(sNodes[0], sNodes[4], sNodes[5]);
		}

	}

	// Adds a triangle to the list of all triangles
	// and adds the triangle to the triangle dictionary for
	// later lookup
	void addTriangle(Node n1, Node n2, Node n3) {
		triangles.Add(n1.vertIndex);
		triangles.Add(n2.vertIndex);
		triangles.Add(n3.vertIndex);

		Triangle t = new Triangle(n1.vertIndex, n2.vertIndex, n3.vertIndex);

		foreach (int vertIndex in t.vertIndexes) {
			if (triangleDict.ContainsKey(vertIndex)) {
				triangleDict[vertIndex].Add(t);
			}
			else {
				List<Triangle> l = new List<Triangle>();
				l.Add(t);
				triangleDict.Add(vertIndex, l);
			}
		}
	}

	// Given two vertex indexes returns true if they only share one
	// triangle in common
	bool shareOneTriangle(int vertIndexA, int vertIndexB) {
		int numSharedTriangles = 0;
		List<Triangle> trianglesA = triangleDict[vertIndexA];
		foreach (Triangle t in trianglesA) {
			foreach (int vertIndex in t.vertIndexes) {
				if (vertIndexB == vertIndex) numSharedTriangles++;
				if (numSharedTriangles > 1) return false;
			}
		}
		return true;
	}

	// Given a vertex index looks for a another vertex index which is on the
	// same wall
	int getConnectWallVertex(int vertIndex) {
		List<Triangle> triangles = triangleDict[vertIndex];

		foreach (Triangle t in triangles) {
			foreach (int vertIndexT in t.vertIndexes) {
				if (vertIndexT != vertIndex) {
					if (!checkedVertIndex.Contains(vertIndexT)) {
						if (shareOneTriangle(vertIndexT, vertIndex))
							return vertIndexT;
					}
				}
			}
		}
		return -1;
	}

	// Given a vertex thats part of a wall keep finding the rest of
	// the wall and add it to the wall path specified by the wall index
	void buildWallVertPath(int vertIndex, int wallIndex) {
		wallVertPaths[wallIndex].Add(vertIndex);
		checkedVertIndex.Add(vertIndex);
		int nextWallVertIndex = getConnectWallVertex(vertIndex);
		if (nextWallVertIndex > -1) buildWallVertPath(nextWallVertIndex, wallIndex);
	}

	// For each vertex check if it should be a wall then build a wall
	void buildWallVertPaths() {
		for (int vertIndex = 0; vertIndex < vertices.Count; vertIndex++) {
			if (!checkedVertIndex.Contains(vertIndex)) {
				int connectedWallVertex = getConnectWallVertex(vertIndex);
				if (connectedWallVertex > -1) {
					checkedVertIndex.Add(connectedWallVertex);
					List<int> wall = new List<int>();
					wall.Add(vertIndex);
					wallVertPaths.Add(wall);
					buildWallVertPath(connectedWallVertex, wallVertPaths.Count-1);
					// Add original vertex to go all the way round to the start again
					wallVertPaths[wallVertPaths.Count-1].Add(vertIndex);
				}
			}
		}
	}

	public class SquareGrid {
		public Square[,] grid;

		public SquareGrid(int[,] map, float squareSize, int wallSize) {
			int mapX = map.GetLength(0);
			int mapZ = map.GetLength(1);

			// Build all the corner nodes
			CornerNode[,] cornerNodes = new CornerNode[mapX, mapZ];
			for (int x = 0; x < mapX; x++) {
				for (int z = 0; z < mapZ; z++) {
					float xPos = -(mapX * squareSize) / 2 + x * squareSize;
					float zPos = -(mapZ * squareSize) / 2 + z * squareSize;
					// Set the state of the corner node to be true if there is a wall on the map
					cornerNodes[x, z] = new CornerNode(new Vector3(xPos, wallSize, zPos), map[x, z] == 1, squareSize);
				}
			}
			// Build the squares onto the grid
			// The number of squares is 1 less in both directions
			grid = new Square[mapX-1, mapZ-1];
			// Note that this grid is iterating through, starting from the bottom left
			for (int x = 0; x < mapX-1; x++) {
				for (int z = 0; z < mapZ-1; z++) {
					grid[x, z] = new Square(cornerNodes[x, z+1],
							cornerNodes[x+1, z+1],
							cornerNodes[x+1, z],
							cornerNodes[x, z]);
				}
			}
		}
	}

	public class Triangle {
		public int[] vertIndexes;

		public Triangle(int vertIndex1, int vertIndex2, int vertIndex3) {
			vertIndexes = new int[3];
			vertIndexes[0] = vertIndex1;
			vertIndexes[1] = vertIndex2;
			vertIndexes[2] = vertIndex3;
		}
	}

	// Represents a square in the marching square algorithm,
	// contains four corners and four midpoints. The cell type is essentially a 4 bit
	// number with the bottom left being the least significant bit and the top left being
	// the most significant bit increasing in an anticlockwise direction.
	// Like so:
	//
	// 8 - - - 4
	// |       |
	// |       |
	// 1 - - - 2
	//
	public class Square {
		public CornerNode topLeft, topRight, botLeft, botRight;
		public Node topCent, leftCent, rightCent, botCent;
		public int cellType;

		public Square(CornerNode topLeft, CornerNode topRight, CornerNode botRight, CornerNode botLeft) {
			this.topLeft = topLeft;
			this.topRight = topRight;
			this.botLeft = botLeft;
			this.botRight = botRight;

			this.topCent = topLeft.posX;
			this.leftCent = botLeft.posZ;
			this.rightCent = botRight.posZ;
			this.botCent = botLeft.posX;

			cellType = 0;
			if (topLeft.state) cellType |= 8;
			if (topRight.state) cellType |= 4;
			if (botRight.state) cellType |= 2;
			if (botLeft.state) cellType |= 1;
		}

	}

	// A node representing the midpoint between two corner nodes in
	// a square in the marching square algorithm
	public class Node {
		public Vector3 position;
		public int vertIndex = -1;
		
		public Node(Vector3 position) {
			this.position = position;
		}
	}

	// A node representing the corner of a square in the marching squares
	// algorithm. Each corner node is responsible for the mid point nodes in
	// the positive X direction and positive Z direction.
	public class CornerNode : Node {
		public bool state;
		public Node posX, posZ;
		
		public CornerNode(Vector3 position, bool state, float squareSize) : base(position) {
			this.state = state;

			posX = new Node(squareSize/2 * Vector3.right + position);
			posZ = new Node(squareSize/2 * Vector3.forward + position);
		}
	}
}
