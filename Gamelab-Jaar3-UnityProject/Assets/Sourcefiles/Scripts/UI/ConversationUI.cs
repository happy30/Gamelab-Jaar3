//By Jordi

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

    public Animator conversationEffectsAnimator;

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
        if(port.portraitString != act + "_" + portrait || portrait != "None")
        {
            port.ChangePortrait(act, act + "_" + portrait);
        }
        
        actor.text = act;
        text.text = txt;
    }

    public void Strobe()
    {
        conversationEffectsAnimator.SetTrigger("Strobe");
    }

    public void Black()
    {
        conversationEffectsAnimator.SetBool("BlackScreen", true);
    }

    public void FadeOutBlack()
    {
        conversationEffectsAnimator.SetBool("BlackScreen", false);
    }

}
