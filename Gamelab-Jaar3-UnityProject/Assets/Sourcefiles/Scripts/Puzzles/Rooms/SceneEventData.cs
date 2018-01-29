using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventData : MonoBehaviour
{
    public PlayMode.GameMode startMode;
    public PlayMode.GamePart gamePart;


	public bool rushHourCompleted;
	public bool iconPuzzleCompleted;
	public bool shapePuzzleCompleted;

	public int shapeCount;
    public bool drowsy;

	public GameObject[] puzzles;

	public Interact[] interactions;

    void Start()
    {

        //AudioPeer.bandBuffer = AudioPeer.freqBand;

        switch (startMode)
        {
            case PlayMode.GameMode.Conversation:
                PlayMode.gameMode = PlayMode.GameMode.Conversation;
                interactions[0].Trigger(true);
                break;
        }

        if(drowsy)
        {
            GameObject.Find("GameManager").GetComponent<ExploreStats>().forceCrouch = true;
        }
    }
}
