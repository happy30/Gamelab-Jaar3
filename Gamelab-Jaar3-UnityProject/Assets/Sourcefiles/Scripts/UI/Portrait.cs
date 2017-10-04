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

    public void ChangePortrait(string portrait)
    {
        Sprite port = (Sprite)Resources.Load(portrait);
        portraitSprite.sprite = port;
        portraitString = portrait;
    }
}
