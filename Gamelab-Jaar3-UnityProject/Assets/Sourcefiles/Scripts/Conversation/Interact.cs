﻿//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConversationController))]
public class Interact : MonoBehaviour
{
	[Header("Fill These Sascha <3")]
	public string interactionCodeName;
	public GameObject[] hideObjects;
	float timer;


	public enum InteractType
	{
		Examine,
		Conversation,
		Use
			
	};

	public InteractType interactType;

	
	public bool activated;


	public void Trigger(bool on)
	{

		switch(interactType)
		{
            case InteractType.Conversation:

				if (on)
				{
                    if(!activated)
                    {
                        activated = true;
                        PlayMode.ChangeGameMode(PlayMode.GameMode.Conversation);
                        GameObject.Find("Canvas").GetComponent<ConversationUI>().ActivateConversationUI();
                        GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(false);
                        GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
                        GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;

                        foreach (GameObject obj in hideObjects)
                        {
                            obj.SetActive(false);
                        }
                    }
					

					
				}
				else
				{
                    
                    activated = false;
					GameObject.Find("Canvas").GetComponent<ConversationUI>().DeactivateConversationUI();
					GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
					StartCoroutine(EnableCursor());

					foreach (GameObject obj in hideObjects)
					{
						obj.SetActive(true);
					}

					
				}
				break;

            case InteractType.Examine:
                if (on)
                {
                    activated = true;
                    PlayMode.ChangeGameMode(PlayMode.GameMode.Conversation);
                    GameObject.Find("Canvas").GetComponent<ConversationUI>().ActivateConversationUI();
                    GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(false);
                    GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
                    GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;

                    foreach (GameObject obj in hideObjects)
                    {
                        obj.SetActive(false);
                    }


                }
                else
                {
                    
                    activated = false;
                    GameObject.Find("Canvas").GetComponent<ConversationUI>().DeactivateConversationUI();
                    GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
                    //StartCoroutine(EnableCursor());
                    PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);

                    foreach (GameObject obj in hideObjects)
                    {
                        obj.SetActive(true);
                    }


                }
                break;


            case InteractType.Use:
				GetComponent<AnimationTrigger>().Activate();
				break;

		}


	}

	IEnumerator EnableCursor()
	{
		yield return new WaitForSeconds(0.1f);

        if(PlayMode.gameMode == PlayMode.GameMode.Conversation)
        {
            PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
        }

		
		yield break;

	}
}
