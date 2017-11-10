using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjects : MonoBehaviour
{
	public GameObject[] sceneObjects;

	public void ActivateObject(int id)
	{
		sceneObjects[id].SetActive(true);
	}

	public void DeactivateObject(int id)
	{
		sceneObjects[id].SetActive(false);
	}

}
