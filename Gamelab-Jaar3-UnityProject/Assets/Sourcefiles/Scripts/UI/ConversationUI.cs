﻿//By Jordi

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
    public GameObject actorBox;
    public GameObject ProgressArrowBox;
    public DialogueBoxColorChanger diaBoxCol;

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

    public void RefreshPortrait(bool left)
    {
        port.Refresh(left);
    }

    public void RefreshColor(Actors.Actor act)
    {
        diaBoxCol.ChangeColorOnActor(act);
    }

    /*
    public void DisplayConversationUI(string act, string txt, string camPos, string portrait)
    {
        if(port.portraitString != act + "_" + portrait || portrait != "None")
        {
            StartCoroutine(port.ChangePortrait(act, act + "_" + portrait));
        }
        
        actor.text = act;
        text.text = txt;
    }

    */

    public void DisplayConversationUI(string act, string txt)
    {
        if(act == "Mind")
        {
            actorBox.SetActive(false);
            text.gameObject.GetComponent<Outline>().enabled = true;
            actor.text = "";
            text.text = txt;
            
        }
        else
        {
            actorBox.SetActive(true);
            text.gameObject.GetComponent<Outline>().enabled = false;
            actor.text = act;
            text.text = txt;
            
        }
    }

    public void UpdatePortrait(string act, string portrait)
    {
        print(portrait);
        if (port.portraitString != act + "_" + portrait && portrait != "None")
        {
            port.ChangePortrait(act, act + "_" + portrait);
        }
        else
        {
            print("There is no sprite change");
        }
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