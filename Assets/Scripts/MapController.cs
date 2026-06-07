using System;
using System.Collections.Generic;
using System.Linq;
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

    public List<Vector2Int> wayPoints;



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
        wayPoints = null;
    }

    private void GenerateTileset()
    {
        Vector2Int tempPoint;

        wayPoints = new List<Vector2Int>();

        startPoint = new Vector2Int(UnityEngine.Random.Range(0, mapSize), 0);
        endPoint = new Vector2Int((mapSize - 1) - startPoint.x , (mapSize - 1) - startPoint.y);

        //for (int i = 0; i < Mathf.RoundToInt(mapSize / 5); i++)
        //{
        //    tempPoint = new Vector2Int(UnityEngine.Random.Range(0, mapSize), UnityEngine.Random.Range(0, mapSize));
        //
        //    if (!wayPoints.Contains(tempPoint))
        //    {
        //        wayPoints.Add(tempPoint);
        //        tileGeneration[tempPoint.x, tempPoint.y] = true;
        //    }
        //}

        tileGeneration[startPoint.x, startPoint.y] = true;
        tileGeneration[endPoint.x, endPoint.y] = true;

        FindPath(startPoint, endPoint);
    }

    private void FindPath(Vector2Int startPoint, Vector2Int endPoint)
    {
        Location current = null;
        var start = new Location { Position = startPoint };
        var end = new Location { Position = endPoint };
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;

        start.G = 0;
        start.H = ComputeHScore(start.Position.x, start.Position.y,endPoint.x,endPoint.y);
        start.F = start.G + start.H;

        current = start;
        
        openList.Add(start);

        while (openList.Count > 0)
        {
            Vector2Int tempVector;

            for (int i = -1; i < 2; i++)
            {                
                for (int j = -1; j < 2; j+=2)
                {
                    if (Mathf.Abs(i) == 1)
                    {
                        j = 0;
                    }
                    tempVector = new Vector2Int(startPoint.x + (i), startPoint.y + (j));
                    if (tempVector.x >= 0 && tempVector.y >= 0 && tempVector.x < mapSize && tempVector.y < mapSize)
                    {
                        openList.Add(TileDetection(tempVector, current));
                        //tileGeneration[tempVector.x, tempVector.y] = true;
                    }
                    
                }
            }

            break;
        }
    }
    private int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }

    private Location TileDetection(Vector2Int tempVector, Location current)
    {
        Location tempLocation = new Location();

        tempLocation.Position = tempVector;
        tempLocation.G = current.G + 1;
        tempLocation.H = ComputeHScore(tempLocation.Position.x, tempLocation.Position.y, endPoint.x, endPoint.y);
        tempLocation.F = tempLocation.G + tempLocation.H;

        return tempLocation;
    }

}
