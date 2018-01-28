using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneProgression : MonoBehaviour
{
	public int progression;
	public int startProgression;

	public AudioClip tenseMp3;

	public SceneObjects sceneObjects;

	public enum Scene
	{
		NotSpecified,
		_001_01_Story,
		_017_01_Story,
		_143_01_Story,
        _143_02_Story,
		_144_01_Story,
		_144_02_Escape,
	};

	public Scene scene;

	void Awake()
	{
		sceneObjects = GetComponent<SceneObjects>();
	}

	void Start()
	{
		progression = startProgression;
		ProgressionEffect (progression);
	}

	public void ProgressionEffect (int prog)
	{
		switch(scene)
		{
			case Scene._001_01_Story:

				switch (prog)
				{
					case 0:
					break;

				}

			break;


			case Scene._017_01_Story:
				switch (prog)
				{
					case 1:

						GameObject.Find("Player").GetComponent<ExploreController>().crouchAvailable = true;
						break;

					case 2:
						sceneObjects.sceneObjects[0].SetActive(false);
						sceneObjects.sceneObjects[1].SetActive(true);
						break;

					case 3:
						GameObject.Find("Coll2").GetComponent<Interact>().interactionCodeName = "017_ShapePuzzleCompleted2";
						break;

				}
				break;

			case Scene._143_01_Story:

				switch(prog)
				{
                    case 0:
                        PlayMode.gameMode = PlayMode.GameMode.Explore;
                        break;


					case 1:
						sceneObjects.sceneObjects[0].GetComponent<Animator>().SetBool("Activate", true);

					break;

                    case 2:
                        sceneObjects.ActivateObject(1);
                        sceneObjects.DeactivateObject(0);
                        break;

                    case 3:
                        sceneObjects.ActivateObject(2);
                        break;
				}
				break;

            case Scene._143_02_Story:
                
                switch(prog)
                {
                    case 1:
                        sceneObjects.sceneObjects[0].GetComponent<Animator>().SetBool("Activate", true);

                        break;

                    case 2:
                        sceneObjects.ActivateObject(1);
                        sceneObjects.DeactivateObject(0);
                        break;
                }
                break;


            case Scene._144_01_Story:

				switch (prog)
				{
					case 0:
						sceneObjects.ActivateObject(0);
						break;

				}
				break;

			case Scene._144_02_Escape:

				switch(prog)
				{
					case 3:
						sceneObjects.ActivateObject(2);
						break;

					case 4:
						GameObject.Find("GameManager").GetComponent<InventoryManager>().RemoveHeldItem(GameObject.Find("GameManager").GetComponent<InventoryManager>().inventory[0]);
						GameObject.Find("GameManager").GetComponent<InventoryManager>().inventory.Clear();
						GameObject.Find("Coll2").GetComponent<Interact>().interactionCodeName = "CC_ShapePuzzle3";
						break;

					case 5:
						//GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
						Camera.main.GetComponent<AudioSource>().clip = tenseMp3;
						GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
						break;

				}

			break;
		}
	}
}
