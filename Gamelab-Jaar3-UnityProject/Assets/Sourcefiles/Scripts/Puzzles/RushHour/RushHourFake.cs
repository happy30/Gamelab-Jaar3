using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHourFake : MonoBehaviour
{
	public GameObject block;
	public Canvas myCanvas;

	public GameObject sceneBlock;
	public GameObject computer;

	bool completed;


	public void Drag()
	{
		Vector2 pos;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
		block.transform.position = new Vector3(myCanvas.transform.TransformPoint(pos).x, block.transform.position.y, block.transform.position.z);
	}

	void Update()
	{
		if(block.GetComponent<RectTransform>().anchoredPosition.x > 508 && !completed)
		{
			CompletePuzzle();
			completed = true;
		}
	}

	void CompletePuzzle()
	{
		GameObject.Find("SceneSettings").GetComponent<SceneEventData>().iconPuzzleCompleted = true;
		GameObject.Find("SceneSettings").GetComponent<SceneEventData>().interactions[1].Trigger(true);
		computer.GetComponent<Collider>().enabled = false;
		sceneBlock.SetActive(false);
		block.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
	}
	
}
