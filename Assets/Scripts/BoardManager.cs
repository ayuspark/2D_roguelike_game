﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int Min;
        public int Max;

        public Count(int min, int max)
        {
            Min = min;
            Max = min;
        }
    }

    public int colums = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] WallTiles;
    public GameObject[] OuterWallTiles;
    public GameObject[] FoodTiles;
    public GameObject[] EnemyTiles;

    private Transform _boardholder;
    private List<Vector3> _gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        _gridPositions.Clear();
        for (int x = 0; x < colums - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                _gridPositions.Add(new Vector3(x, y, 0.0f));
            }
        }
    }

    void BoardSetup()
    {
        _boardholder = new GameObject("board").transform;
        for (int x = -1; x < colums + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = FloorTiles[Random.Range(0, FloorTiles.Length)];
                if (x == -1 || x == colums || y == -1 || y == rows)
                {
                    toInstantiate = OuterWallTiles[Random.Range(0, OuterWallTiles.Length)];
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(_boardholder);
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randIndex = Random.Range(0, _gridPositions.Count);
        Vector3 result_randPosition = _gridPositions[randIndex];
        _gridPositions.RemoveAt(randIndex);

        return result_randPosition;
    }

    void LayoutObjAtRandom(GameObject[] tileArr, int min, int max)
    {
        int objCount = Random.Range(min, max + 1);
        for (int i = 0; i < objCount; i++)
        {
            Vector3 randPosition = RandomPosition();
            GameObject tile = tileArr[Random.Range(0, tileArr.Length)];
            Instantiate(tile, randPosition, Quaternion.identity);
        }

    }

    public void SetupScenes(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjAtRandom(WallTiles, wallCount.Min, wallCount.Max);
        LayoutObjAtRandom(FoodTiles, foodCount.Min, foodCount.Max);

        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjAtRandom(EnemyTiles, enemyCount, enemyCount);

        Instantiate(Exit, new Vector3(colums - 1, rows - 1, 0.0f), Quaternion.identity);
    }

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}
}