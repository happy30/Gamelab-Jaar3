using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Portrait : MonoBehaviour
{
    Image portraitSprite;
    public string portraitString;

    void Awake()
    {
        portraitSprite = GetComponent<Image>();
    }

    public void ChangePortrait(string actor, string portrait)
    {
        string path = "Portraits/" + actor + "/" + portrait;

        print(path);

        Sprite port = Resources.Load<Sprite>(path);
        portraitSprite.sprite = port;
        portraitString = portrait;
        print(portrait);
    }
}
