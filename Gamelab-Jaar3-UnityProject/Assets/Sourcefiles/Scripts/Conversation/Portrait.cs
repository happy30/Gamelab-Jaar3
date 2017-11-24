//By Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Portrait : MonoBehaviour
{
    Image portraitSprite;
    public string portraitString;
    Animator anim;

    public bool waiting;
    public string newActor;
    public string newPortrait;

    void Awake()
    {
        portraitSprite = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    public void ChangePortrait(string actor, string portrait)
    {
        

        string path = "Portraits/" + actor + "/" + portrait;


        Sprite port = Resources.Load<Sprite>(path);
        portraitSprite.sprite = port;
        portraitString = portrait;
        GetComponent<CanvasGroup>().alpha = 1;

    }

    public void AnimationPortraitRefreshTrigger()
    {
        ChangePortraitWaitForFadeOut();
    }

    public void ChangePortraitWaitForFadeOut()
    {
        if(waiting)
        {
                ChangePortrait(newActor, newActor + "_" + newPortrait);
                waiting = false;
        }
    }

    public void Refresh(bool left)
    {
        
        if(left)
        {
            anim.SetTrigger("MoveLeft");
        }
        else
        {
            anim.SetTrigger("MoveRight");
        }
    }
}
