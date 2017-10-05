using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConversationController))]
public class Interact : MonoBehaviour
{
    [Header("Fill These Sascha <3")]
    public string interactionCodeName;
    public GameObject hideObject;
    float timer;


	public enum InteractType
    {
        Examine,
        Conversation
    };

    public InteractType interactType;

    [HideInInspector]
    public bool activated;


    public void Trigger(bool on)
    {
        if(on)
        {
            activated = true;
            PlayMode.ChangeGameMode(PlayMode.GameMode.Conversation);
            GameObject.Find("Canvas").GetComponent<ConversationUI>().ActivateConversationUI();
            GetComponent<ConversationController>().ActivateConversation(interactionCodeName, interactType);
            hideObject.SetActive(false);
        }
        else
        {
            activated = false;
            GameObject.Find("Canvas").GetComponent<ConversationUI>().DeactivateConversationUI();
            StartCoroutine(EnableCursor());
            hideObject.SetActive(true);

        }
    }

    IEnumerator EnableCursor()
    {
        yield return new WaitForSeconds(0.5f);
        PlayMode.ChangeGameMode(PlayMode.GameMode.Explore);
        yield break;

    }
}
