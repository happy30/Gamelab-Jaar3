using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{

    public Text infoText;
    public Animator anim;

    public void ShowInfoText(string txt)
    {
        infoText.text = txt;
    }
}
