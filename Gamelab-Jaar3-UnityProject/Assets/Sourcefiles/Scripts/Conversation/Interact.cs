//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConversationController))]
public class Interact : MonoBehaviour
{
    [Header("Fill These Sascha <3")]
    public string xmlName;
	public string interactionCodeName;
	public GameObject[] hideObjects;
	float timer;
    public bool DestroyAfterInteraction;

    [HideInInspector]
    public bool changeToUse;

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
                        GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;
                        GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
                        

                        foreach (GameObject obj in hideObjects)
                        {
                            obj.SetActive(false);
                        }
                    }
					
				}
				else
				{
                    if (changeToUse)
                    {
                        interactType = InteractType.Use;
                    }

                    activated = false;
					GameObject.Find("Canvas").GetComponent<ConversationUI>().DeactivateConversationUI();
					GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
					StartCoroutine(EnableCursor());

                    if(!DestroyAfterInteraction)
                    {
                        foreach (GameObject obj in hideObjects)
                        {
                            obj.SetActive(true);
                        }
                    }
                    else
                    {
                        GetComponent<BoxCollider>().enabled = false;
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
                    GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;
                    GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
                    

                    foreach (GameObject obj in hideObjects)
                    {
                        obj.SetActive(false);
                    }


                }
                else
                {
                    if(changeToUse)
                    {
                        interactType = InteractType.Use;
                    }

                    activated = false;
                    GameObject.Find("Canvas").GetComponent<ConversationUI>().DeactivateConversationUI();
                    GameObject.Find("Canvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
                    if(DestroyAfterInteraction)
                    {
                        PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
                    }
                    else
                    {
                        StartCoroutine(EnableCursor());
                    }
                    
                    //

                    foreach (GameObject obj in hideObjects)
                    {
                        obj.SetActive(true);
                    }


                }
                break;


            case InteractType.Use:
                GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;
                GetComponent<AnimationTrigger>().Activate();
				break;

		}


	}

	IEnumerator EnableCursor()
	{
		yield return new WaitForSeconds(0.1f);

        if(PlayMode.gameMode == PlayMode.GameMode.Conversation && PlayMode.gameMode != PlayMode.GameMode.Menu)
        {
            PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
        }

		
		yield break;

	}
}
