//Made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHour : MonoBehaviour
{
    public int gridWidth, gridHeight;
    
    
    public bool[,] grid = new bool[6,6];
    public Transform[] blocksArray;

	void Start () {
        for(int x = 0; x < blocksArray.Length -1; x ++)
        {
            for (int y = 0; y < blocksArray.Length - 1; y++)
                grid[(int)blocksArray[x].transform.position.x , (int)blocksArray[x].transform.position.y] = true;
        }
	}

    void Update()
    {
        Debug.DrawLine(new Vector3(0, 0, 0), new Vector3(6, 0, 0), Color.red);
        Debug.DrawLine(new Vector3(0, 6, 0), new Vector3(6, 6, 0), Color.red);
        Debug.DrawLine(new Vector3(6, 0, 0), new Vector3(6, 6, 0), Color.red);
        Debug.DrawLine(new Vector3(0, 6, 0), new Vector3(0, 0, 0), Color.red);
    }
}