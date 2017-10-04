using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour
{
    public Portrait port;
    public Text text;
    public Text actor;

    public GameObject conversationCanvas;

    public void ActivateConversationUI()
    {
        conversationCanvas.SetActive(true);
        conversationCanvas.GetComponent<Animator>().SetBool("Activated", true);
    }

    public void DeactivateConversationUI()
    {
        conversationCanvas.SetActive(false);

    }

    public void DisplayConversationUI(string act, string txt, string camPos, string portrait)
    {
        if(port.portraitString != act + "_" + portrait)
        {
            port.ChangePortrait(act + "_" + portrait);
        }
        
        actor.text = act;
        text.text = txt;
    }

    

}
