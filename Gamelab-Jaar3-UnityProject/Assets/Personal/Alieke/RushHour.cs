//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public int gridWidth, gridHeight;
    
    public bool[,] grid;
    public Transform[] blocksArray;

	void Start () {
        grid = new bool[6, 6];
        grid[5, 5] = true;
        for(int x = 0; x < blocksArray.Length -1; x ++)
        {
            for (int y = 0; y < blocksArray.Length - 1; y++)
                grid[(int)blocksArray[x].transform.position.x , (int)blocksArray[x].transform.position.y] = true;
        }

        CheckArray();
	}

    void CheckArray()
    {
        foreach (bool value in grid)
        {
            print(value);
        }
    }
}