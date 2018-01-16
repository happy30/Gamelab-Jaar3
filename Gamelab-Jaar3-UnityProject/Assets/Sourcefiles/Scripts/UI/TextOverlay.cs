using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextOverlay : MonoBehaviour
{
    public float waitTime;
    public float fadeSpeed;
    public bool enableFade;

    float alpha;
    float timer;

    Text overlay;

    void Awake()
    {
        overlay = GetComponent<Text>();
    }

    void Start()
    {
        if (enableFade)
        {
            overlay.color = new Color(1, 1, 1, 1);
            StartCoroutine(FadeOut());
            overlay.text = SceneManager.GetActiveScene().name;
        }
    }


    IEnumerator FadeOut()
    {
        alpha = 1;
        timer = 0;
        while (timer < waitTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }


        while (overlay.color.a > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, alpha);
            yield return new WaitForEndOfFrame();

        }
        yield break;
    }
}
