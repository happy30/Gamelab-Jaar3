//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConversationController))]
public class Interact : MonoBehaviour
{
    public enum InteractType
    {
        Examine,
        Conversation,
        Use

    };
    [Header("Fill These Sascha <3")]
    public InteractType interactType;
    public string xmlName;
	public string interactionCodeName;
	
	float timer;


    public bool DestroyAfterInteraction;

    [HideInInspector]
    public bool changeToUse;

    public Renderer highlightRenderer;

    float opacity;

    public bool activated;

    public GameObject[] hideObjects;


    void Start()
    {
        opacity = 0.2f;
        if(GetComponent<Renderer>() != null)
        {
            highlightRenderer = GetComponent<Renderer>();
        }
    }

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
                        GameObject.Find("HUDCanvas").GetComponent<ConversationUI>().ActivateConversationUI();
                        GameObject.Find("HUDCanvas").GetComponent<ExploreUI>().ShowInteractCursor(false);
                        GameObject.Find("GameManager").GetComponent<ConversationStats>().interactedObject = this;
                        GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
                        

                        foreach (GameObject obj in hideObjects)
                        {

                            StartCoroutine(FadePortrait(false, obj.GetComponent<Renderer>()));
                            //obj.SetActive(false);
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
					GameObject.Find("HUDCanvas").GetComponent<ConversationUI>().DeactivateConversationUI();
					GameObject.Find("HUDCanvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
					StartCoroutine(EnableCursor());

                    if(!DestroyAfterInteraction)
                    {
                        foreach (GameObject obj in hideObjects)
                        {
                            StartCoroutine(FadePortrait(true, obj.GetComponent<Renderer>()));
                            //obj.SetActive(true);
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
                    GameObject.Find("HUDCanvas").GetComponent<ConversationUI>().ActivateConversationUI();
                    GameObject.Find("HUDCanvas").GetComponent<ExploreUI>().ShowInteractCursor(false);
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
                    GameObject.Find("HUDCanvas").GetComponent<ConversationUI>().DeactivateConversationUI();
                    GameObject.Find("HUDCanvas").GetComponent<ExploreUI>().ShowInteractCursor(true);
                    if(DestroyAfterInteraction)
                    {
                        PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
                        GetComponent<BoxCollider>().enabled = false;
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

    IEnumerator FadePortrait(bool fadeIn, Renderer hideObject)
    {
        

        if(!fadeIn)
        {
            while(opacity > 0)
            {
                opacity -= Time.deltaTime;
                hideObject.material.SetFloat("_Opacity", opacity);
                yield return new WaitForEndOfFrame();
            }
            hideObject.material.SetFloat("_Opacity", 0);
            yield break;
        }
        else
        {
            while (opacity < 0.2f)
            {
                opacity += Time.deltaTime;
                hideObject.material.SetFloat("_Opacity", opacity);
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }
    }
}
