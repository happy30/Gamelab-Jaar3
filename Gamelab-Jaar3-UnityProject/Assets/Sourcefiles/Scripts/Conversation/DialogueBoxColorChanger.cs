using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxColorChanger : MonoBehaviour
{
	public Image box;
	float fadeSpeed;
    public Color newColor;

	void Awake()
	{
		box = GetComponent<Image>();
	}

	void Start()
	{
		fadeSpeed = 7;
	}
	
	public void ChangeColorOnActor(Actors.Actor act)
    {
        switch(act)
        {
            case Actors.Actor.Chiaki:
                newColor = new Color32(0x03, 0x40, 0x45, 0xFF);
                break;

            case Actors.Actor.Hiyoko:
                newColor = new Color32(0x67, 0x53, 0x1D, 0xFF);
                break;

            case Actors.Actor.Mind:
                newColor = Color.black;
                break;

            case Actors.Actor.Client929:
                newColor = new Color32(0x56, 0x56, 0x56, 0xFF);
                break;

            case Actors.Actor.Client274:
                newColor = new Color32(0xcc, 0x52, 0x00, 0xFF);
                break;

            case Actors.Actor.Client597:
                newColor = new Color32(0x66, 0x66, 0x99, 0xFF);
                break;

        }
        StopAllCoroutines();
        StartCoroutine(ChangeColor(newColor));
    }
	

	public IEnumerator ChangeColor(Color col)
	{
		while(box.color != col)
		{
			box.color = Color.Lerp(box.color, col, fadeSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

}
