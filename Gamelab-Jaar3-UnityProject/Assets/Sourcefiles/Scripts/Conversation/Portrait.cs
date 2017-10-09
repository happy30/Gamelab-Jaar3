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

    void Awake()
    {
        portraitSprite = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator ChangePortrait(string actor, string portrait)
    {
        while(!anim.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            yield return new WaitForEndOfFrame();
        }

        string path = "Portraits/" + actor + "/" + portrait;

        print(path);

        Sprite port = Resources.Load<Sprite>(path);
        portraitSprite.sprite = port;
        portraitString = portrait;
        print(portrait);

        yield break;
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
