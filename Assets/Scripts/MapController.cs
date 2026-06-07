using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject pathPrefab, placablePrefab;

    [Range(3,100)]
    public int mapSize = 5;

    private int currentMapSize = 5;

    public GameObject[,] tileSet;
    public bool[,] tileGeneration;

    public Vector2Int startPoint;
    public Vector2Int endPoint;
    void Start()
    {        
        GenerateMap();
        currentMapSize = mapSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMapSize != mapSize)
        {
            GenerateMap();
            currentMapSize = mapSize;
        }
    }

    private void GenerateMap()
    {
        if (tileSet != null)
        {
            CleanupMap();
        }

        tileSet = new GameObject[mapSize, mapSize];
        tileGeneration = new bool[mapSize, mapSize];

        GenerateTileset();

        GameObject usedPrefab = pathPrefab;

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {

                switch (tileGeneration[j,i])
                {
                    case true:
                        usedPrefab = pathPrefab; break;
                    case false:
                        usedPrefab = placablePrefab; break;
                }

                tileSet[i, j] = Instantiate(usedPrefab, new Vector3(j * -5, 0, i * -5),Quaternion.identity,this.transform);
            }
        }
    }

    private void CleanupMap()
    {
        for (int i = 0; i < tileSet.GetLength(0); i++)
        {
            for (int j = 0; j < tileSet.GetLength(1); j++)
            {
                Destroy(tileSet[i, j]);
            }
        }

        tileSet = null;
        tileGeneration = null;
    }

    private void GenerateTileset()
    {
        startPoint = new Vector2Int(UnityEngine.Random.Range(0, mapSize), 0);
        //endPoint = new Vector2Int((mapSize - 1) - startPoint.x , (mapSize - 1) - startPoint.y);

        

        tileGeneration[startPoint.x, startPoint.y] = true;
        //tileGeneration[endPoint.x, endPoint.y] = true;
    }
}
