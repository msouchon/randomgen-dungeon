using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CaveGenerator : MonoBehaviour
{
    public GameObject portal;
    public int portalSpacing = 20;
    public int killsRequired = 10;

    public int xSize;
    public int ySize;
    public int borderSize = 1;

    public string seed;
    public bool randomSeed;

    [Range(0, 1)] public float wallChance;
    public int iterations;
    public bool invert;
    public int vonNeumannN;
    [Range(0, 20)] public int wallThreshold;

    private int[,] heightmap;
    private System.Random random;

    void Start()
    {
        random = new System.Random();
        generateCave(true);

    }

    void clearPosition(int xPos, int yPos, int width)
    {
        if (width % 2 != 0) width++;
        int a, b;

        for (int x = xPos - width / 2; x < xPos + width / 2; x++)
        {
            for (int y = yPos - width / 2; y < yPos + width / 2; y++)
            {
                a = xPos - x;
                b = yPos - y;
                if (a * a + b * b <= width / 2 * width / 2)
                {
                    heightmap[x, y] = 0;
                }
            }
        }
    }

    int[,] borderize(int borderSize, int[,] heightmap)
    {
        int[,] newHeightmap = new int[xSize + 2 * borderSize, ySize + 2 * borderSize];
        for (int x = 0; x < xSize + 2 * borderSize; x++)
        {
            for (int y = 0; y < ySize + 2 * borderSize; y++)
            {
                if (x < borderSize || x >= xSize + borderSize || y < borderSize || y >= ySize + borderSize)
                {
                    newHeightmap[x, y] = 1;
                }
                else
                {
                    newHeightmap[x, y] = heightmap[x - borderSize, y - borderSize];
                }
            }
        }
        return newHeightmap;
    }

    // Generates a cave using a cellular automaton
    public void generateCave(bool placePortal)
    {
        heightmap = new int[xSize, ySize];

        // Set sides as walls and do initial random fill
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                // Edges are always walls
                if (x == 0 || y == 0 || x == xSize - 1 || y == ySize - 1)
                    heightmap[x, y] = 0;
                else if (UnityEngine.Random.value > wallChance)
                    heightmap[x, y] = 1;
                else
                    heightmap[x, y] = 0;
            }
        }

        // Build an array of pairs
        Tuple<int, int>[] pairs = new Tuple<int, int>[xSize * ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                pairs[x * ySize + y] = new Tuple<int, int>(x, y);
            }
        }

        // Randomise the array
        for (int i = 0; i < pairs.Length; i++)
        {
            Tuple<int, int> tmp = pairs[i];
            int randomIndex = UnityEngine.Random.Range(i, pairs.Length);
            pairs[i] = pairs[randomIndex];
            pairs[randomIndex] = tmp;
        }


        for (int i = 0; i < pairs.Length; i++)
        {

            int x = pairs[i].Item1;
            int y = pairs[i].Item2;
            //int x = random.Next(0, xSize);
            //int y = random.Next(0, ySize);
            if (getVonNeumannNeighbourCount(x, y, vonNeumannN) > wallThreshold)
            {
                heightmap[x, y] = invert ? 0 : 1;
            }
            else heightmap[x, y] = invert ? 1 : 0;
        }

        for (int i = 0; i < 10; i++)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (getMooreNeighbourCount(x, y) > 4)
                    {
                        heightmap[x, y] = invert ? 0 : 1;
                    }
                    else heightmap[x, y] = invert ? 1 : 0;
                }
            }
        }
        MeshGenerator mG = GetComponent<MeshGenerator>();
        clearPosition(xSize / 2, ySize / 2, 40);

        if (placePortal)
        {
            int xPos, yPos;
            if (UnityEngine.Random.value > 0.5)
            {
                xPos = UnityEngine.Random.Range(portalSpacing * 2, xSize - portalSpacing * 2);
                yPos = (UnityEngine.Random.value > 0.5) ? portalSpacing * 2 : ySize - portalSpacing * 2;
            }
            else
            {
                yPos = UnityEngine.Random.Range(portalSpacing * 2, ySize - portalSpacing * 2);
                xPos = (UnityEngine.Random.value > 0.5) ? portalSpacing * 2 : xSize - portalSpacing * 2;
            }
            clearPosition(xPos, yPos, portalSpacing);
            GameObject g = Instantiate(portal);
            g.transform.position = new Vector3(xPos - xSize / 2, 1.4f, yPos - ySize / 2);
            if (g.GetComponent<PortalScript>())
            {
                g.GetComponent<PortalScript>().killsRequired = killsRequired;
            }
        }

        heightmap = borderize(borderSize, heightmap);
        mG.generateMesh(heightmap, 1);
    }

    // Gets the number of walls in the Moore neighbourhood
    // of a tile. This is the surrounding 8 cells.
    int getMooreNeighbourCount(int xTarget, int yTarget)
    {
        int count = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (
                    xTarget + x > 0 && xTarget + x < xSize - 1 &&
                    yTarget + y > 0 && yTarget + y < ySize - 1
                   )
                    count += heightmap[xTarget + x, yTarget + y];
                else count++;
            }
        }
        return count;
    }

    // Gets the number of walls in the Von Neumann Neighbourhood
    // defined by cells within a manhattan distance of n.
    int getVonNeumannNeighbourCount(int xTarget, int yTarget, int n)
    {
        int count = 0;
        for (int x = -n; x <= n; x++)
        {
            for (int y = -n; y <= n; y++)
            {
                if (Mathf.Abs(x + y) > n) continue;
                if (
                    xTarget + x > 0 && xTarget + x < xSize - 1 &&
                    yTarget + y > 0 && yTarget + y < ySize - 1
                   )
                    count += heightmap[xTarget + x, yTarget + y];
                else count++;
            }
        }
        return count;
    }
}
