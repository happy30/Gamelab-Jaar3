using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOverlay : MonoBehaviour
{
    public float waitTime;
    public float fadeSpeed;
    public bool enableFade;

    float alpha;
    float timer;

    Image overlay;

    void Awake()
    {
        overlay = GetComponent<Image>();
    }

    void Start()
    {
        if(enableFade)
        {
            overlay.color = new Color(0, 0, 0, 1);
            StartCoroutine(FadeOut());
        }
    }


    IEnumerator FadeOut()
    {
        alpha = 1;
        timer = 0;
        while(timer < waitTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        while(overlay.color.a > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alpha);
            yield return new WaitForEndOfFrame();

        }
        yield break;
    }
}
