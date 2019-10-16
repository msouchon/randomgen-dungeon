using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	public SquareGrid sGrid;
	List<Vector3> vertices;
	List<int> triangles;

	public void generateMesh(int[,] map, float squareSize) {
		sGrid = new SquareGrid(map, squareSize);

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
			triangles.Add(sNodes[0].vertIndex); triangles.Add(sNodes[1].vertIndex); triangles.Add(sNodes[2].vertIndex);
		}
		if (sNodes.Count >= 4) {
			triangles.Add(sNodes[0].vertIndex); triangles.Add(sNodes[2].vertIndex); triangles.Add(sNodes[3].vertIndex);
		}
		if (sNodes.Count >= 5) {
			triangles.Add(sNodes[0].vertIndex); triangles.Add(sNodes[3].vertIndex); triangles.Add(sNodes[4].vertIndex);
		}
		if (sNodes.Count >= 6) {
			triangles.Add(sNodes[0].vertIndex); triangles.Add(sNodes[4].vertIndex); triangles.Add(sNodes[5].vertIndex);
		}

	}

	public class SquareGrid {
		public Square[,] grid;

		public SquareGrid(int[,] map, float squareSize) {
			int mapX = map.GetLength(0);
			int mapZ = map.GetLength(1);

			// Build all the corner nodes
			CornerNode[,] cornerNodes = new CornerNode[mapX, mapZ];
			for (int x = 0; x < mapX; x++) {
				for (int z = 0; z < mapZ; z++) {
					float xPos = -(mapX * squareSize) / 2 + x * squareSize;
					float zPos = -(mapZ * squareSize) / 2 + z * squareSize;
					// Set the state of the corner node to be true if there is a wall on the map
					cornerNodes[x, z] = new CornerNode(new Vector3(xPos, 0, zPos), map[x, z] == 1, squareSize);
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
