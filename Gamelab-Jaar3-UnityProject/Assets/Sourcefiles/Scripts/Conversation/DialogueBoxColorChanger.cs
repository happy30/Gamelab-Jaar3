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

            case Actors.Actor.Mind:
                newColor = Color.black;
                break;

            case Actors.Actor.Dougal:
                newColor = new Color32(0x56, 0x56, 0x56, 0xFF);
                break;

            case Actors.Actor.Blaze:
                newColor = new Color32(0xcc, 0x52, 0x00, 0xFF);
                break;

            case Actors.Actor.Livie:
                newColor = new Color32(0x00, 0x86, 0xb3, 0xFF);
                break;

            case Actors.Actor.Moss:
                newColor = new Color32(0xA7, 0xA4, 0x32, 0xFF);
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
