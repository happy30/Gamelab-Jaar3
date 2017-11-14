using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoomData : MonoBehaviour
{

    public GameObject[] puzzles;
    public GameObject puzzleUI;

    public int currentID;

    public void StartPuzzle()
    {
        print("EnteringPuzzle");
        PlayMode.ChangeGameMode(PlayMode.GameMode.Puzzle);
        Cursor.lockState = CursorLockMode.None;
        puzzleUI.SetActive(true);
        puzzles[currentID].SetActive(true);
    
    }

    public void EndPuzzle()
    {
        puzzles[currentID].SetActive(false);
        puzzleUI.SetActive(false);
        PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
    }

}
