using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    public GameObject puzzleUI;
    public GameObject[] puzzles;

    public void Reset()
    {

    }

    public void Back()
    {
        GameObject.Find("SceneSettings").GetComponent<PuzzleRoomData>().EndPuzzle();
    }

    public void Complete()
    {
        foreach(GameObject puz in puzzles)
        {
            puz.SetActive(false);
        }
        GameObject.Find("SceneSettings").GetComponent<PuzzleRoomData>().EndPuzzle();
    }

}
